namespace AgileViews.Model
{
    public class Message : Element
    {
        public MessageType Type { get; set; }
        public ExecutionType Execution { get; set; }

        public override Element GetParent()
        {
            return null;
        }
    }
}