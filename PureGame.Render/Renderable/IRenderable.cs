using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Client.Renderable
{
    public interface IRenderable
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime time);
    }
}
