using System.Linq;
using System.Text;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll.Views
{
    public class ElementListViewExporter : AbstractViewExporter
    {
        protected override string Header()
        {
            return @"
### Elements

|-------------+----------------+----------------|
| Name        | Attribute      | Value          |
|-------------|----------------+----------------|";
        }

        protected override string ExportElement(Element element)
        {
            var sb = new StringBuilder();

            var name = element.Name;
            var descriptions = element.GetInformation(Information.Description);
            var urls = element.GetInformation(Information.Url);

            foreach (var value in descriptions)
            {
                sb.AppendLine($"| **{name}** | Description | {value} |");
            }

            foreach (var url in urls)
            {
                sb.AppendLine($"| **{name}** | Url | **[{url}]({url})** |");
            }

            return sb.ToString();
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