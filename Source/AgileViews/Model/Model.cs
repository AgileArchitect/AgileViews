using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AgileViews.Model
{
    public class Model
    {
        internal Model()
        {
            
        }

        private readonly List<Element> _elements = new List<Element>();

        private readonly List<Relationship> _relationships = new List<Relationship>();

        public IReadOnlyCollection<Relationship> Relationships => new ReadOnlyCollection<Relationship>(_relationships);

        public IReadOnlyCollection<Element> Elements => new ReadOnlyCollection<Element>(_elements);

        public Person AddPerson(string name, string description)
        {
            return AddPerson(name, description, Location.Unknown);
        }

        public Person AddPerson(string name, string description, Location location)
        {
            var person = new Person();
            person.Name = name;
            person.Model = this;
            person.Description = description;

            _elements.Add(person);

            return person;
        }

        public System AddSystem(string name, string description, Location location)
        {
            var system = new System();
            system.Name = name;
            system.Model = this;
            system.Description = description;

            _elements.Add(system);

            return system;
        }

        public Component AddComponent(Container system, string name, string description)
        {
            var component = new Component();
            component.Name = name;
            component.Description = description;
            component.Model = this;

            system.Add(component);
            _elements.Add(component);

            return component;
        }

        internal Relationship AddRelationship(Element source, Element target, string description)
        {
            var relationship = new Relationship()
            {
                Source = source,
                Target = target,
                Label = description
            };

            _relationships.Add(relationship);

            return relationship;
        }


        internal Container AddContainer(System parent, string name, string description)
        {
            var container = new Container();

            container.Name = name;
            container.Description = description;
            container.Parent = parent;
            container.Model = this;

            parent.Add(container);
            _elements.Add(container);

            return container;

        }
    }
}