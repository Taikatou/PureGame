using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        private IPureGame game;
        private RenderWorld render_world;
        public Camera2D Camera;

        public IPureGame Game
        {
            get
            {
                return game;
            }
        }

        public PlainPureGameRenderer(IPureGame game, ViewportAdapter viewport_adapter)
        {
            this.game = game;
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
            if(render_world == null || render_world.World.Name != game.Current.Name)
            {
                render_world?.UnLoad();
                render_world = new RenderWorld(game.Current);
            }
            render_world.Update(time);
        }

        public void ChangeFocus(EntityObject e)
        {
            RenderWorld.FocusEntity = e;
        }
    }
}
