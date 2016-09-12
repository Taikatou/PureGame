using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Events.WorldTriggers
{
    public abstract class Trigger
    {
        public Vector2 Position;

        public abstract override string ToString();
        public abstract TriggerEvent GetTriggerEvent(Entity entity);
    }
}
