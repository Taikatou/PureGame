﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Engine.World.EntityMover;

namespace PureGame
{
    public class PureGame
    {
        public WorldManager WorldManager;
        public EntityMoverManager EntitiyMover;
        public PureGame(ContentManager content)
        {
            WorldManager = new WorldManager();
            EntitiyMover = new EntityMoverManager();
            ContentManagerManager.Instance = new ContentManagerManager(content);
        }

        public void Update(GameTime time)
        {
            WorldManager.Update(time);
            EntitiyMover.Update(time);
        }

        public EntityMover AddEntityMover(IEntity entity, Point endPoint)
        {
            EntityMover toReturn = null;
            var entityMapper = WorldManager.EntityMapper;
            if (entityMapper.ContainsKey(entity))
            {
                var world = entityMapper[entity];
                toReturn = EntitiyMover.AddMover(world, entity, endPoint);
            }
            return toReturn;
        }
    }
}