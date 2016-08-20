using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using System.Diagnostics;

namespace PureGame.Engine.EntityData
{
    public class EntityUpdateManager
    {
        public EntityUpdateManagerData Data;
        private WorldArea parent;

        public EntityUpdateManager(WorldArea parent)
        {
            Data = new EntityUpdateManagerData();
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

        public void ProccessInteraction(IEntity e)
        {
            var FacingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
            Vector2 new_position = e.Position + FacingPosition;
            if (Data.SpatialHash.ContainsKey(new_position))
            {
                IEntity interact_entity = Data.SpatialHash[new_position];
                e.Interact(interact_entity);
            }
        }

        public void ProccessMovement(IEntity e)
        {
            var MovementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
            Vector2 new_position = e.Position + MovementPosition;
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
