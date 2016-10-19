using System;

namespace AgileViews.Annotate
{
    [AttributeUsage(AttributeTargets.All)]
    public class TechnologyAttribute : Attribute
    {
        public readonly string Technology;

        public TechnologyAttribute(string technology)
        {
            Technology = technology;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ExternalAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public readonly string Description;

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }

    public class Url : Attribute
    {
        public Url(string href, string name)
        {
            
        }
    }
}
