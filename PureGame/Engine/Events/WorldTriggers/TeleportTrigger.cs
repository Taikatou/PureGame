using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TeleportTrigger<T> : Trigger where T : WorldArea, new()
    {
        public Vector2 EndPosition;
        private readonly IWorldLoader _worldLoader;

        public TeleportTrigger(Vector2 position, Vector2 endPosition, IWorldLoader worldLoader)
        {
            Position = position;
            EndPosition = endPosition;
            _worldLoader = worldLoader;
        }

        public override string ToString()
        {
            return "Teleport trigger at" + Position + "Endpoint " + EndPosition;
        }

        public override TriggerEvent GetTriggerEvent(Entity entity)
        {
            var trigger = new TriggerEvent();
            trigger.Event += (sender, args) =>
            {
                _worldLoader.AddEntity<T>(entity, EndPosition);
            };
            return trigger;
        }
    }
}
