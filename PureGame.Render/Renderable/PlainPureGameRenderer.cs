using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer
    {
        private RenderWorld render_world;
        public Camera2D Camera;
        public PureGameClient GameClient;
        public PlainPureGameRenderer(PureGameClient GameClient, ViewportAdapter viewport_adapter)
        {
            this.GameClient = GameClient;
            Camera = new Camera2D(viewport_adapter);
            Camera.Zoom = 0.25f;
            render_world = new RenderWorld(GameClient.PureGame.World);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            render_world.Draw(sprite_batch, Camera);
        }

        public void Update(GameTime time)
        {
            GameClient.Update(time);
            render_world.Update(time);
        }

        public void ChangeFocus(EntityObject e)
        {
            RenderWorld.FocusEntity = e;
        }
    }
}
