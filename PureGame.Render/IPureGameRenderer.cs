using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;

namespace PureGame.Render
{
    public interface IPureGameRenderer
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime timer);
        void ChangeFocus(EntityObject e);
    }
}
