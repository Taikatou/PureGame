using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Client.Renderable.TextRenderer
{
    public interface ITextBoxRenderer
    {
        string Text { get; }
        bool Tap(Vector2 position);
        void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture);
    }
}
