using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui : AbstractPureGameRenderer
    {
        public IPureGameRenderer GameRenderer;
        public PureGameRendererGui(IPureGameRenderer game)
        {
            this.game = game;
            game.Parent = this;
            GameRenderer = game;
        }

        public override void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
        }

        public override void ChangeFocus(IEntity e)
        {
            GameRenderer.ChangeFocus(e);
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            GameRenderer.Draw(sprite_batch);
        }
    }
}
