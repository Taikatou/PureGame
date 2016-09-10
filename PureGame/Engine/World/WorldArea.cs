using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using PureGame.SmallGame;
using PureGame.Engine.Events;

namespace PureGame.Engine.World
{
    public class WorldArea : GameLevel
    {
        public ContentManager Content;
        public EntityManager EntityManager;
        public TriggerManager TriggerManager;
        public InteractionManager Interactions;
        public MapObject Map;
        public List<EntityObject> Entities;
        public WorldArea()
        {
            Content = ContentManagerManager.RequestContentManager();
            Interactions = new InteractionManager();
        }

        public void Update(GameTime time) => EntityManager.Update(time);
        public bool CurrentlyInteracting(EntityObject e) => Interactions.Interacting(e);
        public bool CurrentlyMoving(EntityObject e) => EntityManager.EntityCurrentlyMoving(e);

        public void ProccessInteraction(EntityObject entity)
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

        public void ProccessInteraction(EntityObject entity, EntityObject interactWith)
        {
            var entityInteracting = CurrentlyInteracting(interactWith);
            if (!entityInteracting)
            {
                //Move entity to face
                var directionVector = entity.Position - interactWith.Position;
                var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
                interactWith.FacingDirection = direction;
                //start interaction
                AddInteraction(entity, interactWith);
            }
        }

        public void AddInteraction(EntityObject entity, EntityObject interactWith)
        {
            Interactions.AddInteraction(entity, interactWith);
        }

        public void ProgressInteraction(EntityObject e) => Interactions.ProgressInteractions(e);

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                var newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    var triggerEvent = TriggerManager.Trigger(e, newPosition);
                    EntityManager.MoveEntity(e, triggerEvent, newPosition);
                    e.MomentumChange();
                }
                e.FacingDirection = e.MovementDirection;
            }
        }
        public void AddEntity(EntityObject e)
        {
            EntityManager.AddEntity(e);
            Entities.Add(e);
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
            Content.Unload();
            Map.Content.Unload();
        }

        public void OnInit(WorldManager worldManager)
        {
            Map = Objects.GetObjects<MapObject>()[0];
            Map.OnInit();
            Entities = Objects.GetObjects<EntityObject>();
            TriggerManager = new TriggerManager(Objects.GetObjects<TriggerObject>(), worldManager);
            EntityManager = new EntityManager(Entities);
        }
    }
}
