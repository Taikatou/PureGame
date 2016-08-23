using System.Collections.Generic;

namespace PureGame.MessageBus
{
    public class Subscription
    {
        private List<ISubscriber> Subscribers;
        public string Name;

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
    }
}
