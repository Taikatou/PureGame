using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui : IPureGameRenderer
    {
        public IPureGameRenderer GameRenderer;
        public PureGameRendererGui(PlainPureGameRenderer GameRenderer)
        {
            this.GameRenderer = GameRenderer;
            rotation = 90.0f;
        }

        public float rotation = 0.0f;

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

        public IPureGame Game
        {
            get
            {
                return GameRenderer.Game;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            GameRenderer.Draw(sprite_batch);
        }

        public void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
        }

        public void ChangeFocus(EntityObject e)
        {
            GameRenderer.ChangeFocus(e);
        }
    }
}
