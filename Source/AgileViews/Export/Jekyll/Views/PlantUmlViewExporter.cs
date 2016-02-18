using System.Collections.Generic;
using System.IO;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll.Views
{
    public class PlantUmlViewExporter : IJekyllViewExporter
    {
        private Dictionary<ViewType, AbstractViewExporter> _strategies = new Dictionary<ViewType, AbstractViewExporter>() { 
            {ViewType.Context,new PlantUmlContextStrategy()},
            {ViewType.Containers,new PlantUmlContainerStrategy()}
        };

        public void Export(JekyllExporter exporter, View view, StreamWriter writer)
        {
            writer.WriteLine("### Diagram");
            if ( _strategies.ContainsKey(view.ViewType))
                _strategies[view.ViewType].Export(exporter, view, writer);
        }
    }
}