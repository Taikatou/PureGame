using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Common;
using PureGame.Common.PathFinding;
using PureGame.Engine.EntityData;
using PureGame.Engine.World.Controllers;

namespace PureGame.Engine.World.EntityMover
{
    public class EntityMover
    {
        public bool Complete => CurrentPath.Count == 0;
        public IEntity Entity;
        public Point EndPoint;
        public List<Point> CurrentPath;
        public Point NextPosition => CurrentPath[0];
        public BaseController Controller;
        public bool InteractAfter;

        public EntityMover(WorldArea world, IEntity entity, Point endPoint)
        {
            Entity = entity;
            EndPoint = endPoint;
            var searchParams = new SearchParameters(entity.Position, endPoint, world);
            var pathFinder = new AStarPathFinder(searchParams);
            CurrentPath = pathFinder.FindPath();
            Controller = new BaseController(world, entity);
            InteractAfter = world.HasEntity(endPoint);
        }

        public void Update(GameTime time)
        {
            var currentlyMoving = Controller.CurrentWorld.EntityManager.EntityCurrentlyMoving(Entity);
            if (!Complete && !currentlyMoving)
            {
                var directionVector = NextPosition - Entity.Position;
                var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
                if (direction != Direction.None)
                {
                    Controller.MoveDirection(direction);
                }
                CurrentPath.RemoveAt(0);
                if (Complete && InteractAfter)
                {
                    Controller.Interact();
                }
            }
        }
    }
}
