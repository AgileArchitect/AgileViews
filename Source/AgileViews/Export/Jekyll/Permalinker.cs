using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    public class Permalinker
    {
        public static string GetDefaultViewType(Element element)
        {
            return null;
        }

        public static string GetPermalink(ViewType viewType, Element element)
        {
            return $"/{viewType.ToString().ToLower()}/{element.Alias.ToLower()}/";
        }
    }
}