using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render.Renderable
{
    public interface IRenderable
    {
        void Draw(SpriteBatch sprite_batch);
        void Update(GameTime time);
    }
}
