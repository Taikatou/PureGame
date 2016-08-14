using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        private PlainPureGame game;
        private RenderWorld render_world;
        private ContentManager content;
        public Camera2D Camera;

        public PlainPureGameRenderer(PlainPureGame game, ViewportAdapter viewport_adapter, ContentManager content)
        {
            this.game = game;
            this.content = content;
            Camera = new Camera2D(viewport_adapter);
            Camera.Zoom = 0.25f;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            render_world.Draw(sprite_batch, Camera);
        }

        public void Update(GameTime time)
        {
            game.Update(time);
            if(render_world == null || render_world.WorldName != game.Current.Name)
            {
                render_world?.UnLoad();
                render_world = new RenderWorld(game.Current, this, content);
            }
            render_world.Update(time);
        }
    }
}
