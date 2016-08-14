using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using SmallGame;

namespace PureGame.Engine
{
    public class EntityObject : BaseIGameObject
    {
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position;
        public float Rotation = 0.0f;
        public EntityObject()
        {

        }

        public Direction Facing;

        public Vector2 FacingPosition
        {
            get
            {
                return DirectionMapper.GetDirection(Facing);
            }
        }
    }
}
