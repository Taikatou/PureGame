using Microsoft.Xna.Framework;

namespace PureGame.Render
{
    public class PlainPureGameRendererDebug : IPureGameRenderer
    {
        IPureGameRenderer game;
        public PlainPureGameRendererDebug(IPureGameRenderer game)
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
