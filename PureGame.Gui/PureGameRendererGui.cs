using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui
    {
        public PlainPureGameRendererDebug GameRenderer;
        public PureGameRendererGui(PlainPureGameRendererDebug gameRenderer)
        {
            GameRenderer = gameRenderer;
        }

        public void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
        }

        public void ChangeFocus(EntityObject e)
        {
            GameRenderer.ChangeFocus(e);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameRenderer.Draw(spriteBatch);
        }
    }
}
