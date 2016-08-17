using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{
    public class EntityUpdateManager
    {
        public EntityUpdateManagerData Data;
        private WorldArea parent;

        public EntityUpdateManager(List<EntityObject> entities, WorldArea parent)
        {
            Data = new EntityUpdateManagerData(entities);
            this.parent = parent;
        }

        public void Update(GameTime time)
        {
            foreach (var e in Data.Entities)
            {
                if (e.RequestMovement && !Data.EntityCurrentlyMoving(e))
                {
                    ProccessMovement(e);
                }
                if(e.RequestInteraction)
                {
                    ProccessInteraction(e);
                }
            }
            Data.Update(time);
        }

        public void ProccessInteraction(EntityObject e)
        {
            Vector2 new_position = e.Position + e.FacingPosition;
            if (Data.SpatialHash.ContainsKey(new_position))
            {
                EntityObject interact_entity = Data.SpatialHash[new_position];
                Debug.WriteLine(e.Id + " Interact with " + interact_entity.Id);
                interact_entity.Interact(e);
            }
        }

        public void ProccessMovement(EntityObject e)
        {
            Vector2 new_position = e.Position + e.MovementPosition;
            if (ValidPosition(new_position))
            {
                var movement_key = new ExpiringKey<Vector2>(e.Position, e.Speed);
                Data.AddEntityKey(e, movement_key);
                e.Position = new_position;
            }
            e.FacingDirection = e.MovementDirection;
            e.RequestMovement = false;
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
