using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public interface IPureGameRenderer
    {
        void Draw(SpriteBatch sprite_batch);
        void Update(GameTime time);
        IPureGame Game
        {
            get;
        }
        void ChangeFocus(EntityObject e);
    }
}
