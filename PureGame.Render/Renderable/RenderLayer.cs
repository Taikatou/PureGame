using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Render.Controllers;

namespace PureGame.Render.Renderable
{
    public abstract class RenderLayer : IRenderable
    {
        public abstract void Draw(SpriteBatch spriteBatch);
        public readonly List<IController> Controllers;

        protected RenderLayer()
        {
            Controllers = new List<IController>();
        }

        public virtual void Update(GameTime time)
        {
            foreach (var controller in Controllers)
            {
                controller.Update(time);
            }
        }
    }
}
