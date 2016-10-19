using System.IO;
using AgileViews.Export.Jekyll.Views;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    /// <summary>
    ///     TODO:
    ///     permalink structure
    ///     /NControl/context (outside of the system)
    ///     /NControl/components (inside of the system)
    ///     /NControl/components/segments (some perspective of the inside of the system)
    ///     /NControl/MijnNControl/classes (inside of a component)
    ///     /NControl/MijnNControl/components (nested)
    ///     /NControl/MijnNControl/NCare/classes
    ///     /NControl/DashboardService/packages
    /// </summary>
    public class JekyllExporter
    {
        private JekyllExporterConfiguration _configuration;

        public JekyllExporter(Workspace workspace, string destination)
        {
            _configuration = new JekyllExporterConfiguration(workspace, destination);
        }

        public void Export(View view)
        {
            var path = _configuration.Path(view);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("---");
                writer.WriteLine("layout: page");
                writer.WriteLine($"title: {view.Name}");
                writer.WriteLine($"permalink: {_configuration.Permalink(view)}");
                writer.WriteLine($"type: {view.ViewType.ToString().ToLowerInvariant()}");
                writer.WriteLine($"tags: [view, {view.Name}]");
                writer.WriteLine("---");
                // export the image

                writer
                    .AppendViewBlock(this, view, new XGraphVizExporter.XGraphVizExporter())
                    .EmptyLine()
                    .AppendViewBlock(this, view, new ElementListViewExporter());

                writer.Flush();
            }
        }
    }
}