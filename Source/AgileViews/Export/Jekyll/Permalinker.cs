using System;
using System.Collections;
using System.Collections.Generic;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    public class Permalinker
    {
        public static IDictionary<Type, ViewType> _DefaultViewTypes = new Dictionary<Type, ViewType>()
        {
            { typeof(Model.System), ViewType.Context},
            { typeof(Model.Container), ViewType.Components},
            { typeof(Model.Component), ViewType.Classes},
        };

        public static ViewType GetDefaultViewType(Element element)
        {
            var type = element.GetType();
            return _DefaultViewTypes[element.GetType()];
        }

        public static string GetUrl(ViewType viewType, Element element)
        {
            return $"/{viewType}/{element.Alias}";
        }
    }
}