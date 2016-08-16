using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using SmallGame;

namespace PureGame.Engine.EntityData
{
    public class EntityObject : BaseIGameObject
    {
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public bool RequestMovement = false;
        public bool RequestInteraction = false;
        public Direction MovementDirection;
        private int WalkingSpeed = 500;
        private int RunningSpeed = 250;
        public bool Running = false;
        public int Speed
        {
            get
            {
                if(Running)
                {
                    return RunningSpeed;
                }
                else
                {
                    return WalkingSpeed;
                }
            }
        }

        public EntityObject()
        {

        }

        public EntityObject(Vector2 Position, string Id, string FileName="CharacterSheet", Direction FacingDirection = Direction.Down)
        {
            this.Position = Position;
            this.FileName = FileName;
            this.FacingDirection = FacingDirection;
            this.Id = Id;
            Type = "EntityObject";
        }

        public Direction FacingDirection;

        public Vector2 FacingPosition
        {
            get
            {
                return DirectionMapper.GetDirection(FacingDirection);
            }
        }

        public Vector2 MovementPosition
        {
            get
            {
                return DirectionMapper.GetDirection(MovementDirection);
            }
        }

        public void Interact(EntityObject interact_with)
        {

        }
    }
}
