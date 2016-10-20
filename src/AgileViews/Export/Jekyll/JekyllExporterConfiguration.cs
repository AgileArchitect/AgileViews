using System.Linq;
using AgileViews.Model;

namespace AgileViews.Export.Jekyll
{
    public class JekyllExporterConfiguration
    {
        private readonly Workspace _workspace;
        private readonly string _jekyllPath;

        public JekyllExporterConfiguration(Workspace workspace, string jekyllPath)
        {
            _workspace = workspace;
            _jekyllPath = jekyllPath;
        }

        public string Path(View view)
        {
            return System.IO.Path.Combine(System.IO.Path.GetFullPath(_jekyllPath), _workspace.Name, $"{view.Subject.Name}.{view.ViewType}.md");
        }

        public string Permalink(View view)
        {
            return $"/{_workspace.Name}/{view.Subject.Name}/{view.ViewType}/".ToLowerInvariant();
        }
    }
}