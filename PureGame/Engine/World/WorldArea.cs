using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PureGame.Engine.EntityData;
using System.Diagnostics;
using PureGame.SmallGame;
using System.Collections.Generic;

namespace PureGame.Engine.World
{
    public class WorldArea : GameLevel
    {
        public ContentManager Content;
        public EntityManager EntityManager;
        public MapObject Map;
        public bool Updated;
        private readonly Dictionary<EntityObject, IWorldController> _worldControllers;
        public WorldArea()
        {
            Updated = false;
            Content = ContentManagerManager.RequestContentManager();
            EntityManager = new EntityManager();
            _worldControllers = new Dictionary<EntityObject, IWorldController>();
        }

        public void RegisterWorldController(IWorldController worldController, EntityObject entity)
        {
            _worldControllers[entity] = worldController;
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
                Vector2 newPosition = e.Position + facingPosition;
                if (EntityManager.SpatialHash.ContainsKey(newPosition))
                {
                    EntityObject interactEntity = EntityManager.SpatialHash[newPosition];
                    if (_worldControllers.ContainsKey(interactEntity))
                    {
                        var interactWith = _worldControllers[interactEntity];
                        _worldControllers[e].Interact(interactWith);
                    }
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
                    var movementKey = new ExpiringKey<Vector2>(e.Position, e.Speed);
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
            foreach(var entity in Objects.GetObjects<EntityObject>())
            {
                AddEntity(entity);
            }
        }
    }
}
