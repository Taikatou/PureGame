using System.Collections.Generic;

namespace PureGame.MessageBus
{
    public class MessageManager
    {
        protected static MessageManager instance;
        public static MessageManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new MessageManager();
                }
                return instance;
            }
        }

        protected Dictionary<string, Subscription> Subscriptions;

        public MessageManager()
        {
            Subscriptions = new Dictionary<string, Subscription>();
        }

        protected Subscription GetSubscription(string SubscriptionName)
        {
            if (!Subscriptions.ContainsKey(SubscriptionName))
            {
                Subscriptions[SubscriptionName] = new Subscription(SubscriptionName);
            }
            return Subscriptions[SubscriptionName];
        }

        public void SendMessage(string SubscriptionName, Message m)
        {
            GetSubscription(SubscriptionName).SendMessage(m);
        }

        public void Subscribe(string SubscriptionName, ISubscriber subscriber)
        {
            GetSubscription(SubscriptionName).Subscribe(subscriber);
        }
    }
}
