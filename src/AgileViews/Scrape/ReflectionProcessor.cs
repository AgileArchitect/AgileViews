using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AgileViews.Annotate;
using AgileViews.Extensions;
using AgileViews.Model;

namespace AgileViews.Scrape
{
    public static class ExtensionsToReflectionProcessor
    {
        public static ReflectionProcessor Default(this ReflectionProcessor r)
        {
            r.ValueAttributeFromFactory(Information.Kind, t => t.GetTypeInfo().BaseType.Name.ToLowerInvariant());
            r.TagAttributeFromAttribute<ExternalAttribute>(Information.External);
            r.ValueAttributeFromAttribute<DescriptionAttribute>(Information.Description, d => d.Description);
            r.ValueAttributeFromAttribute<TechnologyAttribute>(Information.Technology, t => t.Technology);

            return r;
        }
    }

    public class ReflectionProcessor
    {
        public Func<Type,ILabel> TypeLabeler = t => new TypeLabel(t);

        public Func<PropertyInfo, ILabel> PropertyLabeler = p => new StringLabel(p.Name);

        public Predicate<Type> InclusionFilter = t => true;

        public ReflectionProcessor(Action<ReflectionProcessor> config)
        {
            config(this);
        }

        public ReflectionProcessor()
        {
        }

        private List<Action<Type, Element>> _classProcessors = new List<Action<Type, Element>>();

        private List<Action<PropertyInfo, Relationship>> _propertyProcessors = new List<Action<PropertyInfo, Relationship>>();

        private T[] GetAttributes<T>(Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>().ToArray();
        }

        private T[] GetAttributes<T>(PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttributes<T>().ToArray();
        }

        public void TagAttributeFromAttribute<T>(string attribute) where T : Attribute
        {
            Action<Type, Element> action = (t, e) =>
            {
                if (GetAttributes<T>(t).Any())
                {
                    e.Add(attribute, string.Empty);
                }
            };
            _classProcessors.Add(action);
        }

        public void ValueAttributeFromFactory(string attribute, Func<Type, object> valueFunc)
        {
            Action<Type, Element> classAction = (t, e) =>
            {
                e.Add(attribute, valueFunc(t).ToString());
            };
            _classProcessors.Add(classAction);
        }

        public void ValueAttributeFromAttribute<T>(string attribute, Func<T, object> valueFunc) where T : Attribute
        {
            Action<Type, Element> classAction = (t, e) =>
            {
                var attrubutes = GetAttributes<T>(t);
                foreach (var a in attrubutes)
                {
                    e.Add(attribute, valueFunc(a).ToString());
                }
            };
            _classProcessors.Add(classAction);

            Action<PropertyInfo, Relationship> elementAction = (p, r) =>
            {
                foreach (var a in GetAttributes<T>(p))
                {
                    r.Add(attribute, valueFunc(a).ToString());
                }
            };
            _propertyProcessors.Add(elementAction);
        }

        public void BooleanAttributeFromPredicate(string attribute, Predicate<Type> check)
        {
            Action<Type, Element> action = (t, e) =>
            {
                if( check(t))
                    e.Add(attribute, true);
            };

            _classProcessors.Add(action);
        }

        public void Process(Assembly assembly, Model.Model model)
        {
            assembly.ExportedTypes.Where(t => InclusionFilter(t)).ForEach(t =>
            {
                var element = new Element(TypeLabeler(t));
                model.Add(element);

                foreach (var p in _classProcessors)
                {
                    p(t, element);
                }

                foreach (var property in t.GetRuntimeProperties())
                {
                    var type = property.PropertyType;
                    var name = property.Name;
                    var r = new Relationship
                    {
                        SourceName = TypeLabeler(t).QualifiedName,
                        Label = name,
                        TargetName = TypeLabeler(property.PropertyType).QualifiedName
                    };
                    model.Add(r);

                    foreach (var pa in _propertyProcessors)
                    {
                        pa(property, r);
                    }
                }
            });

            model.ResolveNodes();
        }
    }

    public class TypeLabel : ILabel
    {
        private readonly Type _type;

        public static explicit operator TypeLabel(Type type)
        {
            return new TypeLabel(type);
        }

        public TypeLabel(Type type)
        {
            _type = type;
        }

        public string Name => _type.Name;
        public string QualifiedName => _type.FullName;
    }
}