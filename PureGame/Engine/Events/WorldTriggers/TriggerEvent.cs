using System;
using System.Diagnostics;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TriggerEvent
    {
        public EventHandler Event;

        public void OnTrigger()
        {
            Debug.WriteLine("Invoking trigger set to: " + (Event != null));
            Event?.Invoke(this, EventArgs.Empty);
        }
    }
}
