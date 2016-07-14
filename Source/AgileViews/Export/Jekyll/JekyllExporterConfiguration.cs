namespace AgileViews.Export.Jekyll
{
    public class JekyllExporterConfiguration
    {
        public JekyllExporterConfiguration(string jekyllPath)
        {
            JekyllPath = jekyllPath;
        }

        public string JekyllPath { get; private set; }
    }
}