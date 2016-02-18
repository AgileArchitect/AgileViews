using System.Collections.Generic;

namespace AgileViews.Model
{
    public interface IRelationshipStrategy
    {
        ICollection<Relationship> GetRelationships(Model model, ICollection<Element> elements);
    }
}