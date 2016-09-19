using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World.EntityMover
{
    public class EntityMoverManager
    {
        public readonly Dictionary<IEntity, EntityMover> EntityMoverDict;

        public EntityMoverManager()
        {
            EntityMoverDict = new Dictionary<IEntity, EntityMover>();
        }

        public void AddMover(WorldArea world, IEntity entity, Point endPoint)
        {
            if (EntityMoverDict.ContainsKey(entity))
            {
                EntityMoverDict.Remove(entity);
            }
            var newEntityMover = new EntityMover(world, entity, endPoint);
            EntityMoverDict[entity] = newEntityMover;
        }

        public void Update(GameTime time)
        {
            var toRemoveList = new List<EntityMover>();
            foreach (var entityMover in EntityMoverDict.Values)
            {
                entityMover.Update(time);
                if (entityMover.Complete)
                {
                    toRemoveList.Add(entityMover);
                }
            }
            foreach (var entityMover in toRemoveList)
            {
                EntityMoverDict.Remove(entityMover.Entity);
            }
        }
    }
}
