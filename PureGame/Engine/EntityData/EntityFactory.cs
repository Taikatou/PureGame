using Microsoft.Xna.Framework;

namespace PureGame.Engine.EntityData
{
    public class EntityFactory
    {
        public static Entity MakeEntityObject(Vector2 position, string fileName, Direction facingDirection = Direction.Down)
        {
            var e = new Entity
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
