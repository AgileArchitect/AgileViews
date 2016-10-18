using System.IO;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    public interface IJekyllViewExporter
    {
        void Export(JekyllExporter exporter, View view, StreamWriter writer);
    }
}