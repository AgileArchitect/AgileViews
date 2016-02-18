using System.IO;
using Microsoft.Msagl.Drawing;

namespace AgileViews.Export.Svg
{
    public class SvgWriter
    {
        public void Write(Graph graph, StreamWriter writer)
        {
            using (var ms = new MemoryStream())
            {
                var w = new SvgGraphWriter(ms, graph);
                w.Write();
                ms.Flush();

                ms.Position = 0;
                var sr = new StreamReader(ms);
                var svg = sr.ReadToEnd();


                writer.WriteLine(svg);
            }
            
        }
    }
}
