using System.Collections.Generic;

namespace DocCoder.Model
{
    public interface IRelationshipStrategy
    {
        ICollection<Relationship> GetRelationships(Model model, ICollection<Element> elements);
    }
}