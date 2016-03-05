using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Export.Svg;
using AgileViews.Extensions;
using AgileViews.Model;
using AgileViews.Scrape;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace AgileViews.Test
{
    public class AnalyserTest
    {
        [Fact]
        public void FoundProjects()
        {
            // arrange
            var analyzer = new RoslynAnalyser(@"..\..\..\AgileViews.sln");

            // act
            var projects = analyzer.Projects(p => p.Name.Contains("Agile") && !p.Name.Contains("Test"));


            var workspace = new Workspace();
            var model = workspace.GetModel();

            model.AddAll(projects);
            model.AddAll(analyzer.GetProjectDependencies(projects));

            var classes = analyzer.Classes(projects, c => true).ToList();
            var interfaces = analyzer.Interfaces(projects, i => true).ToList();

            model.AddAll(classes);
            model.AddAll(interfaces);

            var attributed = analyzer.GetClassesWithAttribute(projects, "AgileViews.Model.Rationale").ToList();

            new ScrapeClassAttribute().Scape(model, "rationale", "AgileViews.Model.Rationale", 0);

            foreach (var c in classes)
            {
                model.AddAll(c.RelationshipsFromClass());
            }

            model.ResolveNodes();

            model.ElementByName("Element").Add(Information.URL, "http://www.google.nl");

            var view = workspace.CreateView(projects.Single(), "class");

            ViewConfiguration.AddConfiguration("class", new ClassDiagramConfiguration());

            //view.AddChildren();
            view.AddEverythingRelatedTo(model.ElementByName("Element"));

            // assert
            var exporter = new JekyllExporter(new JekyllExporterConfiguration(@"D:\Projects\AgileArchitect\Documentation\jekyll"));
            exporter.Export(view);
        }

    }
}
