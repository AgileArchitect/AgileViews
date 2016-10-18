using System.Collections.Generic;
using AgileViews.Export.Jekyll;

namespace AgileViews.Model
{
    public class Workspace
    {
        public string Name { get; set; }
        private readonly Model _model = new Model();
        public ViewSet Set { get; }

        public Workspace(string name)
        {
            Name = name;
            Set = new ViewSet(_model);
        }

        public Model GetModel()
        {
            return _model;
        }

        public View CreateView(Element subject, ViewType viewType)
        {
            return Set.Create(subject, viewType);
        }

        /// <summary>
        /// Export all views
        /// </summary>
        /// <param name="destination"></param>
        public void Export(string destination)
        {
            var exporter = new JekyllExporter(this, destination);
            foreach (var view in Set.Views)
            {
                exporter.Export(view);
            }
        }
    }

    public class ViewSet
    {
        public Model _model;

        public IList<View> Views { get; } = new List<View>();

        public ViewSet(Model model)
        {
            _model = model;
        }

        public View Create(Element subject, ViewType viewType)
        {
            var view = new View(subject, viewType);
            view.Model = _model;
            Views.Add(view);
            return view;
        }
    }
}