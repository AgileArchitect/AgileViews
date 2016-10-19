using System;
using System.IO;
using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Model;

namespace AgileViews.Export.XGraphVizExporter
{
    internal class XGraphVizExporter : IJekyllViewExporter
    {
        public string BoilerPlate()
        {
            return @"
            graph [size=""20,20""];
            node[
                fontname = ""Helvetica"";                
                shape = ""record""
            ]

            edge[
                fontname = ""Helvetica"";
            ]";
        }

        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {


            var guid = Guid.NewGuid();

            writer.WriteLine(@"
{% xdot png  w=2048 %}
digraph finite_state_machine {
    rankdir=TB;
    size=""6"";
"
+ BoilerPlate()
+ string.Join(Environment.NewLine, view.Elements.Select(e => $"{e.QualifiedName.Dotify()} [ label = \"{e.Name}\"]"))
+ string.Join(Environment.NewLine, view.Relationships.Select(r => $"{r.Source.QualifiedName.Dotify()} -> {r.Target.QualifiedName.Dotify()} [ label = \"{r.Label}\"]"))
+
@"      
}
{% endxdot %}");
        }
    }

    public static class ExtensionsToString
    {
        public static string Dotify(this string s)
        {
            return s.Replace(".", "_").Replace("+", "_");
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