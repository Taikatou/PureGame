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

        public void Trigger(EntityObject entity)
        {
            var position = entity.Position;
            if (_spatialTriggers.ContainsKey(position))
            {
                Debug.WriteLine("Activate trigger:" + position);
                var newWorld = _spatialTriggers[position].Value;
                _worldManager.AddEntity(entity, newWorld);
            }
        }
    }
}
