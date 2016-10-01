using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine.Communication;
using PureGame.Client.Renderable.WorldRenderer;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class PlainTextBoxRenderer : ITextBoxRenderer
    {
        private readonly string _text = "Interacting";
        private readonly Vector2 _position;
        private Rectangle _textBoxSpace;
        private readonly ITextBox _interaction;
        private readonly BitmapFont _font;

        public PlainTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            _font = font;
            var offset = r.PositionFinder.Offset;
            _interaction = r.BaseEntity.Interaction;
            _position = r.ScreenPosition.ToVector2() - offset * 2;
            var textSpace = font.MeasureString(_text);
            var textBoxSize = new Size((int)(textSpace.Width * 1.2), (int)(textSpace.Height * 1.2));
            _textBoxSpace = new Rectangle(_position.ToPoint(), textBoxSize);
            var difference = _textBoxSpace.Width - textSpace.Width;
            _position.X += (int)(difference / 2);
        }

        public bool Tap(Vector2 position)
        {
            var found = _textBoxSpace.Contains(position);
            Debug.WriteLine(position);
            if (found)
            {
                _interaction.Interact();
            }
            return found;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            spriteBatch.Draw(textBoxTexture, _textBoxSpace, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, _text, _position, Color.Black);
        }
    }
}
