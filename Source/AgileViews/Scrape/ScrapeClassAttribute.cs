using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using AgileViews.Model;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AgileViews.Scrape
{
    /// <summary>
    /// Maybe do this on the fly when the information is requested? It could hold a reference to the compilation.
    /// </summary>
    public class ScrapeClassAttribute
    {
        public void Scape(Model.Model model, string information, string attribute, int paramIndex)
        {
            foreach (var element in model.Elements.OfType<Element<ClassDeclarationSyntax>>())
            {
                foreach (var list in element.UserData.AttributeLists)
                {
                    foreach (var a in list.Attributes)
                    {
                        Console.WriteLine("Hello");
                    }
                }
            }
        }
    }
}
