using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using MonoGame.Extended.Maps.Tiled;

namespace PureGame.Engine
{
    public class EntityUpdateManager
    {
        public List<EntityObject> entities;
        private List<ExpiringKey<Vector2>> expiring_tiles;
        private Dictionary<ExpiringKey<Vector2>, EntityObject> KeyToEntity;
        public Dictionary<EntityObject, ExpiringKey<Vector2>> EntityToKey;
        public Dictionary<string, EntityObject> id_hash;
        public Dictionary<Vector2, EntityObject> spatial_hash;
        public List<EntityMover> movers;
        private WorldArea parent;

        public EntityUpdateManager(List<EntityObject> entities, WorldArea parent, List<EntityMover> movers)
        {
            this.movers = movers;
            this.entities = entities;
            this.parent = parent;
            id_hash = new Dictionary<string, EntityObject>();
            expiring_tiles = new List<ExpiringKey<Vector2>>();
            spatial_hash = new Dictionary<Vector2, EntityObject>();
            EntityToKey = new Dictionary<EntityObject, ExpiringKey<Vector2>>();
            KeyToEntity = new Dictionary<ExpiringKey<Vector2>, EntityObject>();
            foreach (EntityObject e in this.entities)
            {
                id_hash[e.Id] = e;
                spatial_hash[e.Position] = e;
            }
        }

        public void AddEntity(EntityObject e)
        {
            if(!(spatial_hash.ContainsKey(e.Position) || id_hash.ContainsKey(e.Id)))
            {
                entities.Add(e);
                spatial_hash[e.Position] = e;
                id_hash[e.Id] = e;
            }
        }

        public void Update(GameTime timer)
        {
            Vector2 previous_position;
            foreach(EntityMover m in movers)
            {
                if(!EntityToKey.ContainsKey(m.Entity))
                {
                    previous_position = m.Position;
                    m.Update(timer);
                    if (previous_position != m.Position)
                    {
                        if (ValidPosition(m.Position))
                        {
                            var movement_key = new ExpiringKey<Vector2>(previous_position, m.Speed);
                            expiring_tiles.Add(movement_key);
                            EntityToKey[m.Entity] = movement_key;
                            KeyToEntity[movement_key] = m.Entity;
                            spatial_hash[m.Position] = m.Entity;
                        }
                        else
                        {
                            m.Position = previous_position;
                        }
                    }
                }
            }
            for (int i = 0; i < expiring_tiles.Count; i++)
            {
                expiring_tiles[i].Update(timer);
                if(expiring_tiles[i].TimeLeft <= 0)
                {
                    Vector2 position = expiring_tiles[i].Key;
                    Debug.WriteLine("Remove tile : " + position);
                    var EntitiyObject = KeyToEntity[expiring_tiles[i]];
                    spatial_hash.Remove(position);
                    EntityToKey.Remove(EntitiyObject);
                    KeyToEntity.Remove(expiring_tiles[i]);
                    expiring_tiles.RemoveAt(i);
                }
            }
        }

        private bool ValidPosition(Vector2 position)
        {
            bool within_limits = position.X >= 0 && position.Y >= 0 &&
                                 position.X < parent.CollisionMap.TiledMap.Width &&
                                 position.Y < parent.CollisionMap.TiledMap.Height;
            bool entity_collision = !spatial_hash.ContainsKey(position);
            bool map_collision = !parent.CollisionMap.CheckCollision(position);
            Debug.WriteLine("Map collision: " + map_collision);
            return within_limits && entity_collision && map_collision;
        }
    }
}
