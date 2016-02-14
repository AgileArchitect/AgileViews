namespace DocCoder.Model
{
    public class Component : Element
    {
        internal Component()
        {
            
        }

        public Container Parent { get; set; }
        public override Element GetParent()
        {
            return Parent;
        }
    }
}