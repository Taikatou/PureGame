using SmallGame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
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
            WorldEntities.Movers.Add(mover);
        }

        public List<MapObject> Maps => Objects.GetObjects<MapObject>();

        public List<EntityObject> Entities => WorldEntities.Data.Entities;

        public List<EntityMover> Movers => WorldEntities.Movers;

        public EntityUpdateManager world_entities;

        public EntityUpdateManager WorldEntities
        {
            get
            {
                if(world_entities == null)
                {
                    List<EntityObject> e = Objects.GetObjects<EntityObject>();
                    world_entities = new EntityUpdateManager(e, this);
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
