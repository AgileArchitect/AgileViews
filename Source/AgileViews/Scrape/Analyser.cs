using System;
using System.Collections.Generic;
using System.Linq;
using AgileViews.Model;
using Microsoft.CodeAnalysis;

namespace AgileViews.Scrape
{
    public class Analyser
    {
        public static Func<Project, bool> ALL_PROJECTS = p => true;
        public static Func<Project, bool> CONSOLE_APPS = p => p.CompilationOptions.OutputKind == OutputKind.ConsoleApplication;

        private Solution _solution;
        public Analyser(string solutionPath)
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            _solution = ws.OpenSolutionAsync(solutionPath).Result;
        }

        public ICollection<Project> Projects(Func<Project,bool> predicate)
        {
            return _solution.Projects.Where(predicate).ToList();
        }

        public ICollection<string> Find()
        {
            _solution.Projects.First().GetCompilationAsync().Result

            return new[] {"a"};
        }
    }

    public class ProjectFinderStrategy : IComponentFinderStrategy
    {
        public ICollection<Component> FindComponents(Solution solution)
        {
            return solution.Projects.Select(p => new Component() {Name = p.Name}).ToList();
        }

        public ICollection<Component> FindDependencies(Component component)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IComponentFinderStrategy
    {
        ICollection<Component> FindComponents(Solution solution);
        ICollection<Component> FindDependencies(Component component);
    }
}