using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class DialogBox
    {
        public readonly Point Size;
        public Point Position;
        public Rectangle TextBoxSpace;
        public string Text;

        public bool Contains(Point point)
        {
            return TextBoxSpace.Contains(point);
        }

        public bool Contains(Vector2 point)
        {
            return Contains(point.ToPoint());
        }

        public DialogBox(Point screenPosition, Point size, string text)
        {
            Text = text;
            Size = size;
            var xPos = Size.X/2;
            var offset = new Point(xPos, Size.Y);
            Position = screenPosition - offset;
            Position.Y += Size.Y;
            TextBoxSpace = new Rectangle(Position, Size);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture, BitmapFont font)
        {
            spriteBatch.Draw(textBoxTexture, TextBoxSpace, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
            spriteBatch.DrawString(font, Text, Position.ToVector2(), Color.Black);
        }
    }
}
