using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRendererDebug
    {
        private FramesPerSecondCounter fps_counter;
        private BitmapFont bitmapFont;
        public PlainPureGameRenderer GameRenderer;
        private ContentManager Content;

        public PlainPureGameRendererDebug(PlainPureGameRenderer GameRenderer)
        {
            this.GameRenderer = GameRenderer;
            fps_counter = new FramesPerSecondCounter();
            string file_name = string.Format("Fonts/{0}", "montserrat-32");
            Content = ContentManagerManager.RequestContentManager();
            bitmapFont = Content.Load<BitmapFont>(file_name);
        }
        public void Draw(SpriteBatch sprite_batch)
        {
            GameRenderer.Draw(sprite_batch);
            sprite_batch.Begin();
            sprite_batch.DrawString(bitmapFont, $"FPS: {fps_counter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
            sprite_batch.End();
        }
        public void Update(GameTime timer)
        {
            GameRenderer.Update(timer);
            fps_counter.Update(timer);
        }

        public void ChangeFocus(EntityObject e)
        {
            GameRenderer.FocusEntity = e;
        }
    }
}
