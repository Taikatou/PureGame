using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine.Communication;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class BaseTextBoxRenderer : ITextBoxRenderer
    {
        public BitmapFont Font;
        public DialogBox DialogBox;
        public virtual string Text
        {
            get;
            set;
        }
        public BaseTextBoxRenderer()
        {

        }
        public BaseTextBoxRenderer(BitmapFont font, Point position, Point size, string text)
        {
            Font = font;
            Text = text;
            DialogBox = new DialogBox(position, size);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            DialogBox.Draw(spriteBatch, textBoxTexture);
            spriteBatch.DrawString(Font, Text, DialogBox.Position.ToVector2(), Color.Black);
        }

        public virtual bool Tap(Vector2 position)
        {
            var found = DialogBox.Contains(position);
            return found;
        }
    }
}
