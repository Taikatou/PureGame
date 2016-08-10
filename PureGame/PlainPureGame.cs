using Microsoft.Xna.Framework;
using PureGame.Engine;

namespace PureGame
{
    public class PlainPureGame : IPureGame
    {
        WorldArea current;
        public PlainPureGame(WorldArea current)
        {
            this.current = current;
        }
        public void Update(GameTime timer)
        {
            current.Update(timer);
        }
    }
}
