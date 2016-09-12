using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TriggerManager
    {
        public Dictionary<Vector2, Trigger> SpatialTriggers;
        public IWorldLoader WorldLoader;
        public void AddTrigger(Trigger trigger) => SpatialTriggers[trigger.Position] = trigger;
        public TriggerManager()
        {
            SpatialTriggers = new Dictionary<Vector2, Trigger>();
        }

        public TriggerEvent GetTriggerEvent(Entity entity, Vector2 position)
        {
            TriggerEvent toReturn = null;
            if (SpatialTriggers.ContainsKey(position))
            {
                var trigger = SpatialTriggers[position];
                toReturn = trigger.GetTriggerEvent(entity);
            }
            return toReturn;
        }
    }
}
