using System.IO;
using AgileViews.Export.Jekyll.Views;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    /// <summary>
    /// TODO: 
    /// 
    /// permalink structure
    /// 
    /// /context/NControl
    /// /containers/NControl
    /// /components/MijnNControl
    /// /classes/ZorgPlus
    /// 
    /// </summary>
    public class JekyllExporter
    {
        private readonly JekyllExporterConfiguration _configuration;

        public JekyllExporterConfiguration Configuration => _configuration;


        public JekyllExporter(JekyllExporterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Export(View view, IJekyllViewExporter imageExporter)
        {
            var path = Path.Combine(_configuration.JekyllPath, $"components/{view.Name}.md");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var writer = new StreamWriter(path))
            {
                var scope = view.ViewType.ToString().ToLowerInvariant();

                writer.WriteLine("---");
                writer.WriteLine("layout: page");
                writer.WriteLine($"title: {view.Name}");
                writer.WriteLine($"permalink: /{scope}/{view.Name}");
                writer.WriteLine($"tags: [generated, {scope}, diagram]");
                writer.WriteLine("---");
                // export the image

                writer
                    .AppendViewBlock(this, view, imageExporter)
                    .EmptyLine()
                    .AppendViewBlock(this, view, new ElementListViewExporter());

                writer.Flush();
                writer.Close();
            }
        }
    }
}