using System;
using System.IO;
using System.Linq;
using AgileViews.Export.Jekyll;
using AgileViews.Extensions;
using AgileViews.Model;
using DotLiquid;

namespace AgileViews.Export.JoinJs
{
    internal class XGraphVizExporter : IJekyllViewExporter
    {
        public string BoilerPlate()
        {
            return @"
            
            node[
                fontname = ""Helvetica"";                
                shape = ""record""
            ]

            edge[
                fontname = ""Helvetica"";
                fontsize = 10
            ]";
        }

        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            Template template = Template.Parse("hi {{view}}");  // Parses and compiles the template
            template.Render(Hash.FromAnonymousObject(view)); // Renders the output => "hi tobi"


            var guid = Guid.NewGuid();

            writer.WriteLine(@"
{% xdot png %}
digraph finite_state_machine {
    rankdir=TB;
    size=""6"";
"
+ BoilerPlate()
+ string.Join(Environment.NewLine, view.Elements.Select(e => e.QualifiedName))
+ string.Join(Environment.NewLine, view.Relationships.Select(r => $"{r.Source.QualifiedName} -> {r.Target.QualifiedName} [ label = \"{r.Label}\"]"))
+
@"      
}
{% endxdot %}");
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