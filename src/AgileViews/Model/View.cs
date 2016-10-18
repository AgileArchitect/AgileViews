using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileViews.Model
{
    /// <summary>
    ///     We want different kinds of views
    ///     - A softwareSystem and its context (systems and users)
    ///     - The internals of a softwaresystem (components), with or without relations to externals systems and users
    ///     - The internals of a component (subcomponents, classes, etc, messages), with or without relations to external
    ///     componenets
    /// </summary>
    public class View
    {
        private static Func<View, Relationship, bool> ConnectedNotDangling = (view, relationship) =>
        {
            return view.Elements.Contains(relationship.Source) && view.Elements.Contains(relationship.Target);
        };

        public Func<View, Relationship, bool> RelationshipInclusion { get; private set; } = ConnectedNotDangling;

        public Element Subject;

        internal View(Element subject, ViewType viewType)
        {
            Subject = subject;
            ViewType = viewType;
        }

        public void SetRelationshipStrategy(IRelationshipStrategy strategy)
        {
        }

        public void SetRelationshipStrategy(Func<View, Relationship, bool> shouldInclude)
        {
        }

        public ViewType ViewType { get; private set; }

        internal Model Model { get; set; }

        public string Name => Subject.Name;

        public ICollection<Element> Elements { get; private set; } = new List<Element>();
        
        public ICollection<Relationship> Relationships
        {
            get { return Model.Relationships.Where(r => RelationshipInclusion(this, r)).ToList(); }
        }

        public void AddElements(Func<Element, bool> selector)
        {
            foreach (var e in Model.Elements.Where(selector))
            {
                if (!Elements.Contains(e))
                    Elements.Add(e);
            }
        }

        public void AddSubject()
        {
            Elements.Add(Subject);
        }

        public void AddChildren()
        {
            AddElements(e =>
            {
                var parent = e.GetParent();
                var equality = parent != null && parent.Equals(Subject);
                return equality;
            });
        }
    }
}