using System.Collections.Generic;

namespace AgileViews.Model
{
    public class Workspace
    {
        private readonly Model _model = new Model();
        public ViewSet Set { get; }

        public Workspace()
        {
            Set = new ViewSet(_model);
        }

        public Model GetModel()
        {
            return _model;
        }

        public View CreateView(Element subject, string viewType)
        {
            return Set.Create(subject, viewType);
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

        public View Create(Element subject, string viewType)
        {
            var view = new View(subject, new AllRelationshipStrategy(), viewType);
            view.Model = _model;
            Views.Add(view);
            return view;
        }
    }
}