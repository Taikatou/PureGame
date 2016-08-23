using System;

namespace PureGame.MessageBus
{
    public interface ISubscriber : IDisposable
    {
        void RecieveMessage(Message m);
    }
}
