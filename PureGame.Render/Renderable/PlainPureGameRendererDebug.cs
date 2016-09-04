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
        private readonly FramesPerSecondCounter _fpsCounter;
        private readonly BitmapFont _bitmapFont;
        public PlainPureGameRenderer GameRenderer;
        public readonly ContentManager Content;

        public PlainPureGameRendererDebug(PlainPureGameRenderer gameRenderer, string fontName="montserrat-32")
        {
            GameRenderer = gameRenderer;
            _fpsCounter = new FramesPerSecondCounter();
            string fileName = $"Fonts/{fontName}";
            Content = ContentManagerManager.RequestContentManager();
            _bitmapFont = Content.Load<BitmapFont>(fileName);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            GameRenderer.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(_bitmapFont, $"FPS: {_fpsCounter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
            spriteBatch.End();
        }
        public void Update(GameTime time)
        {
            GameRenderer.Update(time);
            _fpsCounter.Update(time);
        }

        public void ChangeFocus(EntityObject e)
        {
            GameRenderer.ChangeFocus(e);
        }
    }
}
