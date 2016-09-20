using PureGame.Engine.EntityData;

namespace PureGame.Engine.World.Controllers
{
    public abstract class AbstractController
    {
        public IEntity Entity;
        public abstract WorldArea CurrentWorld { get; }

        public bool Running
        {
            get { return Entity.Running; }
            set { Entity.Running = value; }
        }

        public void Interact()
        {
            CurrentWorld.ProccessInteraction(Entity);
        }

        public void FaceDirection(Direction direction)
        {
            Entity.FacingDirection = direction;
        }

        public void MoveDirection(Direction direction)
        {
            Entity.MovementDirection = direction;
            CurrentWorld.ProccessMovement(Entity);
        }

        protected AbstractController(IEntity entity)
        {
            Entity = entity;
        }
    }
}
