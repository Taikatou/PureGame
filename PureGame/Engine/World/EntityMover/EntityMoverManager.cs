using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World.EntityMover
{
    public class EntityMoverManager
    {
        private readonly Dictionary<IEntity, EntityMover> _entityMoverDict;

        public EntityMoverManager()
        {
            _entityMoverDict = new Dictionary<IEntity, EntityMover>();
        }

        public EntityMover AddMover(WorldArea world, IEntity entity, Point endPoint)
        {
            if (_entityMoverDict.ContainsKey(entity))
            {
                _entityMoverDict.Remove(entity);
            }
            var newEntityMover = new EntityMover(world, entity, endPoint);
            _entityMoverDict[entity] = newEntityMover;
            return _entityMoverDict[entity];
        }

        public void Update(GameTime time)
        {
            var toRemoveList = new List<EntityMover>();
            foreach (var entityMover in _entityMoverDict.Values)
            {
                entityMover.Update(time);
                if (entityMover.Complete)
                {
                    toRemoveList.Add(entityMover);
                }
            }
            foreach (var entityMover in toRemoveList)
            {
                _entityMoverDict.Remove(entityMover.Entity);
            }
        }
    }
}
