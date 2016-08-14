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
        private float rotation = 0.0f;

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

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

        public void Update(GameTime timer)
        {
            game.Update(timer);
            if(render_world == null ||
               render_world.WorldName != game.Current.Name)
            {
                render_world?.UnLoad();
                render_world = new RenderWorld(game.Current, this, content);
            }
        }
    }
}
