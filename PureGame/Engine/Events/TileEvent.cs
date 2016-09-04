using System;

namespace PureGame.Engine.Events
{
    public class TileEvent
    {
        public event EventHandler<EventArgs> TriggerEvent;

        public void Trigger() => TriggerEvent?.Invoke(this, null);
    }
}
