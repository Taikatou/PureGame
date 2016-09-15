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
        public Dictionary<ExpiringKey<Point>, Entity> KeyToEntity;
        public Dictionary<Entity, ExpiringKey<Point>> EntityToKey;
        public Dictionary<Point, Entity> SpatialHash;
        public Dictionary<string, Entity> IdHash;

        public EntityManager()
        {
            IdHash = new Dictionary<string, Entity>();
            ExpiringTiles = new List<ExpiringKey<Point>>();
            TileEvents = new Dictionary<ExpiringKey<Point>, TriggerEvent>();
            EntityToKey = new Dictionary<Entity, ExpiringKey<Point>>();
            KeyToEntity = new Dictionary<ExpiringKey<Point>, Entity>();
            SpatialHash = new Dictionary<Point, Entity>();
        }

        public void AddEntity(Entity e)
        {
            if (!ContainsEntity(e))
            {
                SpatialHash[e.Position] = e;
                IdHash[e.Id] = e;
            }
        }

        public void RemoveEntity(Entity entity) => RemoveEntity(entity.Id);

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

        public void MoveEntity(Entity e, TriggerEvent onCompleteEvent, Point newPosition)
        {
            var movementKey = new ExpiringKey<Point>(e.Position, e.Speed);
            if (onCompleteEvent != null)
            {
                TileEvents[movementKey] = onCompleteEvent;
            }
            ExpiringTiles.Add(movementKey);
            EntityToKey[e] = movementKey;
            KeyToEntity[movementKey] = e;
            Debug.WriteLine("Move Entity " + e.Id + " to " + newPosition);
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

        public bool ContainsEntity(Entity e) => SpatialHash.ContainsKey(e.Position) || IdHash.ContainsKey(e.Id);

        public bool EntityCurrentlyMoving(Entity e) => EntityToKey.ContainsKey(e);
    }
}
