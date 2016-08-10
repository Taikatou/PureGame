using Microsoft.Xna.Framework;
using PureGame.Render;

namespace PureGame.Gui
{
    public class PureGameRendererGui : IPureGameRenderer
    {
        public IPureGameRenderer game;
        public PureGameRendererGui(IPureGameRenderer game)
        {
            this.game = game;
        }
        public void Draw()
        {
            game.Draw();
        }

        public void Update(GameTime timer)
        {
            game.Update(timer);
        }
    }
}
