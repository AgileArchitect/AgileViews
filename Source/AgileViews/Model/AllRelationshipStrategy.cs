using System.Collections.Generic;
using System.Linq;

namespace AgileViews.Model
{
    public class AllRelationshipStrategy : IRelationshipStrategy
    {
        public ICollection<Relationship> GetRelationships(Model model, ICollection<Element> elements)
        {
            return
                model.Relationships.Where(
                    r =>
                        r.Source != null && r.Target != null && elements.Contains(r.Source) &&
                        elements.Contains(r.Target)).ToList();
        }
    }
}