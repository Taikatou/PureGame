using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine.Communication;
using PureGame.Client.Renderable.WorldRenderer;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class PlainTextBoxRenderer : ITextBoxRenderer
    {
        private readonly string _text = "Interacting";
        private readonly ITextBox _interaction;
        private readonly BitmapFont _font;
        public DialogBox DialogBox;

        public PlainTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            _font = font;
            _interaction = r.BaseEntity.Interaction;
            var textSpace = font.GetSize(_text);
            DialogBox = new DialogBox(r, textSpace);
        }

        public bool Tap(Vector2 position)
        {
            var found = DialogBox.Contains(position.ToPoint());
            Debug.WriteLine(position);
            if (found)
            {
                _interaction.Interact();
            }
            return found;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            DialogBox.Draw(spriteBatch, textBoxTexture);
            spriteBatch.DrawString(_font, _text, DialogBox.Position.ToVector2(), Color.Black);
        }
    }
}
