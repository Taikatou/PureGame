using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Render.Renderable;

namespace PureGame.Gui
{
    public class PureGameRendererGui : IPureGameRenderer
    {
        public PlainPureGameRenderer game;
        public PureGameRendererGui(PlainPureGameRenderer game)
        {
            this.game = game;
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

        public void Draw(SpriteBatch sprite_batch)
        {
            game.Draw(sprite_batch);
        }

        public void Update(GameTime timer)
        {
            game.Update(timer);
        }
    }
}
