namespace AgileViews.Model
{
    public class Person : Element
    {
        internal Person()
        {
            
        }

        public Location Location { get; set; }
        public override Element GetParent()
        {
            return null;
        }
    }
}