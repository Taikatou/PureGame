using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World
{
    public interface IWorldLoader
    {
        void AddEntity<T>(IEntity entity, Point endPosition) where T : WorldArea, new();
    }
}
