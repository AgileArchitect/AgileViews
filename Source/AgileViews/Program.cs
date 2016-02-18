using System;
using System.Linq;

namespace AgileViews
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ws = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            var soln = ws.OpenSolutionAsync(@"D:\Projects\pipeline\Source\Pipeline.sln").Result;

            var listOfListofCsFiles = soln.Projects.Select(p => p.Documents.Where(d => d.Name.EndsWith(".cs")));

            var proj = soln.Projects.Select(p => p.Name);
            foreach (var l in listOfListofCsFiles)
            {
                foreach (var ll in l)
                {
                    foreach (var lll in l)
                    {
                        Console.WriteLine(lll.Name);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
