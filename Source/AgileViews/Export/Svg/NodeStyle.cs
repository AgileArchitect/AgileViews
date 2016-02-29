using Microsoft.Msagl.Drawing;

namespace AgileViews.Export.Svg
{
    public class NodeStyle : INodeStyle
    {
        /// <summary>
        /// Space within the node around the label
        /// </summary>
        public int Margin { get; set; }
        public double Radius { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }
        public double BorderWidth { get; set; }

        public Shape Shape { get; set; }
        public void Apply(Node node)
        {
            node.Attr.LabelMargin = Margin;
            node.Attr.FillColor = FillColor;
            node.Attr.Shape = Shape;
            node.Attr.XRadius = Radius;
            node.Attr.YRadius = Radius;
            node.Attr.LineWidth = BorderWidth;
        }

        public NodeStyle Clone()
        {
            return new NodeStyle
            {
                Shape = this.Shape,
                FillColor = this.FillColor,
                BorderWidth = this.BorderWidth,
                Margin = this.Margin,
                BorderColor = this.BorderColor,
                Radius = this.Radius
            };
        }
    }
}