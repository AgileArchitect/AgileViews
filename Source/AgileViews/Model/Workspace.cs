using System;
using System.Collections;
using System.Collections.Generic;
using AgileViews.Export.Svg;
using Microsoft.Msagl.Core.Layout;

namespace AgileViews.Model
{
    public class Workspace
    {
        private Model _model = new Model();
        private ViewSet _viewSet ;

        public Workspace()
        {
            _viewSet = new ViewSet(_model);
        }

        public Model GetModel()
        {
            return _model;
        }

//        public View CreateContextView(System element)
//        {
//            var view = new View(element, new AllRelationshipStrategy());
//            view.ViewType = ViewType.Context;
//            view.Model = GetModel();
//            return view;
//        }
//
//        public View CreateContainerView(System element)
//        {
//            var view = new View(element, new AllRelationshipStrategy());
//            view.ViewType = ViewType.Containers;
//            view.Model = GetModel();
//            return view;
//        }

        public View CreateView(Element subject)
        {
            return _viewSet.Create(subject);
        }
    }

    public class ViewSet
    {
        public Model _model;

        private IList<View> _views = new List<View>();

        public ViewSet(Model model)
        {
            _model = model;
        }

        public View Create(Element subject)
        {
            var view = new View(subject, new AllRelationshipStrategy());
            view.Model = _model;
            _views.Add(view);
            return view;
        }
    }

    public static class ViewConfiguration
    {
        static ViewConfiguration()
        {
            Diagram = new Dictionary<string, DiagramConfiguration>();
        }


        public static Dictionary<string, DiagramConfiguration> Diagram { get; set; }

        public class DiagramConfiguration
        {
            public LayoutAlgorithmSettings Layouter { get; set; }

            public Dictionary<string, NodeStyle> NodeStyle { get; set; } 
            public Dictionary<string, EdgeStyle> EdgeStyle { get; set; }
        }
    }

    public class Rationale : Attribute
    {
        public string Text { get; set; }
    }
}
