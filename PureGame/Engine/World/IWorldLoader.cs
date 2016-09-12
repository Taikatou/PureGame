using PureGame.Engine.EntityData;

namespace PureGame.Engine.World
{
    public interface IWorldLoader
    {
        void AddEntity<T>(Entity entity) where T : WorldArea, new();
    }
}
