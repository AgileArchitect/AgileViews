using System.Diagnostics;

namespace AgileViews.Model
{
    [DebuggerDisplay("{Source?.QualifiedName ?? SourceRole} -> {Target?.QualifiedName ?? TargetName}")]
    public class Relationship : Information
    {
        public Element Source { get; set; }
        public Element Target { get; set; }

        public string Label { get; set; }

        public string SourceName { get; set; }
        public string TargetName { get; set; }
    }
}