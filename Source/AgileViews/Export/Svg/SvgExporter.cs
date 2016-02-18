using System;
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
}