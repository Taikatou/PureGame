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
        public string Text => "Interacting";
        private readonly IInteraction _interaction;
        private readonly BitmapFont _font;
        public DialogBox DialogBox;

        public PlainTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            _font = font;
            _interaction = r.BaseEntity.Interaction;
            var textSpace = font.GetSize(Text);
            DialogBox = new DialogBox(r.ScreenPosition, textSpace);
        }

        public PlainTextBoxRenderer(BitmapFont font, Point position, Point size, IInteraction interaction)
        {
            _font = font;
            _interaction = interaction;
            DialogBox = new DialogBox(position, size);
        }

        public bool Tap(Vector2 position)
        {
            var found = DialogBox.Contains(position);
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
            spriteBatch.DrawString(_font, Text, DialogBox.Position.ToVector2(), Color.Black);
        }
    }
}
