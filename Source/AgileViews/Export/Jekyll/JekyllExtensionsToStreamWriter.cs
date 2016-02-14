using System.IO;
using DocCoder.Model;

namespace DocCoder.Export
{
    public static class JekyllExtensionsToStreamWriter
    {
        public static StreamWriter EmptyLine(this StreamWriter writer)
        {
            writer.WriteLine("");
            return writer;
        }

        public static StreamWriter AppendViewBlock(this StreamWriter writer, JekyllExporter exporter, View view, IJekyllViewExporter viewExporter)
        {
            viewExporter.Export(exporter, view, writer);
            return writer;
        }

    }
}