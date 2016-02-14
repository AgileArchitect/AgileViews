using System.IO;
using DocCoder.Model;

namespace DocCoder.Export
{
    public interface IJekyllViewExporter
    {
        void Export(JekyllExporter exporter, View view, StreamWriter writer);
    }
}