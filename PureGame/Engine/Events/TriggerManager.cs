using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Engine.Events
{
    public class TriggerManager
    {
        private readonly Dictionary<Vector2, TriggerObject> _spatialTriggers;
        private readonly WorldManager _worldManager;

        public TriggerManager(IEnumerable<TriggerObject> triggers, WorldManager worldManager)
        {
            _spatialTriggers = new Dictionary<Vector2, TriggerObject>();
            _worldManager = worldManager;
            foreach (var trigger in triggers)
            {
                _spatialTriggers[trigger.Position] = trigger;
            }
        }

        public TileEvent Trigger(EntityObject entity, Vector2 position)
        {
            var toReturn = new TileEvent(position);
            if (_spatialTriggers.ContainsKey(position))
            {
                var trigger = _spatialTriggers[position];
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
