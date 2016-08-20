using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Client.Controllers
{
    enum Controls { Left, Right, Up, Down, A, B}
    public interface IController
    {
        void Update(IEntity entity, GameTime time);
    }
}
