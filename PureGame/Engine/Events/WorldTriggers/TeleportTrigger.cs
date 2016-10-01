using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using System;
using PureGame.Common;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TeleportTrigger<T> : Trigger, IDisposable, IUnSubscribe where T : WorldArea, new()
    {
        public Point EndPosition;
        private readonly IWorldLoader _worldLoader;
        private readonly TriggerEvent _trigger;
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
            UnSubscribe();
        }

        public void UnSubscribe()
        {
            _trigger.Event -= TriggerHandler;
        }
    }
}
