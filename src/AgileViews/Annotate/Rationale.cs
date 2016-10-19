using System;

namespace AgileViews.Model
{
    public class Rationale : Attribute
    {
        public Rationale(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}