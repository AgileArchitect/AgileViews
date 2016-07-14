using System.IO;
using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Extensions;
using AgileViews.Model;
using AgileViews.Scrape;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using Workspace = AgileViews.Model.Workspace;

namespace AgileViews.Test
{
    public class AnalyserTest
    {
        [Fact]
        public void FoundProjects()
        {
            var path = Path.GetFullPath(@"..\..\..\..\..\..\..\Libs\NCore\src\NCore.sln");
            // arrange
            var analyzer = new RoslynAnalyser(path);

            // act
            var projects = analyzer.Projects(p => true);
            var system = new Element<string>() { Name = "System " };

            var workspace = new Workspace();

            var model = workspace.GetModel();

            model.Add(system);
            model.AddAll(projects);
            model.AddAll(analyzer.GetProjectDependencies(projects));

            var classes = analyzer.Classes(projects, c => true).GroupBy(e => e.UserData).Select(g => g.First()).ToList();
            var interfaces = analyzer.Interfaces(projects, i => true).GroupBy(e => e.UserData).Select(g => g.First()).ToList();
//
            model.AddAll(classes);
            model.AddAll(interfaces);

//            var attributed = analyzer.GetClassesWithAttribute(projects, "AgileViews.Model.Rationale").ToList();

            new ScrapeClassAttribute().Scape(model, "rationale", "AgileViews.Model.Rationale", 0);

            foreach (var c in classes)
            {
                model.AddAll(c.RelationshipsFromClass());
            }

            model.ResolveNodes();

            var element = model.ElementByName("AgileViews.Element");
            if (element != null)
            {
                element.Add(Information.URL, "http://www.google.nl");
            }

            var view = workspace.CreateView(system, "package");
            view.AddElements(e => e is Element<Project>);
            view.AddEverythingRelatedTo(model.ElementByName("Element"));

            foreach (var p in projects)
            {
                var v = workspace.CreateView(p, "class");
                v.AddElements(e => e.GetParent() == p);
            }

            // assert
            var exporter = new JekyllExporter(new JekyllExporterConfiguration(@"D:\Projects\Private\AgileArchitect\Documentation\JekyllSite"));
            

            foreach (var v in workspace.Set.Views)
            {
                exporter.Export(v);
            }

        }

    }
}
