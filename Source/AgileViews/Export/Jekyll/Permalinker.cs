using DocCoder.Model;

namespace DocCoder.Export
{
    public class Permalinker
    {
        public static string GetDefaultViewType(Element element)
        {
            if (element is Model.System)
                return $"/context/{element.Alias}";
            if (element is Model.Container)
                return $"/container/{element.Alias}";

            return null;
        }

        public static string GetPermalink(ViewType viewType, Element element)
        {
            return $"/{viewType.ToString().ToLower()}/{element.Alias.ToLower()}";
        }
    }
}