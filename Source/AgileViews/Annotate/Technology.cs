using System;

namespace AgileViews.Annotate
{
    [AttributeUsage(AttributeTargets.All)]
    public class TechnologyAttribute : Attribute
    {
        public string Technology { get; }

        public TechnologyAttribute(string technology)
        {
            Technology = technology;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ExternalAttribute : Attribute
    {

    }
}
