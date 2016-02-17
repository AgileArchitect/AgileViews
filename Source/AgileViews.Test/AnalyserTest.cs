using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocCoder.Drawing;
using DocCoder.Export;
using DocCoder.Model;
using Xunit;

namespace DocCoder.Test
{
    public class AnalyserTest
    {
//        [Fact]
//        public void FoundProjects()
//        {
//            // arrange
//            var analyzer = new Analyser(@"D:\Projects\ncontrol\Source\NControl\NControl.sln");
//
//            // act
//            var projects = analyzer.Projects(Analyser.ALL_PROJECTS);
//
//            var workspace = new Workspace();
//
//            var model = workspace.GetModel();
//
//            var system = model.AddSystem("NControl", "What we make", Location.Internal);
//
//            var dict = projects.ToDictionary(p => p.Id, p => p);
//            var elements = projects.ToDictionary(p => p.Id, p => system.AddContainer(p.Name, p.AssemblyName));
//
//            foreach (var p in projects)
//            {
//                foreach (var reference in p.ProjectReferences)
//                {
//                    if (dict.ContainsKey(reference.ProjectId))
//                    {
//                        elements[p.Id].Uses(elements[reference.ProjectId], "reference");
//                    }
//                }
//            }
//
//            var view = workspace.CreateContainerView(system);
//            view.AddChildren();
//
//            // assert
//            var exporter = new JekyllExporter(new JekyllExporterConfiguration(@"D:\Projects\AgileArchitect\Documentation\jekyll"));
//            exporter.Export(view, new SvgExporter());
//        }

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
