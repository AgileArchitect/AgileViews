using System;
using System.Linq;
using System.Reflection;
using AgileViews.Model;
using AgileViews.Scrape;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            ReflectionProcessor rp = new ReflectionProcessor();
            var model = new Workspace("Test").GetModel();
            rp.Process(typeof(Tests).GetTypeInfo().Assembly, model);
            Assert.True(model.Elements.Any());
        }
    }
}
