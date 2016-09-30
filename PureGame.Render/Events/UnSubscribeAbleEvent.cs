using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureGame.Client.Events
{
    public class UnSubscribeAbleEvent : IUnSubscribe
    {
        public EventHandler ChildEvent;
        public EventHandler ParentEvent;
        public UnSubscribeAbleEvent(EventHandler childEvent, EventHandler parentEvent)
        {
            ChildEvent = childEvent;
            ParentEvent = parentEvent;
            ParentEvent += ChildEvent;
        }

        public void UnSubscribe()
        {
            ParentEvent -= ChildEvent;
        }
    }
}
