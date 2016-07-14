using System.Linq;
using AgileViews.Model;

namespace AgileViews.Extensions
{
    public static class ExtentionsToView
    {

        public static void AddEverythingRelatedTo(this View view, Element relatedTo)
        {
            view.AddElements(
                e =>
                    view.Model.Relationships.Any(
                        r => (r.Source == relatedTo || r.Target == relatedTo) && (r.Source == e || r.Target == e)));
        }
    }
}