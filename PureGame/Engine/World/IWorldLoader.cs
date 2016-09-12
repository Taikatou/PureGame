using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.World
{
    public interface IWorldLoader
    {
        void AddEntity<T>(Entity entity, Vector2 endPosition) where T : WorldArea, new();
    }
}
