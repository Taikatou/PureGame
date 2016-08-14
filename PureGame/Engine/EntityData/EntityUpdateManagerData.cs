using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{

    public class EntityUpdateManagerData
    {
        public List<ExpiringKey<Vector2>> ExpiringTiles;
        public Dictionary<ExpiringKey<Vector2>, EntityObject> KeyToEntity;
        public Dictionary<EntityObject, ExpiringKey<Vector2>> EntityToKey;
        public Dictionary<Vector2, EntityObject> SpatialHash;
        public Dictionary<string, EntityObject> IdHash;
        public List<EntityObject> Entities;

        public EntityUpdateManagerData(List<EntityObject> Entities)
        {
            this.Entities = Entities;
            ExpiringTiles = new List<ExpiringKey<Vector2>>();
            EntityToKey = new Dictionary<EntityObject, ExpiringKey<Vector2>>();
            KeyToEntity = new Dictionary<ExpiringKey<Vector2>, EntityObject>();
            IdHash = new Dictionary<string, EntityObject>();
            SpatialHash = new Dictionary<Vector2, EntityObject>();
            foreach (EntityObject e in Entities)
            {
                IdHash[e.Id] = e;
                SpatialHash[e.Position] = e;
            }
        }

        public void AddEntity(EntityObject e)
        {
            if (ContainsEntity(e))
            {
                Entities.Add(e);
                SpatialHash[e.Position] = e;
                IdHash[e.Id] = e;
            }
        }

        public void AddEntityKey(EntityObject e, ExpiringKey<Vector2> key)
        {
            ExpiringTiles.Add(key);
            EntityToKey[e] = key;
            KeyToEntity[key] = e;
            SpatialHash[e.Position] = e;
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < ExpiringTiles.Count; i++)
            {
                ExpiringTiles[i].Update(time);
                if (ExpiringTiles[i].TimeLeft <= 0)
                {
                    Vector2 position = ExpiringTiles[i].Key;
                    Debug.WriteLine("Remove tile : " + position);
                    var EntitiyObject = KeyToEntity[ExpiringTiles[i]];
                    SpatialHash.Remove(position);
                    EntityToKey.Remove(EntitiyObject);
                    KeyToEntity.Remove(ExpiringTiles[i]);
                    ExpiringTiles.RemoveAt(i);
                }
            }
        }

        public bool ContainsEntity(EntityObject e)
        {
            bool contains = !(SpatialHash.ContainsKey(e.Position) || IdHash.ContainsKey(e.Id));
            return contains;
        }
    }
}
