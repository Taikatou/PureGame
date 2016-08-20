using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable
{
    public interface IPureGameRenderer :  IPureGame
    {
        void Draw(SpriteBatch sprite_batch);
        IPureGame Game
        {
            get;
        }
        void ChangeFocus(IEntity e);
    }
}
