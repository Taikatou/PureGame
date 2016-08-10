using Microsoft.Xna.Framework;
using System;

namespace PureGame.Render
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        IPureGame game;
        public PlainPureGameRenderer(IPureGame game)
        {
            this.game = game;
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime timer)
        {
            game.Update(timer);
        }
    }
}
