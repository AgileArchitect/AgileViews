using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AgileViews.Model;
using AgileViews.Scrape;
using Xunit;

namespace Tests
{

    public class ReflectionProcessorTests
    {
        [Fact]
        public void CanFindFilteredTypesAndProperties() 
        {
            ReflectionProcessor rp = new ReflectionProcessor(config =>
            {
                config.InclusionFilter = t => t.GetTypeInfo().IsSubclassOf(typeof(Base));
            });

            var model = new Workspace("Test").GetModel();
            rp.Process(typeof(ReflectionProcessorTests).GetTypeInfo().Assembly, model);

            Assert.True(model.Elements.Count == 2);
            Assert.True(model.Relationships.Count == 1);
            
            Assert.True(model.Relationships.All(r => r.Source != null && r.Target != null));

            Assert.True(model.Relationships.FirstOrDefault().Label == "TheB");
            Assert.Equal("Tests.A", model.Relationships.FirstOrDefault().SourceName);
            Assert.Equal("Tests.B", model.Relationships.FirstOrDefault().TargetName);
        }

        [Fact]
        public void CustomLabelerCanStillIdentifyElements()
        {
            ReflectionProcessor rp = new ReflectionProcessor(config =>
            {
                config.InclusionFilter = t => t.GetTypeInfo().IsSubclassOf(typeof(Base));
            });

            var model = new Workspace("Test").GetModel();
            rp.Process(typeof(ReflectionProcessorTests).GetTypeInfo().Assembly, model);

            Assert.True(model.Elements.Count == 2);
            Assert.True(model.Relationships.Count == 1);
        }

    }

    public class GuidTypeLabeler : ILabel
    {
        private readonly Type _type;
        public static Dictionary<Type, Guid> _guids = new Dictionary<Type, Guid>();

        public GuidTypeLabeler(Type type)
        {
            _type = type;

            if( !_guids.ContainsKey(type))
                _guids.Add(type, Guid.NewGuid());
        }

        public string Name => _type.Name;

        public string QualifiedName => _guids[_type].ToString();
    }

    public class A : Base
    {
        public B TheB { get; set; }
    }

    public class B : Base
    {
        
    }

    public abstract class Base
    {
        
    }
}
