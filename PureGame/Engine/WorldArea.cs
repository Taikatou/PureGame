using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.Common;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine.EntityData;
using Newtonsoft.Json;

namespace PureGame.Engine
{
    public class WorldArea
    {
        public ContentManager Content;
        public CollisionTiledMap collision_map;
        public CollisionTiledMap CollisionMap
        {
            get
            {
                if(collision_map == null)
                {
                    TiledMap map = Map.GetTiledMap(Content);
                    collision_map = new CollisionTiledMap(map);
                }
                return collision_map;
            }
        }
        public string Name;
        public WorldArea(string world_name, IFileReader reader)
        {
            Name = world_name;
            Content = ContentManagerManager.RequestContentManager();
            string json_string = reader.ReadAllText(world_name);
            Map = JsonConvert.DeserializeObject<MapObject>(json_string);
        }

        public List<IEntity> Entities => EntityManager.Data.Entities;

        public MapObject Map;

        {
            EntityManager.Data.AddEntity(e);
        }

        public EntityUpdateManager entity_manager;

        public EntityUpdateManager EntityManager
        {
            get
            {
                if(entity_manager == null)
                {
                    entity_manager = new EntityUpdateManager(this);
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
