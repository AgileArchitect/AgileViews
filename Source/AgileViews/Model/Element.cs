using System;
using System.Diagnostics;

namespace AgileViews.Model
{
    [DebuggerDisplay("{QualifiedName}")]
    public class Element<T> : Element
    {
        public Element(string name) : base(name)
        {
            
        }

        public Element Parent { get; set; }

        public T UserData { get; set; }

    }

    public class Element<T1, T2> : Element<T1>
    {
        public Element(string name) : base(name)
        {

        }

        public T2 UserData2 { get; set; }
    }

    public class Element : Information
    {
        private Guid _guid = Guid.NewGuid();
        private string _name;

        public Element(string name)
        {
            Name = name;
            QualifiedName = name;
        }

        public Model Model { get; set; }

        /// <summary>
        ///     Element name which will be displayed in diagrams
        /// </summary>
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        public string QualifiedName { get; set; }

        /// <summary>
        ///     Longer description of the element, which can be added in notes or underneath diagrams.
        /// </summary>
        public string Description { get; set; }

        public string Alias => QualifiedName.Replace(" ", "");

//        public Element GetParent();

        public Relationship Uses(Element target, string description)
        {
            return Model.AddRelationship(this, target, description);
        }
    }
}