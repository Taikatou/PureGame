using SmallGame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;

namespace PureGame.Engine
{
    public class WorldArea : GameLevel
    {
        public ContentManager Content;
        public CollisionTiledMap collision_map;
        public CollisionTiledMap CollisionMap
        {
            get
            {
                if(collision_map == null)
                {
                    TiledMap map = Maps[0].GetTiledMap(Content);
                    collision_map = new CollisionTiledMap(map);
                }
                return collision_map;
            }
        }
        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
        }

        public void AddMover(EntityMover mover)
        {
            WorldEntities.movers.Add(mover);
        }

        public List<MapObject> Maps => Objects.GetObjects<MapObject>();

        public List<EntityObject> Entities => Objects.GetObjects<EntityObject>();

        public List<EntityMover> Movers => Objects.GetObjects<EntityMover>();

        public EntityUpdateManager world_entities;

        public EntityUpdateManager WorldEntities
        {
            get
            {
                if(world_entities == null)
                {
                    world_entities = new EntityUpdateManager(Entities, this, Movers);
                }
                return world_entities;
            }
        }

        public void Update(GameTime timer)
        {
            WorldEntities.Update(timer);
        }

        public void UnLoad()
        {
            Content.Unload();
        }
    }
}
