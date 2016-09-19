using Microsoft.Xna.Framework;

namespace PureGame.Engine.EntityData
{
    public class EntityFactory
    {
        public static IEntity MakeEntityObject(Point position, string fileName, Direction facingDirection = Direction.Down)
        {
            var e = new BaseEntity
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
