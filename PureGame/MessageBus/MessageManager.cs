using System.Collections.Generic;

namespace PureGame.MessageBus
{
    public class MessageManager
    {
        protected static MessageManager _instance;
        public static MessageManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MessageManager();
                }
                return _instance;
            }
        }

        protected Dictionary<string, Subscription> Subscriptions;

        public MessageManager()
        {
            Subscriptions = new Dictionary<string, Subscription>();
        }

        protected Subscription GetSubscription(string subscriptionName)
        {
            if (!Subscriptions.ContainsKey(subscriptionName))
            {
                Subscriptions[subscriptionName] = new Subscription(subscriptionName);
            }
            return Subscriptions[subscriptionName];
        }

        public void SendMessage(string subscriptionName, Message m)
        {
            GetSubscription(subscriptionName).SendMessage(m);
        }

        public void Subscribe(string subscriptionName, ISubscriber subscriber)
        {
            GetSubscription(subscriptionName).Subscribe(subscriber);
        }

        public void UnSubscribe(string subscriptionName, ISubscriber subscriber)
        {
            Subscription Sub = GetSubscription(subscriptionName);
            Sub.UnSubscribe(subscriber);
            if(Sub.Count == 0)
            {
                Subscriptions.Remove(subscriptionName);
            }
        }
    }
}
