using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Engine.World.EntityMover;

namespace PureGame
{
    public class PureGame
    {
        public WorldManager WorldManager;
        public EntityMoverManager EntitiyMover;
        public PureGame()
        {
            WorldManager = new WorldManager();
            EntitiyMover = new EntityMoverManager();
        }

        public void Update(GameTime time)
        {
            WorldManager.Update(time);
            EntitiyMover.Update(time);
        }

        public void AddEntityMover(IEntity entity, Point endPoint)
        {
            var entityMapper = WorldManager.EntityMapper;
            if (entityMapper.ContainsKey(entity))
            {
                var world = entityMapper[entity];
                EntitiyMover.AddMover(world, entity, endPoint);
            }
        }
    }
}