using System.IO;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll.Views
{
    public abstract class AbstractViewExporter : IJekyllViewExporter
    {
        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            writer.WriteLine(Header());
            foreach (var element in view.Elements)
            {
                writer.WriteLine(ExportElement(element));
            }
            foreach (var relation in view.Relationships)
            {
                writer.WriteLine(ExportRelationship(relation));
            }
            writer.WriteLine(Footer());
        }

        protected abstract string Header();
        protected abstract string Footer();
        protected abstract string ExportElement(Element element);

        protected abstract string ExportRelationship(Relationship relationship);
    }
}