using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : AbstractPureGameRenderer
    {
        private RenderWorld render_world;
        public Camera2D Camera;

        public PlainPureGameRenderer(IPureGame game, ViewportAdapter viewport_adapter)
        {
            this.game = game;
            game.Parent = this;
            Camera = new Camera2D(viewport_adapter);
            Camera.Zoom = 0.25f;
            render_world = new RenderWorld(game.Current);
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            render_world.Draw(sprite_batch, Camera);
        }

        public override void Update(GameTime time)
        {
            Game.Update(time);
            render_world.Update(time);
        }

        public override void ChangeFocus(EntityObject e)
        {
            RenderWorld.FocusEntity = e;
        }

        public override void OnWorldChange()
        {
            render_world?.UnLoad();
            render_world = new RenderWorld(game.Current);
        }
    }
}
