using Microsoft.Msagl.Drawing;

namespace AgileViews.Export.Svg
{
    public class EdgeStyle : IEdgeStyle
    {
        public Color Color { get; set; }
        public double LineWidth { get; set; }

        public ArrowStyle SourceStyle { get; set; }

        public ArrowStyle TargetStyle { get; set; }

        public bool ShowText { get; set; }


        public void Apply(Edge edge)
        {
            edge.Attr.ArrowheadAtSource = SourceStyle;
            edge.Attr.ArrowheadAtTarget = TargetStyle;
            edge.Attr.LineWidth = LineWidth;
            edge.Attr.Color = Color;
            edge.Label.IsVisible = ShowText;
        }

        public EdgeStyle Clone()
        {
            return new EdgeStyle
            {
                Color = this.Color,
                SourceStyle = this.SourceStyle,
                TargetStyle = this.TargetStyle,
                LineWidth = this.LineWidth,
                ShowText = this.ShowText
            };
        }
    }
}