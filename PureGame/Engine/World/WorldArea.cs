using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.SmallGame;
using PureGame.Engine.Events;

namespace PureGame.Engine.World
{
    public class WorldArea : GameLevel
    {
        public ContentManager Content;
        public EntityManager EntityManager;
        public TriggerManager TriggerManager;
        public MapObject Map;
        public bool Updated;
        public WorldArea()
        {
            Updated = true;
            Content = ContentManagerManager.RequestContentManager();
        }

        public void Update(GameTime time)
        {
            EntityManager.Update(time);
        }

        public void ProccessInteraction(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var facingPosition = DirectionMapper.GetMovementFromDirection(e.FacingDirection);
                var newPosition = e.Position + facingPosition;
                if (EntityManager.SpatialHash.ContainsKey(newPosition))
                {
                    var interactEntity = EntityManager.SpatialHash[newPosition];
                    var direction = DirectionMapper.GetDirectionFromMovment(e.Position - interactEntity.Position);
                    interactEntity.FacingDirection = direction;
                }
            }
        }

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                var newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    var movementKey = new ExpiringKey<Vector2>(e.Position, e.Speed);
                    EntityManager.AddEntityKey(e, movementKey);
                    e.Position = newPosition;
                    TriggerManager.Trigger(e);
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
            var withinLimits = position.X >= 0 && position.Y >= 0 &&
                               position.X < Map.Map.Width &&
                               position.Y < Map.Map.Height;
            var entityCollision = !EntityManager.SpatialHash.ContainsKey(position);
            var mapCollision = !Map.CheckCollision(position);
            return withinLimits && entityCollision && mapCollision;
        }

        public void UnLoad()
        {
            Content?.Unload();
            Map?.UnLoad();
        }

        public void OnInit(WorldManager worldManager)
        {
            Map = Objects.GetObjects<MapObject>()[0];
            Map.OnInit();
            TriggerManager = new TriggerManager(Objects.GetObjects<TriggerObject>(), worldManager);
            EntityManager = new EntityManager(Objects.GetObjects<EntityObject>());
        }
    }
}
