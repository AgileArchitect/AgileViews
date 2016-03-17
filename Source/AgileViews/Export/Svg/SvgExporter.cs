using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AgileViews.Export.Jekyll;
using AgileViews.Model;

namespace AgileViews.Export.Svg
{
    public class SvgExporter : IJekyllViewExporter
    {
        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            // create the graph
            var graph = new ViewToGraph().Convert(view);
            
            // write the graph
            new SvgWriter().Write(graph, writer);

//          writer.WriteLine($" <p><object type='image/svg+xml' data='/uml/{guid}.svg' class='uml'>Your browser does not support SVG</object></p>");
        }
    }

    public class PngExporter : IJekyllViewExporter
    {
        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            var graph = new ViewToGraph().Convert(view);

            int width = 1200;
            if (graph.NodeCount > 100)
                width = 1200 + graph.NodeCount/10;

            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), PixelFormat.Format32bppPArgb);
            var pngFile = Path.Combine(exporter.Configuration.JekyllPath, view.Subject.Alias + ".png");
            var renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
            renderer.Render(bitmap);
            bitmap.Save(pngFile);

            writer.WriteLine($"![${view.Subject.Name}](/${pngFile})");
        }
    }
}