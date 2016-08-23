namespace PureGame.MessageBus
{
    public interface ISubscriber
    {
        void RecieveMessage(Message m);
    }
}
