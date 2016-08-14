﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRendererDebug : IPureGameRenderer
    {
        private FramesPerSecondCounter fps_counter;
        private BitmapFont bitmapFont;
        PlainPureGameRenderer game;
        ContentManager Content;

        public PlainPureGameRendererDebug(PlainPureGameRenderer game)
        {
            this.game = game;
            fps_counter = new FramesPerSecondCounter();
            string file_name = string.Format("Fonts/{0}", "montserrat-32");
            Content = ContentManagerManager.RequestContentManager();
            bitmapFont = Content.Load<BitmapFont>(file_name);
        }
        public void Draw(SpriteBatch sprite_batch)
        {
            game.Draw(sprite_batch);
            sprite_batch.Begin();
            sprite_batch.DrawString(bitmapFont, $"FPS: {fps_counter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
            sprite_batch.End();
        }
        public void Update(GameTime timer)
        {
            game.Update(timer);
            fps_counter.Update(timer);
        }
    }
}