using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRendererDebug : AbstractPureGameRenderer
    {
        private FramesPerSecondCounter fps_counter;
        private BitmapFont bitmapFont;
        IPureGameRenderer GameRenderer;
        ContentManager Content;

        public PlainPureGameRendererDebug(IPureGameRenderer GameRenderer)
        {
            this.GameRenderer = GameRenderer;
            game = GameRenderer;
            GameRenderer.Parent = this;
            fps_counter = new FramesPerSecondCounter();
            string file_name = string.Format("Fonts/{0}", "montserrat-32");
            Content = ContentManagerManager.RequestContentManager();
            bitmapFont = Content.Load<BitmapFont>(file_name);
        }
        public override void Draw(SpriteBatch sprite_batch)
        {
            GameRenderer.Draw(sprite_batch);
            sprite_batch.Begin();
            sprite_batch.DrawString(bitmapFont, $"FPS: {fps_counter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
            sprite_batch.End();
        }
        public override void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
            fps_counter.Update(timer);
        }

        {
            GameRenderer.ChangeFocus(e);
        }
    }
}
