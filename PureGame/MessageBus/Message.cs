namespace PureGame.MessageBus
{
    public class Message
    {
        public int Code;
        public string Value;

        public Message(int Code, string Value)
        {
            this.Code = Code;
            this.Value = Value;
        }
    }
}
