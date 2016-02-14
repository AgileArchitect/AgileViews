using System.Linq;

namespace DocCoder.Model
{
    public abstract class Element : Information
    {
        internal Element()
        {
            
        }

        public DocCoder.Model.Model Model { get; set; }

        /// <summary>
        /// Element name which will be displayed in diagrams
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Longer description of the element, which can be added in notes or underneath diagrams.
        /// </summary>
        public string Description { get; set; }

        public abstract Element GetParent();

        public Relationship Uses(Element target, string description)
        {
            return Model.AddRelationship(this, target, description);
        }

        public string Alias
        {
            get { return Name.Replace(" ", ""); }
        }
    }
}