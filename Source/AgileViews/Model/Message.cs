namespace AgileViews.Model
{
    public class Message : Element
    {
        public override Element GetParent()
        {
            return null;
        }

        public MessageType Type { get; set; }
        public ExecutionType Execution { get; set; }

    }
}
