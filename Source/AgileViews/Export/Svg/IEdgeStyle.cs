using Microsoft.Msagl.Drawing;

namespace AgileViews.Export.Svg
{
    public interface IEdgeStyle
    {
        void Apply(Edge edge);
    }
}