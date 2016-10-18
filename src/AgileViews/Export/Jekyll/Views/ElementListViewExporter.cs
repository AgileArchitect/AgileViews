using System.Linq;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll.Views
{
    public class ElementListViewExporter : AbstractViewExporter
    {
        protected override string Header()
        {
            return "### Elements";
        }

        protected override string ExportElement(Element element)
        {
            var name = element.Name;
            var description = element.Description;
            var url = element.GetInformation(Information.URL);

            if (!url.Any())
            {
                return $"* **{name}**: {description}";
            }
            return $"* **[{name}]({url.First()})**: {description}";
        }


        protected override string ExportRelationship(Relationship relationship)
        {
            return string.Empty;
        }

        protected override string Footer()
        {
            return string.Empty;
        }
    }
}