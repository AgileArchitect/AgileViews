namespace DocCoder.Export
{
    

    public class JekyllExporterConfiguration
    {

        public string JekyllPath { get; private set; }
        public JekyllExporterConfiguration(string jekyllPath)
        {
            JekyllPath = jekyllPath;
        }
    }
}