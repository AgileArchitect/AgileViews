using System.Collections.Generic;

namespace AgileViews.Model
{
    public class System : Element
    {
        internal System()
        {
            
        }

        private IList<Container> _containers = new List<Container>();

        public Location Location { get; set; }
        public override Element GetParent()
        {
            return null;
        }

        public Container AddContainer(string name, string description)
        {
            return Model.AddContainer(this, name, description);
        }

        internal void Add(Container container)
        {
            _containers.Add(container);
        }

    }
}