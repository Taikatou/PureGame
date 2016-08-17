using SmallGame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine.EntityData;

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

        public List<MapObject> Maps => Objects.GetObjects<MapObject>();

        public List<EntityObject> Entities => EntityManager.Data.Entities;

        public EntityUpdateManager entity_manager;

        public EntityUpdateManager EntityManager
        {
            get
            {
                if(entity_manager == null)
                {
                    List<EntityObject> e = Objects.GetObjects<EntityObject>();
                    entity_manager = new EntityUpdateManager(e, this);
                }
                return entity_manager;
            }
        }

        public void Update(GameTime timer)
        {
            EntityManager.Update(timer);
        }

        public void UnLoad()
        {
            Content.Unload();
        }
    }
}
