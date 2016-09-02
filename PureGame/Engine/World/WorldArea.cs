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
        public Dictionary<EntityObject, EntityObject> Interactions;
        public MapObject Map;
        public bool Updated;
        public WorldArea()
        {
            Updated = true;
            Content = ContentManagerManager.RequestContentManager();
            Interactions = new Dictionary<EntityObject, EntityObject>();
        }

        public void Update(GameTime time) => EntityManager.Update(time);

        public void ProccessInteraction(EntityObject entity)
        {
            if (!(entity.CurrentlyInteracting || EntityManager.EntityCurrentlyMoving(entity)))
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
            if (!interactWith.CurrentlyInteracting)
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
            interactWith.CurrentlyInteracting = true;
            entity.CurrentlyInteracting = true;
            Interactions[entity] = interactWith;
            Interactions[interactWith] = entity;
        }

        public void RemoveInteraction(EntityObject entity, EntityObject interactWith)
        {
            interactWith.CurrentlyInteracting = true;
            entity.CurrentlyInteracting = true;
            Interactions.Remove(entity);
            Interactions.Remove(interactWith);
        }

        public void ProccessMovement(EntityObject e)
        {
            if (!EntityManager.EntityCurrentlyMoving(e))
            {
                var movementPosition = DirectionMapper.GetMovementFromDirection(e.MovementDirection);
                var newPosition = e.Position + movementPosition;
                if (ValidPosition(newPosition))
                {
                    e.Position = newPosition;
                    var triggerEvent = TriggerManager.Trigger(e);
                    var movementKey = new ExpiringKey<TileEvent>(triggerEvent, e.Speed);
                    EntityManager.AddEntityKey(e, movementKey);
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
