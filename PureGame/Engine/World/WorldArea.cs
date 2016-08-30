using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.Engine.Controllers;
using PureGame.SmallGame;

namespace PureGame.Engine.World
{
    public class WorldArea : GameLevel
    {
        public enum MessageCode { Refresh }
        public ContentManager Content;
        public EntityManager EntityManager;
        public MapObject Map;
        public bool Updated;
        public WorldArea()
        {
            Updated = false;
            Content = ContentManagerManager.RequestContentManager();
            EntityManager = new EntityManager();
        }

        public void Update(GameTime time)
        {
            EntityManager.Update(time);
            foreach (var entity in EntityManager.Entities)
            {
                if (entity.RequentInteraction)
                {
                    ProccessInteraction(entity);
                    entity.RequentInteraction = false;
                }
                if (entity.RequestMovement)
                {
                    ProccessMovement(entity);
                    entity.RequestMovement = false;
                }
            }
        }

        public void ProccessInteraction(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var facingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
                Vector2 newPosition = e.Position + facingPosition;
                if (EntityManager.SpatialHash.ContainsKey(newPosition))
                {
                    EntityObject interactEntity = EntityManager.SpatialHash[newPosition];
                    e.Interact(interactEntity);
                }
            }
        }

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                Vector2 newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    var movementKey = new ExpiringKey<Vector2>(e.Position, e.GetSpeed());
                    EntityManager.AddEntityKey(e, movementKey);
                    e.Position = newPosition;
                    Updated = true;
                }
                e.FacingDirection = e.MovementDirection;
            }
        }
        public void AddEntity(EntityObject e)
        {
            EntityManager.AddEntity(e);
        }

        private bool ValidPosition(Vector2 position)
        {
            bool withinLimits = position.X >= 0 && position.Y >= 0 &&
                                position.X < Map.Map.Width &&
                                position.Y < Map.Map.Height;
            bool entityCollision = !EntityManager.SpatialHash.ContainsKey(position);
            bool mapCollision = !Map.CheckCollision(position);
            Debug.WriteLine("Map collision: " + mapCollision);
            return withinLimits && entityCollision && mapCollision;
        }

        public void UnLoad()
        {
            Content?.Unload();
            Map?.UnLoad();
        }

        public void OnInit()
        {
            Map = Objects.GetObjects<MapObject>()[0];
            Map.OnInit();

            foreach(EntityObject e in Objects.GetObjects<EntityObject>())
            {
                AddEntity(e);
            }
        }
    }
}
