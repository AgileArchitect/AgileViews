using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Export.Svg;
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
            var analyzer = new Analyser(@"..\..\..\AgileViews.sln");

            // act
            var projects = analyzer.Projects(Analyser.ALL_PROJECTS);

            Assert.NotEqual(0, projects.Count);

            var workspace = new Workspace();

            var model = workspace.GetModel();

            var system = model.AddSystem("NControl", "What we make", Location.Internal);

            var dict = projects.ToDictionary(p => p.Id, p => p);
            var elements = projects.ToDictionary(p => p.Id, p => system.AddContainer(p.Name, p.AssemblyName));

            foreach (var p in projects)
            {
                foreach (var reference in p.ProjectReferences)
                {
                    if (dict.ContainsKey(reference.ProjectId))
                    {
                        elements[p.Id].Uses(elements[reference.ProjectId], "reference");
                    }
                }
            }

            var compolation =
                projects.First().GetCompilationAsync().Result.SyntaxTrees.First().GetRoot().DescendantNodesAndSelf().Where(x => x.IsKind(x));


            var view = workspace.CreateContainerView(system);
            view.AddChildren();

            // assert
            var exporter = new JekyllExporter(new JekyllExporterConfiguration(@"D:\Projects\AgileArchitect\Documentation\jekyll"));
            exporter.Export(view, new SvgExporter());
        }

//        [Fact]
//        public void TestExport()
//        {
//            var workspace = new Workspace();
//
//            var model = workspace.GetModel();
//            var user = model.AddPerson("Employee Pharmacy", "An employee in a pharmacy", Location.External);
//            var ncontrol = model.AddSystem("NControl", "Medication Related HealthCare System", Location.Internal);
//            var ncasso = model.AddSystem("NCasso", "Healhcare Insurer Declaration Portal", Location.External);
//
//            user.Uses(ncontrol, "uses");
//            ncontrol.Uses(ncasso, "uses");
//
//            var view = workspace.CreateContextView(ncontrol);
//
//            view.AddAllSystems();
//            view.AddAllPeople();
//
//            var exporter = new JekyllExporter(new JekyllExporterConfiguration(@"D:\Projects\AgileArchitect\Documentation\jekyll"));
//            exporter.Export(view, new SvgExporter());
//        }
    }
}
