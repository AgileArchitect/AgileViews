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
        public static void Main()
        {
            var path = Path.GetFullPath(@"..\..\..\AgileViews.sln");
            // arrange
            var analyzer = new RoslynAnalyser(path);

            // act
            var projects = analyzer.Projects(p => true);

            var workspace = new Workspace("AgileViews");

            var model = workspace.GetModel();

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

            var view = workspace.CreateView(new Element<string> { Name = "AgileViews" }, ViewType.Classes);
            view.AddElements(e => e is Element<Project>);
            view.AddEverythingRelatedTo(model.ElementByName("Element"));

            foreach (var p in projects)
            {
                var v = workspace.CreateView(p, ViewType.Classes);
                v.AddElements(e => e.GetParent() == p);
            }

            workspace.Export(@"D:\Projects\Private\AgileArchitect\Documentation\JekyllSite");
        }

    }
}
