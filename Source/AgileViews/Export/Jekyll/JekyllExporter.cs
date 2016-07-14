using System.IO;
using AgileViews.Export.Jekyll.Views;
using AgileViews.Export.JoinJs;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    /// <summary>
    ///     TODO:
    ///     permalink structure
    ///     /context/NControl
    ///     /containers/NControl
    ///     /components/MijnNControl
    ///     /classes/ZorgPlus
    /// </summary>
    public class JekyllExporter
    {
        public JekyllExporter(JekyllExporterConfiguration configuration)
        {
            Configuration = configuration;
        }

        public JekyllExporterConfiguration Configuration { get; }

        public void Export(View view)
        {
            var path = Path.Combine(Configuration.JekyllPath, $"components/{view.Name}.md");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var writer = new StreamWriter(path))
            {
                var scope = view.ViewType.ToLowerInvariant();

                writer.WriteLine("---");
                writer.WriteLine("layout: page");
                writer.WriteLine($"title: {view.Name}");
                writer.WriteLine($"permalink: /{scope}/{view.Name}/");
                writer.WriteLine($"tags: [generated, {scope}, diagram]");
                writer.WriteLine("---");
                // export the image

                writer
                    //.AppendViewBlock(this, view, new SvgExporter())
                    .AppendViewBlock(this, view, new CytoScapeExporter())
                    .EmptyLine()
                    .AppendViewBlock(this, view, new ElementListViewExporter());

                writer.Flush();
                writer.Close();
            }
        }
    }
}