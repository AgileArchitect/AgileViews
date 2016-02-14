namespace DocCoder.Model
{
    public class Relationship : Information
    {
        public Element Source { get; set; }
        public string SourceRole { get; set; }

        public Element Target { get; set; }
        public string TargetRole { get; set; }

        public string Label { get; set; }
    }
}