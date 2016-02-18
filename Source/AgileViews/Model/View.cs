using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileViews.Model
{
    /// <summary>
    /// We want different kinds of views
    /// - A softwareSystem and its context (systems and users)
    /// - The internals of a softwaresystem (components), with or without relations to externals systems and users
    /// - The internals of a component (subcomponents, classes, etc, messages), with or without relations to external componenets
    /// </summary>
    public class View
    {
        public ViewType ViewType { get; set; }

         readonly IRelationshipStrategy _strategy;
        internal Model Model { get; set; }

        public Element Subject;

        internal View(Element subject, IRelationshipStrategy strategy)
        {
            _strategy = strategy;
            Subject = subject;
            Elements = new List<Element>();
        }

        public string Name => Subject.Name;

        public ICollection<Element> Elements { get; set; }

        public ICollection<Relationship> Relationships
        {
            get { return _strategy.GetRelationships(Model, Elements); }
        }

        public void AddElements(Func<Element, bool> selector)
        {
            foreach (var e in Model.Elements.Where(selector))
            {
                if( !Elements.Contains(e))
                    Elements.Add(e);
            }
        }

        public void AddAllPeople()
        {
            AddElements(e => e is Person);
        }

        public void AddAllSystems()
        {
            AddElements(e => e is System);
        }

        public void AddChildren()
        {
            AddElements(e =>
            {
                var parent = e.GetParent();
                return parent != null && parent.Equals(Subject);
            });
        }
    }
}