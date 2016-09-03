using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Engine.Events
{
    public class TriggerManager
    {
        public Dictionary<Vector2, TriggerObject> SpatialTriggers;
        private readonly WorldManager _worldManager;

        public TriggerManager(IEnumerable<TriggerObject> triggers, WorldManager worldManager)
        {
            SpatialTriggers = new Dictionary<Vector2, TriggerObject>();
            _worldManager = worldManager;
            foreach (var trigger in triggers)
            {
                SpatialTriggers[trigger.Position] = trigger;
            }
        }

        public TileEvent Trigger(EntityObject entity, Vector2 position)
        {
            TileEvent toReturn = null;
            if (SpatialTriggers.ContainsKey(position))
            {
                toReturn = new TileEvent();
                var trigger = SpatialTriggers[position];
                toReturn.TriggerEvent += (sender, args) =>
                {
                    Debug.WriteLine("Activate trigger:" + position);
                    var newWorld = trigger.Value;
                    entity.Position = trigger.EndPosition;
                    _worldManager.AddEntity(entity, newWorld);
                };
            }
            return toReturn;
        }

        public TileEvent Trigger(EntityObject entity) => Trigger(entity, entity.Position);
    }
}
