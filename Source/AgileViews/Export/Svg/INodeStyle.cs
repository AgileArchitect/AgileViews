using Microsoft.Msagl.Drawing;

namespace AgileViews.Export.Svg
{
    public interface INodeStyle
    {
        void Apply(Node node);
    }
}