using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render
{
    public class PlainPureGameRendererDebug : IPureGameRenderer
    {
        PlainPureGameRenderer game;
        public PlainPureGameRendererDebug(PlainPureGameRenderer game)
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
