using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using PureGame.Engine.Events;
using PureGame.Engine.Events.WorldTriggers;

namespace PureGame.Engine.World
{
    public class WorldArea 
    {
        public ContentManager Content;
        public EntityManager EntityManager;
        public TriggerManager TriggerManager;
        public InteractionManager Interactions;
        public WorldMap Map;
        public List<Entity> Entities;
        public void Update(GameTime time) => EntityManager.Update(time);
        public bool CurrentlyInteracting(Entity e) => Interactions.Interacting(e);
        public bool CurrentlyMoving(Entity e) => EntityManager.EntityCurrentlyMoving(e);
        public void AddInteraction(Entity entity, Entity interactWith) => Interactions.AddInteraction(entity, interactWith);
        public void ProgressInteraction(Entity e) => Interactions.ProgressInteractions(e);

        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
            Entities = new List<Entity>();
            EntityManager = new EntityManager();
            Interactions = new InteractionManager();
            TriggerManager = new TriggerManager();
        }

        public virtual void OnInit(IWorldLoader worldLoader)
        {
            TriggerManager.WorldLoader = worldLoader;
        }

        public void ProccessInteraction(Entity entity)
        {
            var currentlyInteracting = CurrentlyInteracting(entity);
            if (!(currentlyInteracting || CurrentlyMoving(entity)))
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
            var entityInteracting = CurrentlyInteracting(interactWith);
            if (!entityInteracting)
            {
                var directionVector = entity.Position - interactWith.Position;
                var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
                interactWith.FacingDirection = direction;
                AddInteraction(entity, interactWith);
            }
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

        private bool ValidPosition(Vector2 position)
        {
            var withinLimits = position.X >= 0 && position.Y >= 0 &&
                               position.X < Map.Map.Width &&
                               position.Y < Map.Map.Height;
            var entityCollision = !EntityManager.SpatialHash.ContainsKey(position);
            var mapCollision = !Map.CheckCollision(position);
            return withinLimits && entityCollision && mapCollision;
        }
    }
}
