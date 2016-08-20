using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui : AbstractPureGameRenderer
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
