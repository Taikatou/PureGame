using System.Collections.Generic;
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

        public void AddMover(WorldArea world, IEntity entity, Point endPoint, bool running)
        {
            if (_entityMoverDict.ContainsKey(entity))
            {
                _entityMoverDict.Remove(entity);
            }
            var newEntityMover = new EntityMover(world, entity, endPoint, running);
            _entityMoverDict[entity] = newEntityMover;
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
