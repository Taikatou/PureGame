using System;
using System.Collections.Generic;

namespace PureGame.MessageBus
{
    public class Subscription
    {
        private List<ISubscriber> Subscribers;
        public string Name;
        public int Count => Subscribers.Count;

        public Subscription(string Name)
        {
            this.Name = Name;
            Subscribers = new List<ISubscriber>();
        }

        public void Subscribe(ISubscriber s)
        {
            Subscribers.Add(s);
        }

        public void SendMessage(Message m)
        {
            foreach(ISubscriber s in Subscribers)
            {
                s.RecieveMessage(m);
            }
        }

        internal void UnSubscribe(ISubscriber subscriber)
        {
            Subscribers.Remove(subscriber);
        }
    }
}
