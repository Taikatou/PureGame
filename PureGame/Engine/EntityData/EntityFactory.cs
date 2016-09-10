using Microsoft.Xna.Framework;
using PureGame.SmallGame;

namespace PureGame.Engine.EntityData
{
    public class EntityFactory
    {
        public static EntityObject MakeEntityObject(Vector2 position, string fileName, Direction facingDirection = Direction.Down)
        {
            var e = new EntityObject
            {
                Position = position,
                FileName = fileName,
                FacingDirection = facingDirection,
                Id = IdFactory.NewId
            };
            return e;
        }
    }
}
