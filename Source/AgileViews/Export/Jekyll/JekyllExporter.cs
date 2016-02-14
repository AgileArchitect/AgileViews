using System.IO;
using DocCoder.Model;

namespace DocCoder.Export
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

        public JekyllExporter(JekyllExporterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Export(View view)
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
                    .AppendViewBlock(this, view, new PlantUmlViewExporter())
                    .EmptyLine()
                    .AppendViewBlock(this, view, new ElementListViewExporter());

                writer.Flush();
                writer.Close();
            }
        }
    }
}