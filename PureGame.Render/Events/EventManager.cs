using System;
using System.Collections.Generic;

namespace PureGame.Client.Events
{
    public class EventManager : IUnSubscribe
    {
        public List<UnSubscribeAbleEvent> Subscribers;

        public EventManager()
        {
            Subscribers = new List<UnSubscribeAbleEvent>();
        }

        public void AddEvent(EventHandler childEvent, EventHandler parentEvent)
        {
            var unSubscribeAbleEvent = new UnSubscribeAbleEvent(childEvent, parentEvent);
            Subscribers.Add(unSubscribeAbleEvent);
        }

        public void UnSubscribe()
        {
            foreach(var unSubscribeAble in Subscribers)
            {
                unSubscribeAble.UnSubscribe();
            }
        }
    }
}
