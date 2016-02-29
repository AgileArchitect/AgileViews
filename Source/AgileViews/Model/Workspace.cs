using System;
using System.Collections;
using System.Collections.Generic;
using AgileViews.Export.Svg;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;

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

        public View CreateView(Element subject, string viewType)
        {
            return _viewSet.Create(subject, viewType);
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

        public View Create(Element subject, string viewType)
        {
            var view = new View(subject, new AllRelationshipStrategy(), viewType);
            view.Model = _model;
            _views.Add(view);
            return view;
        }
    }

    public static class ViewConfiguration
    {
        private static Dictionary<string, DiagramConfiguration> _configurations = new Dictionary<string,DiagramConfiguration>();

        static ViewConfiguration()
        {
            
        }

        public static void AddConfiguration(string viewType, DiagramConfiguration configuration)
        {
            _configurations.Add(viewType, configuration);
        }

        public static DiagramConfiguration GetConfiguration(View view)
        {
            if (_configurations.ContainsKey(view.ViewType))
                return _configurations[view.ViewType];

            return new DefaultDiagramConfiguration();
        }

    }

    public class DefaultDiagramConfiguration : DiagramConfiguration
    {
        public override IEdgeStyle GetStyle(Relationship relationship)
        {
            return new EdgeStyle
            {
                Color = Color.Black,
                SourceStyle = ArrowStyle.None,
                TargetStyle = ArrowStyle.Normal,
                LineWidth = 1,
                ShowText = true
            };
        }

        public override INodeStyle GetStyle(Element element)
        {
            return new NodeStyle
            {
                Shape = Shape.Circle,
                FillColor = Color.White,
                BorderColor = Color.Black,
                BorderWidth = 1,
                Margin = 5,
                Radius = 0
            };
        }
    }

    public abstract class DiagramConfiguration
    {
        public LayoutAlgorithmSettings Layouter { get; }

        public abstract IEdgeStyle GetStyle(Relationship relationship);

        public abstract INodeStyle GetStyle(Element element);
    }

    public class ClassDiagramConfiguration : DiagramConfiguration
    {
        private static EdgeStyle _defaultEdgeStyle = new EdgeStyle()
        {
            Color = Color.Black,
            LineWidth = 2,
            SourceStyle = ArrowStyle.None,
            TargetStyle = ArrowStyle.None,
            ShowText = true
        };

        private static NodeStyle _defaultNodeStyle = new NodeStyle()
        {
            Shape = Shape.Box,
            FillColor = Color.White,
            BorderColor = Color.Black,
            Margin = 10,
            BorderWidth = 2,
            Radius = 0
        };

        private static EdgeStyle _generalization;

        static ClassDiagramConfiguration()
        {
            _generalization = _defaultEdgeStyle.Clone();
            _generalization.ShowText = false;
            _generalization.TargetStyle = ArrowStyle.Generalization;
        }

        public override IEdgeStyle GetStyle(Relationship relationship)
        {
            if (relationship.Label == "inherits")
            {
                return _generalization;
            }

            return _defaultEdgeStyle;
        }

        public override INodeStyle GetStyle(Element element)
        {
            
            return _defaultNodeStyle;
        }
    }
}
