using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using System;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TeleportTrigger<T> : Trigger, IDisposable where T : WorldArea, new()
    {
        public Point EndPosition;
        private readonly IWorldLoader _worldLoader;
        private TriggerEvent _trigger;
        public EventHandler TriggerHandler;

        public TeleportTrigger(Point position, Point endPosition, IWorldLoader worldLoader)
        {
            Position = position;
            EndPosition = endPosition;
            _worldLoader = worldLoader;
            _trigger = new TriggerEvent();
        }

        public override string ToString()
        {
            return "Teleport trigger at" + Position + "Endpoint " + EndPosition;
        }

        public override TriggerEvent GetTriggerEvent(IEntity entity)
        {
            TriggerHandler = (sender, args) =>
            {
                _worldLoader.AddEntity<T>(entity, EndPosition);
            };
            _trigger.Event += TriggerHandler;
            return _trigger;
        }

        public void Dispose()
        {
            _trigger.Event -= TriggerHandler;
        }
    }
}
