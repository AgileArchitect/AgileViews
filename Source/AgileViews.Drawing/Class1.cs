using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DocCoder.Export;
using DocCoder.Model;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Layout.MDS;
using Edge = Microsoft.Msagl.Drawing.Edge;
using Label = Microsoft.Msagl.Drawing.Label;
using Node = Microsoft.Msagl.Drawing.Node;

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

    public class SvgExporter : IJekyllViewExporter
    {
        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            // create the graph
            var graph = new ViewToGraph().Convert(view);
            
            // write the graph
            new SvgWriter().Write(graph, Path.Combine(exporter.Configuration.JekyllPath, $"uml\\{guid}.svg"));

            writer.WriteLine($" <p><object type='image/svg+xml' data='/uml/{guid}.svg' class='uml'>Your browser does not support SVG</object></p>");
        }
    }

    public class ViewToGraph
    {
        private Action<Element, Node> _nodeDecorator = (e,n) =>
        {
            n.Attr.LabelMargin = 10;

            if (e is Person)
            {
                n.Attr.FillColor = Color.LightGray;
                n.Attr.Shape = Shape.Box;
                n.Attr.XRadius = 10;
                n.Attr.YRadius = 10;
            }
            else
            {
                n.Attr.Shape = Shape.Box;
                n.Attr.XRadius = 0;
                n.Attr.YRadius = 0;
            }
            n.Attr.LineWidth = 2;
        };
        private Action<Relationship, Edge> _edgeDecorator = (r, e) =>
        {
            e.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
        };

        public ViewToGraph()
        {
            
        }

        public Graph Convert(View view)
        {
            var graph = new Graph();

            view.Elements.ToDictionary(e => e, e =>
            {
                var node = graph.AddNode(e.Alias);
                node.LabelText = e.Name;
                _nodeDecorator(e,node);
                return node;
            });

            foreach (var rel in view.Relationships)
            {
                var edge = graph.AddEdge(rel.Source.Alias, rel.Target.Alias);
                if (!string.IsNullOrEmpty(rel.Label))
                {
                    edge.LabelText = rel.Label;
                }
                _edgeDecorator(rel,edge);
            }

            SetConsolasFontAndSize(graph, 13);

            graph.LayoutAlgorithmSettings = PickLayoutAlgorithmSettings(graph.NodeCount, graph.EdgeCount);

            var renderer = new GraphRenderer(graph);
            renderer.CalculateLayout();

            SetConsolasFontAndSize(graph, 11);

            return graph;
        }

        static void SetConsolasFontAndSize(Graph graph, int size)
        {
            var labels = GetAllLabels(graph);
            foreach (var label in labels)
            {
                label.FontName = "Consolas";
                label.FontSize = size;
            }
        }

        static IEnumerable<Label> GetAllLabels(Graph graph)
        {
            foreach (var node in graph.Nodes)
                if (node.Label != null)
                    yield return node.Label;
            foreach (var edge in graph.Edges)
                if (edge.Label != null)
                    yield return edge.Label;
        }

        static void EnlargeLabelMargins(Graph graph)
        {
            foreach (var node in graph.Nodes)
                node.Attr.LabelMargin = 8;
        }

        static LayoutAlgorithmSettings PickLayoutAlgorithmSettings(int edges, int nodes)
        {
            LayoutAlgorithmSettings settings;
            const int sugiaymaTreshold = 200;
            const double bundlingTreshold = 3.0;
            bool bundling = nodes != 0 && ((double)edges / nodes >= bundlingTreshold || edges > 100);
            if (nodes < sugiaymaTreshold && edges < sugiaymaTreshold)
            {
                settings = new SugiyamaLayoutSettings();

                if (bundling)
                    settings.EdgeRoutingSettings.EdgeRoutingMode = EdgeRoutingMode.SplineBundling;
            }
            else {
                MdsLayoutSettings mdsSettings;
                settings = mdsSettings = new MdsLayoutSettings
                {
                    EdgeRoutingSettings = {
                        EdgeRoutingMode
                            =
                            bundling
                                ? EdgeRoutingMode.SplineBundling
                                : EdgeRoutingMode.Spline
                    }
                };
                if (bundling)
                    settings.EdgeRoutingSettings.BundlingSettings = new BundlingSettings();
                double scale = FigureOutScaleForMdsLayout(nodes);
                mdsSettings.ScaleX = scale;
                mdsSettings.ScaleY = scale;
            }
            return settings;
        }

        static double FigureOutScaleForMdsLayout(int nodes)
        {
            const int maxScale = 900;
            const int minScale = 400;
            return Math.Min(nodes + minScale, maxScale);
        }

        static void SetBoxRadiuses(Graph graph)
        {
            foreach (var node in graph.Nodes)
            {
                var attr = node.Attr;
                var r = Math.Min(node.Width, node.Height) / 10;
                attr.XRadius = r;
                attr.YRadius = r;
            }
        }
    }

    public class SvgWriter
    {
        public void Write(Graph graph, string filename)
        {
            var directory = Path.GetDirectoryName(filename);
            Directory.CreateDirectory(directory);

            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var writer = new SvgGraphWriter(stream, graph);
                writer.Write();
                stream.Flush();
                stream.Close();
            }

        }
    }
}
