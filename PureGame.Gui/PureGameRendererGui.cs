using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Render;

namespace PureGame.Gui
{
    public class PureGameRendererGui : IPureGameRenderer
    {
        public PlainPureGameRenderer game;
        public PureGameRendererGui(PlainPureGameRenderer game)
        {
            this.game = game;
        }
        public void Draw(SpriteBatch sprite_batch)
        {
            game.Draw(sprite_batch);
        }

        public void Update(GameTime timer)
        {
            game.Update(timer);
        }
    }
}
