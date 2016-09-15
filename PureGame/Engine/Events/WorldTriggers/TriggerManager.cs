using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Engine.Events.WorldTriggers
{
    public class TriggerManager
    {
        public Dictionary<Point, Trigger> SpatialTriggers;
        public IWorldLoader WorldLoader;
        public void AddTrigger(Trigger trigger) => SpatialTriggers[trigger.Position] = trigger;
        public TriggerManager()
        {
            SpatialTriggers = new Dictionary<Point, Trigger>();
        }

        public TriggerEvent GetTriggerEvent(Entity entity, Point position)
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
