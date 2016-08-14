using Microsoft.Xna.Framework;

namespace PureGame.Engine.Controllers
{
    public interface IController
    {
        void Update(EntityObject entity, GameTime time);

        int Speed { get; }
    }
}
