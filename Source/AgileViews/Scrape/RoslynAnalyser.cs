using System;
using System.Collections.Generic;
using System.Linq;
using AgileViews.Extensions;
using AgileViews.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AgileViews.Scrape
{
    /// <summary>
    /// Features: 
    /// - Find and group projects as container or componenets
    /// - Find and group namespaces as component
    /// - Find and group classes/interfaces/enums as component (having a single type as leader)
    /// - Find classes/interfaces/enums as component content
    /// </summary>
    public class RoslynAnalyser
    {
        public static Predicate<Project> ALL_PROJECTS = p => true;

        private Solution _solution;
        public RoslynAnalyser(string solutionPath)
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            _solution = ws.OpenSolutionAsync(solutionPath).Result;
            var proj = _solution.Projects.Select(p => p.Name);
        }

        public ICollection<Element<Project>> Projects(Predicate<Project> predicate)
        {
            return _solution.Projects.Where(p => predicate(p)).Select(p => new Element<Project> {UserData = p, Name = p.Name}).ToList();
        }

        public ICollection<Relationship> GetProjectDependencies(IEnumerable<Element<Project>> projects)
        {
            var dict = projects.ToDictionary(p => p.UserData.Id, p => p);
            var result = new List<Relationship>();
            foreach (var p in projects)
            {
                foreach (var reference in p.UserData.ProjectReferences)
                {
                    if (dict.ContainsKey(reference.ProjectId))
                    {
                        result.Add(new Relationship
                        {
                            Source = p,
                            Target = dict[reference.ProjectId]
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Yields classes and interfaces
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Element<SyntaxNode>> Classes(IEnumerable<Element<Project>> projects, Predicate<SyntaxNode> predicate)
        {
            foreach (var p in projects)
            {
                var comp = p.UserData.GetCompilationAsync().Result;

                foreach (var s in comp.SyntaxTrees)
                {
                    var sm = comp.GetSemanticModel(s);

                    foreach (
                        var decl in
                            s.GetRoot()
                                .DescendantNodesAndSelf()
                                .OfType<ClassDeclarationSyntax>()
                                .Where(x => predicate(x)))
                    {
                        string name = decl.Identifier.Text;

                        var cs = sm.GetDeclaredSymbol(decl);
                        if (cs.GetAttributes().Any())
                        {
                            Console.WriteLine(".");
                        }


                        if (decl.TypeParameterList != null)
                            name += decl.TypeParameterList.ToString();

                            yield return new Element<SyntaxNode>()
                            {
                                UserData = decl,
                                Parent = p,
                                Name = name
                            };
                    }
                }
            }
        }


        public IEnumerable<Element<SyntaxNode>> Interfaces(IEnumerable<Element<Project>> projects, Predicate<SyntaxNode> predicate)
        {
            foreach (var p in projects)
            {
                var comp = p.UserData.GetCompilationAsync().Result;

                foreach (var s in comp.SyntaxTrees)
                {
                    foreach (
                        var decl in
                            s.GetRoot()
                                .DescendantNodesAndSelf()
                                .OfType<InterfaceDeclarationSyntax>()
                                .Where(x => predicate(x)))
                    {
                        string name = decl.Identifier.Text;

                        if (decl.TypeParameterList != null)
                            name += decl.TypeParameterList.ToString();

                        yield return new Element<SyntaxNode>()
                        {
                            UserData = decl,
                            Parent = p,
                            Name = name
                        };
                    }
                }
            }
        }

        //        public IEnumerable<Element<SyntaxNode>> Namespaces(IEnumerable<Element<Project>> projects,
        //            Predicate<SyntaxNode> predicate)
        //        {
        //            return projects.Union(p => FromProject(p, sn => sn.Kind() == SyntaxKind.NamespaceDeclaration));
        //        }

        private IEnumerable<Element<SyntaxNode>> FromProject(Project p, Predicate<SyntaxNode> selector, Element parent)
        {
            var comp = p.GetCompilationAsync().Result;
            foreach (var s in comp.SyntaxTrees)
            {
                foreach (var decl in s.GetRoot().DescendantNodesAndSelf().Where(x => selector(x)))
                {
                    yield return new Element<SyntaxNode>() { UserData = decl, Parent = parent };
                }
            }
        }
    }

    public static class ProjectExtensions
    {
        public static bool IsConsoleApplication(this Project project)
        {
            return project.CompilationOptions.OutputKind == OutputKind.ConsoleApplication;
        }

        public static ICollection<Relationship> RelationshipsFromClass(this Element<SyntaxNode> node)
        {
            var classSyntax = (node.UserData as ClassDeclarationSyntax);
            if (classSyntax.BaseList == null)
                return new List<Relationship>();

            var result = new List<Relationship>();

            // members
            foreach (var prop in classSyntax.Members.OfType<PropertyDeclarationSyntax>() )
            {
                if (prop.Type is IdentifierNameSyntax)
                {
                    var r = new Relationship()
                    {
                        Source = node,
                        TargetName = (prop.Type as IdentifierNameSyntax).Identifier.Text,
                        Label = "depends"
                    };
                    result.Add(r);
                }
                // do something smart
            }

            classSyntax.BaseList.Types.ForEach(t =>
            {
                if (t is SimpleBaseTypeSyntax && t.Type is IdentifierNameSyntax)
                {
                    var st = t as SimpleBaseTypeSyntax;
                    var r = new Relationship()
                    {
                        Source = node,
                        TargetName = (st.Type as IdentifierNameSyntax).Identifier.Text,
                        Label = "inherits"
                    };
                    result.Add(r);
                }
                else
                {
                    throw new Exception("Unsupport base type syntax");
                }
            });

            return result;
        }
    }
}