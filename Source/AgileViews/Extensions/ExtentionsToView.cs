using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileViews.Model;

namespace AgileViews.Extensions
{
    public static class ExtentionsToView
    {
        public static void AddEverythingRelatedTo(this View view, Element relatedTo)
        {
            view.AddElements(e => view.Model.Relationships.Any(r => (r.Source == relatedTo || r.Target == relatedTo) && (r.Source == e || r.Target == e)));
        }
    }
}
