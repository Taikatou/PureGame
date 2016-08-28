using System.Collections.Generic;

namespace PureGame.MessageBus
{
    public class Subscription
    {
        private readonly List<ISubscriber> _subscribers;
        public string Name;
        public int Count => _subscribers.Count;

        public Subscription(string name)
        {
            Name = name;
            _subscribers = new List<ISubscriber>();
        }

        public void Subscribe(ISubscriber s)
        {
            _subscribers.Add(s);
        }

        public void SendMessage(Message m)
        {
            foreach(ISubscriber s in _subscribers)
            {
                s.RecieveMessage(m);
            }
        }

        internal void UnSubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }
    }
}
