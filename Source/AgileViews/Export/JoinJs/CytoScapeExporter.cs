using System;
using System.IO;
using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Extensions;
using AgileViews.Model;

namespace AgileViews.Export.JoinJs
{
    internal class CytoScapeExporter : IJekyllViewExporter
    {
        

        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            var guid = Guid.NewGuid();

            writer.WriteLine($"<div id='{guid}' class='cy'></div>");
            writer.WriteLine(@"
<script>
var cy = cytoscape({
");
            writer.WriteLine($"container: document.getElementById('{guid}'),");
            writer.WriteLine("elements: [");

            var nodes = view.Elements.Select(e => "{ data: { id: '" + e.Name + "' } }");
            var edges = view.Relationships.Select(r => "{ data: { id: '" + r.Source.Name + "_to_" + r.Target.Name + "', source: '"+ r.Source.Name +"', target: '"+r.Target.Name+"' } }");

            writer.WriteLine(string.Join(",", nodes.Union(edges)));

            writer.WriteLine(@"
  ],

  style: [ // the stylesheet for the graph
    {
        selector: 'node',
        style: {
            'background-color': '#666',
            'label' : 'data(id)',
            'text-halign' : 'center',
            'text-valign' : 'center',
            'background-color':'#555',
            'text-outline-color':'#555',
            'text-outline-width':'1px',
            'color':'#fff',
            'overlay-padding':'6px'
        }
    },
    {
      selector: 'edge',
      style: {
        'width': 3,
        'line-color': '#ccc',
        'target-arrow-color': '#ccc',
        'target-arrow-shape': 'triangle',
         'curve-style': 'haystack',
         'haystack-radius': '0.5',
      }
    }
  ],

  layout: {
    name: 'cola',
    fit: true,
    nodeSpacing: 5,
    edgeLengthVal: 45
  }

            });
</script>");
        }
    }

    public static class ExtensionsToElement
    {
        public static string VariableName(this Element element)
        {
            return "a" + Math.Abs(element.Name.GetHashCode());
        }

        public static string ShortName(this Element element)
        {
            var name = element.Name.Split('.').Last();
            if (name.Length > 12)
            {
                return name.Substring(0, 10) + "...";
            }
            return name;
        }
    }
}