using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Controllers
{
    public interface IController
    {
        void Update(EntityObject entity, GameTime time);
    }
}
