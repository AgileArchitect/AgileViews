using System.Collections.Generic;

namespace AgileViews.Model
{
    public class Container : Element
    {
        public System Parent { get; set; }

        public List<Component> _components = new List<Component>();

        public Component AddComponent(string name, string description)
        {
            return Model.AddComponent(this, name, description);
        }

        public override Element GetParent()
        {
            return Parent;
        }

        internal void Add(Component component)
        {
            _components.Add(component);
        }
    }
}