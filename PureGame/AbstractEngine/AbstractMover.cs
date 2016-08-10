using Microsoft.Xna.Framework;
using SmallGame.GameObjects;

namespace PureGame.AbstractEngine
{
    public abstract class AbstractMover : GameObject
    {
        public abstract void Update(GameTime timer);
    }
}
