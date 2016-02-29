using System;
using System.Diagnostics;

namespace AgileViews.Model
{
    [DebuggerDisplay("{UserData}")]
    public class Element<T> : Element
    {
        public Element Parent { get; set; }

        public T UserData { get; set; }
        public override Element GetParent()
        {
            return Parent;
        }
    }

    public abstract class Element : Information
    {
        private Guid _guid = Guid.NewGuid();

        internal Element()
        {
        }

        public Model Model { get; set; }

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