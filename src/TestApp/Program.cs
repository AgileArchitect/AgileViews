using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AgileViews.Model;
using AgileViews.Scrape;

namespace TestApp
{
    public class Program
    {
        public static bool Included(Type t)
        {
            return t.Name != "Program";
        }

        public static void Main()
        {
            var workspace = new Workspace("test");
            var model = workspace.GetModel();

            new ReflectionProcessor(r => r.InclusionFilter = Included)
                .Default()
                .Process(typeof(Program).GetTypeInfo().Assembly, model);

            workspace.CreateView(model.ElementByName(typeof(Program).FullName), ViewType.Context)
                .AddElements(e => true);

            workspace.Export(".");
        }
    }

    public class Test1
    {
        public Test2 Hallo { get; set; }
    }

    public class Test2
    {
        
    }

    public class MyLabeler : ILabel
    {
        private static Dictionary<Type,Guid> _dict = new Dictionary<Type, Guid>();

        private readonly Type _type;

        public MyLabeler(Type type)
        {
            _type = type;
        }

        public string Name { get; }
        public string QualifiedName { get; }
    }
}
