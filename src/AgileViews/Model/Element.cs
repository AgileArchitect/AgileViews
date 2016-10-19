using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace AgileViews.Model
{
    public class StringLabel : ILabel
    {
        private readonly string _label;

        public StringLabel(string label)
        {
            _label = label;
        }

        public string Name { get { return _label; } }

        public string QualifiedName
        {
            get { return _label; }
        }

        public static implicit operator StringLabel(string name)
        {
            StringLabel l = new StringLabel(name);
            return l;
        }
    }

    public interface ILabel
    {
        string Name { get; }
        string QualifiedName { get; }
    }

    [DebuggerDisplay("{QualifiedName}")]
    public class Element<T> : Element
    {
        public Element(ILabel label) : base(label)
        {
        }

        public T UserData { get; set; }

    }

    public class Element : Information
    {
        private readonly ILabel _label;
        private Guid _guid = Guid.NewGuid();
        private string _name;

        public Element(ILabel label)
        {
            _label = label;
        }

        /// <summary>
        /// This is more of an alias or displayname.
        /// </summary>
        public string Name => _label.Name;

        /// <summary>
        /// Name we will use to identify and search for elements.
        /// </summary>
        public string QualifiedName => _label.QualifiedName;

        public Model Model { get; set; }


        public string Alias => QualifiedName.Replace(" ", "");

//        public Element GetParent();

        public Relationship Uses(Element target, string description)
        {
            return Model.AddRelationship(this, target, description);
        }
    }
}