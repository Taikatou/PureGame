using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Controllers
{
    enum Controls { Left, Right, Up, Down, A, B}
    public interface IController
    {
        void Update(PlayerEntityObject entity, GameTime time);
    }
}
