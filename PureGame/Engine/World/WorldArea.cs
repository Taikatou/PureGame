using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using PureGame.Engine.Events.WorldTriggers;

namespace PureGame.Engine.World
{
    public class WorldArea 
    {
        public ContentManager Content;
        public EntityManager EntityManager;
        public TriggerManager TriggerManager;
        public WorldMap Map;
        public int MapWidth => Map.Map.Width;
        public int MapHeight => Map.Map.Height;
        public TalkManager TalkManager;
        public List<Entity> Entities;
        public bool CurrentlyMoving(Entity e) => EntityManager.EntityCurrentlyMoving(e);
        public virtual void Update(GameTime time)
        {
            EntityManager.Update(time);
            TalkManager.Update(time);
        }

        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
            Entities = new List<Entity>();
            EntityManager = new EntityManager();
            TriggerManager = new TriggerManager();
            TalkManager = new TalkManager();
        }

        public virtual void OnInit(IWorldLoader worldLoader)
        {
            TriggerManager.WorldLoader = worldLoader;
        }

        public void ProccessInteraction(Entity entity)
        {
            if (!CurrentlyMoving(entity))
            {
                var facingPosition = DirectionMapper.GetMovementFromDirection(entity.FacingDirection);
                var newPosition = entity.Position + facingPosition;
                if (EntityManager.SpatialHash.ContainsKey(newPosition))
                {
                    var interactEntity = EntityManager.SpatialHash[newPosition];
                    ProccessInteraction(entity, interactEntity);
                }
            }
        }

        public void ProccessInteraction(Entity entity, Entity interactWith)
        {
            var directionVector = entity.Position - interactWith.Position;
            var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
            interactWith.FacingDirection = direction;
            TalkManager.StartTalking(entity);
        }

        public void ProccessMovement(Entity e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                var newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    var triggerEvent = TriggerManager.GetTriggerEvent(e, newPosition);
                    EntityManager.MoveEntity(e, triggerEvent, newPosition);
                    e.MomentumChange();
                }
                e.FacingDirection = e.MovementDirection;
            }
        }

        public void AddEntity(Entity e)
        {
            EntityManager.AddEntity(e);
            Entities.Add(e);
        }

        public void RemoveEntity(Entity e)
        {
            EntityManager.RemoveEntity(e);
            Entities.Remove(e);
        }

        public bool HasEntity(Point p)
        {
            return EntityManager.SpatialHash.ContainsKey(p);
        }

        public bool ValidPosition(Point position)
        {
            var withinLimits = position.X >= 0 && position.Y >= 0 &&
                               position.X < MapWidth &&
                               position.Y < MapHeight;
            var entityCollision = !HasEntity(position);
            var mapCollision = !Map.CheckCollision(position);
            return withinLimits && entityCollision && mapCollision;
        }

        public bool ValidPosition(int x, int y)
        {
            return ValidPosition(new Point(x, y));
        }
    }
}
