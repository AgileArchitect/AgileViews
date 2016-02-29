using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AgileViews.Model;

namespace AgileViews.Scrape
{
    public class PackageAnalyser
    {
        private Assembly _assembly;

        public PackageAnalyser(string pathToAssembly)
        {
            _assembly = Assembly.LoadFile(pathToAssembly);
        }

        public PackageAnalyser(Assembly assembly)
        {
            _assembly = assembly;
        }

        public ICollection<Element<Type>> GetTypes()
        {
            return _assembly.GetTypes().Select(t => new Element<Type>()
            {
                Name = t.Name,
                UserData = t
            }).ToList();
        }
    }
}
