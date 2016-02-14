using System.Collections.Generic;
using System.Linq;

namespace DocCoder.Model
{
    public class AllRelationshipStrategy : IRelationshipStrategy
    {
        public ICollection<Relationship> GetRelationships(Model model, ICollection<Element> elements)
        {
            return model.Relationships.Where(r => elements.Contains(r.Source) && elements.Contains(r.Target)).ToList();
        }
    }
}