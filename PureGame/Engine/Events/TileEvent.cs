using System;
using Microsoft.Xna.Framework;

namespace PureGame.Engine.Events
{
    public class TileEvent
    {
        public Vector2 Position;
        public event EventHandler<EventArgs> TriggerEvent;

        public TileEvent(Vector2 position)
        {
            Position = position;
        }

        public void Trigger() => TriggerEvent?.Invoke(this, null);
    }
}
