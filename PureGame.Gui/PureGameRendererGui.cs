using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui
    {
        public PlainPureGameRenderer GameRenderer;
        public PureGameRendererGui(PlainPureGameRenderer GameRenderer)
        {
            this.GameRenderer = GameRenderer;
        }

        public void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
        }

        public void ChangeFocus(EntityObject e)
        {
            GameRenderer.ChangeFocus(e);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            GameRenderer.Draw(sprite_batch);
        }
    }
}
