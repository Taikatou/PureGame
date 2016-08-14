using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using MonoGame.Extended.Maps.Tiled;

namespace PureGame.Engine
{
    public class EntityUpdateManager
    {
        public EntityUpdateManagerData Data;
        public List<EntityMover> Movers;
        private WorldArea parent;

        public EntityUpdateManager(List<EntityObject> entities, WorldArea parent)
        {
            Data = new EntityUpdateManagerData(entities);
            Movers = new List<EntityMover>();
            this.parent = parent;
        }

        public void AddEntity(EntityObject e)
        {
            Data.AddEntity(e);
        }

        public void Update(GameTime time)
        {
            Vector2 previous_position;
            foreach(EntityMover m in Movers)
            {
                if(!Data.EntityToKey.ContainsKey(m.Entity))
                {
                    previous_position = m.Entity.Position;
                    m.Update(time);
                    if (previous_position != m.Entity.Position)
                    {
                        if (ValidPosition(m.Entity.Position))
                        {
                            var movement_key = new ExpiringKey<Vector2>(previous_position, m.Speed);
                            Data.AddEntityKey(m.Entity, movement_key);
                        }
                        else
                        {
                            m.Entity.Position = previous_position;
                        }
                    }
                }
            }
            Data.Update(time);
        }

        private bool ValidPosition(Vector2 position)
        {
            bool within_limits = position.X >= 0 && position.Y >= 0 &&
                                 position.X < parent.CollisionMap.TiledMap.Width &&
                                 position.Y < parent.CollisionMap.TiledMap.Height;
            bool entity_collision = !Data.SpatialHash.ContainsKey(position);
            bool map_collision = !parent.CollisionMap.CheckCollision(position);
            Debug.WriteLine("Map collision: " + map_collision);
            return within_limits && entity_collision && map_collision;
        }
    }
}
