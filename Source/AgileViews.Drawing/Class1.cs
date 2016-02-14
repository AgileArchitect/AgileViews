using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace DocCoder.Drawing
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            var graph = new Graph();
            graph.AddNode(new Node("pietje"));

            var renderer = new GraphRenderer(graph);
            renderer.CalculateLayout();

            using (var stream = new FileStream("test.svg", FileMode.OpenOrCreate))
            {
                var writer = new SvgGraphWriter(stream, graph);
                writer.Write();
                stream.Flush();
                stream.Close();
            }
        }
    }

}
