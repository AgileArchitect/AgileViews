using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AgileViews.Model
{
    [Rationale("Where model information is kept.")]
    public class Model
    {
        private readonly List<Element> _elements = new List<Element>();

        private readonly List<Relationship> _relationships = new List<Relationship>();

        internal Model()
        {
        }

        public IReadOnlyCollection<Relationship> Relationships => new ReadOnlyCollection<Relationship>(_relationships);

        public IReadOnlyCollection<Element> Elements => new ReadOnlyCollection<Element>(_elements);

        public Element ElementByName(string name)
        {
            return _elements.SingleOrDefault(e => e.QualifiedName == name);
        }

        public void AddAll(IEnumerable<Element> elements)
        {
            _elements.AddRange(elements);
        }

        public void Add(Element element)
        {
            element.Model = this;
            _elements.Add(element);
        }

        public void AddAll(IEnumerable<Relationship> relationships)
        {
            _relationships.AddRange(relationships);
        }

        public void Add(Relationship relationship)
        {
            _relationships.Add(relationship);
        }

        public void ResolveNodes()
        {
            var doublesNames =
                _elements.Select(e => e.QualifiedName)
                    .GroupBy(e => e)
                    .Where(g => g.Count() > 1)
                    .ToDictionary(g => g.Key, g => g.ToList());

            var doubles = _elements.Where(e => doublesNames.ContainsKey(e.Name)).ToList();

            if (doublesNames.Any())
            {
                throw new ArgumentException("You have double names");
            }
            var nodeMap = _elements.ToDictionary(n => n.Alias, n => n);

            foreach (var rel in _relationships.Where(r => r.Source == null || r.Target == null))
            {
                if (rel.Source == null && nodeMap.ContainsKey(rel.SourceName))
                    rel.Source = nodeMap[rel.SourceName];

                if (rel.Target == null && nodeMap.ContainsKey(rel.TargetName))
                    rel.Target = nodeMap[rel.TargetName];
            }
        }

        internal Relationship AddRelationship(Element source, Element target, string description)
        {
            var relationship = new Relationship
            {
                Source = source,
                Target = target,
                Label = description
            };

            _relationships.Add(relationship);

            return relationship;
        }
    }
}