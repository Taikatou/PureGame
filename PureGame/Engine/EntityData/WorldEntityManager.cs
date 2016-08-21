using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using System.Collections.Generic;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{

    public class WorldEntityManager
    {
        public List<ExpiringKey<Vector2>> ExpiringTiles;
        public Dictionary<ExpiringKey<Vector2>, IEntity> KeyToEntity;
        public Dictionary<IEntity, ExpiringKey<Vector2>> EntityToKey;
        public Dictionary<Vector2, IEntity> SpatialHash;
        public Dictionary<string, IEntity> IdHash;
        public List<IEntity> Entities;

        public WorldEntityManager()
        {
            Entities = new List<IEntity>();
            ExpiringTiles = new List<ExpiringKey<Vector2>>();
            EntityToKey = new Dictionary<IEntity, ExpiringKey<Vector2>>();
            KeyToEntity = new Dictionary<ExpiringKey<Vector2>, IEntity>();
            IdHash = new Dictionary<string, IEntity>();
            SpatialHash = new Dictionary<Vector2, IEntity>();
        }

        public void AddEntity(IEntity e)
        {
            if (ContainsEntity(e))
            {
                Entities.Add(e);
                SpatialHash[e.Position] = e;
                IdHash[e.Id] = e;
            }
        }

        public void AddEntityKey(IEntity e, ExpiringKey<Vector2> key)
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
                    RemoveTile(i);
                }
            }
        }

        public void RemoveTile(int i)
        {
            Vector2 position = ExpiringTiles[i].Key;
            Debug.WriteLine("Remove tile : " + position);
            var EntitiyObject = KeyToEntity[ExpiringTiles[i]];
            SpatialHash.Remove(position);
            EntityToKey.Remove(EntitiyObject);
            KeyToEntity.Remove(ExpiringTiles[i]);
            ExpiringTiles.RemoveAt(i);
        }

        public bool ContainsEntity(IEntity e)
        {
            bool contains = !(SpatialHash.ContainsKey(e.Position) || IdHash.ContainsKey(e.Id));
            return contains;
        }

        public bool EntityCurrentlyMoving(IEntity e)
        {
            return EntityToKey.ContainsKey(e);
        }
    }
}
