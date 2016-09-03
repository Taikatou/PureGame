using Microsoft.Xna.Framework;
using System.Collections.Generic;
using PureGame.Engine.Events;
using PureGame.Engine.World;

namespace PureGame.Engine.EntityData
{
    public class EntityManager
    {
        public List<ExpiringKey<TileEvent>> ExpiringTiles;
        public Dictionary<ExpiringKey<TileEvent>, EntityObject> KeyToEntity;
        public Dictionary<EntityObject, ExpiringKey<TileEvent>> EntityToKey;
        public Dictionary<Vector2, EntityObject> SpatialHash;
        public Dictionary<string, EntityObject> IdHash;
        public List<EntityObject> Entities;

        public EntityManager(IEnumerable<EntityObject> entities)
        {
            IdHash = new Dictionary<string, EntityObject>();
            Entities = new List<EntityObject>();
            ExpiringTiles = new List<ExpiringKey<TileEvent>>();
            EntityToKey = new Dictionary<EntityObject, ExpiringKey<TileEvent>>();
            KeyToEntity = new Dictionary<ExpiringKey<TileEvent>, EntityObject>();
            SpatialHash = new Dictionary<Vector2, EntityObject>();
            foreach (var entity in entities)
            {
                AddEntity(entity);
            }
        }

        public void AddEntity(EntityObject e)
        {
            if (!ContainsEntity(e))
            {
                Entities.Add(e);
                SpatialHash[e.Position] = e;
                IdHash[e.Id] = e;
            }
        }

        public void RemoveEntity(EntityObject entity) => RemoveEntity(entity.Id);

        public void RemoveEntity(string entityId)
        {
            if (IdHash.ContainsKey(entityId))
            {
                var entity = IdHash[entityId];
                // start removing
                IdHash.Remove(entityId);
                EntityToKey.Remove(entity);
                SpatialHash.Remove(entity.Position);
                Entities.Remove(entity);
            }
        }

        public void AddEntityKey(EntityObject e, ExpiringKey<TileEvent> key)
        {
            ExpiringTiles.Add(key);
            EntityToKey[e] = key;
            KeyToEntity[key] = e;
            SpatialHash[e.Position] = e;
        }

        public void Update(GameTime time)
        {
            for (var i = 0; i < ExpiringTiles.Count; i++)
            {
                ExpiringTiles[i].Update(time);
                if (ExpiringTiles[i].TimeLeft <= 0)
                {
                    RemoveTile(i);
                }
            }
        }

        public void RemoveTile(int i)
        {
            var position = ExpiringTiles[i].Key.Position;
            var entitiy = KeyToEntity[ExpiringTiles[i]];
            SpatialHash.Remove(position);
            EntityToKey.Remove(entitiy);
            KeyToEntity.Remove(ExpiringTiles[i]);
            ExpiringTiles[i].Key.Trigger();
            ExpiringTiles.RemoveAt(i);
        }

        public bool ContainsEntity(EntityObject e)
        {
            var contains = (SpatialHash.ContainsKey(e.Position) || IdHash.ContainsKey(e.Id));
            return contains;
        }

        public bool EntityCurrentlyMoving(EntityObject e) => EntityToKey.ContainsKey(e);
    }
}
