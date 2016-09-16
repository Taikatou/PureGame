using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using PureGame.Engine.Events.WorldTriggers;
using PureGame.Engine.World;

namespace PureGame.Engine.EntityData
{
    public class EntityManager
    {
        public List<ExpiringKey<Point>> ExpiringTiles;
        public Dictionary<ExpiringKey<Point>, TriggerEvent> TileEvents;
        public Dictionary<ExpiringKey<Point>, IEntity> KeyToEntity;
        public Dictionary<IEntity, ExpiringKey<Point>> EntityToKey;
        public Dictionary<Point, IEntity> SpatialHash;
        public Dictionary<string, IEntity> IdHash;

        public EntityManager()
        {
            IdHash = new Dictionary<string, IEntity>();
            ExpiringTiles = new List<ExpiringKey<Point>>();
            TileEvents = new Dictionary<ExpiringKey<Point>, TriggerEvent>();
            EntityToKey = new Dictionary<IEntity, ExpiringKey<Point>>();
            KeyToEntity = new Dictionary<ExpiringKey<Point>, IEntity>();
            SpatialHash = new Dictionary<Point, IEntity>();
        }

        public void AddEntity(IEntity e)
        {
            if (!ContainsEntity(e))
            {
                SpatialHash[e.Position] = e;
                IdHash[e.Id] = e;
            }
        }

        public void RemoveEntity(IEntity entity) => RemoveEntity(entity.Id);

        public void RemoveEntity(string entityId)
        {
            if (IdHash.ContainsKey(entityId))
            {
                var entity = IdHash[entityId];
                IdHash.Remove(entityId);
                EntityToKey.Remove(entity);
                SpatialHash.Remove(entity.Position);
            }
        }

        public void MoveEntity(IEntity e, TriggerEvent onCompleteEvent, Point newPosition)
        {
            var movementKey = new ExpiringKey<Point>(e.Position, e.Speed);
            if (onCompleteEvent != null)
            {
                TileEvents[movementKey] = onCompleteEvent;
            }
            ExpiringTiles.Add(movementKey);
            EntityToKey[e] = movementKey;
            KeyToEntity[movementKey] = e;
            e.Position = newPosition;
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
            var tile = ExpiringTiles[i];
            var position = tile.Key;
            var entity = KeyToEntity[tile];
            SpatialHash.Remove(position);
            EntityToKey.Remove(entity);
            KeyToEntity.Remove(tile);
            if (TileEvents.ContainsKey(tile))
            {
                TileEvents[tile].OnTrigger();
                TileEvents.Remove(tile);
            }
            ExpiringTiles.RemoveAt(i);
        }

        public bool ContainsEntity(IEntity e) => SpatialHash.ContainsKey(e.Position) || IdHash.ContainsKey(e.Id);

        public bool EntityCurrentlyMoving(IEntity e) => EntityToKey.ContainsKey(e);
    }
}
