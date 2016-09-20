using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PureGame.Render.Renderable.HudRenderer
{
    public class HudRenderLayer
    {
        public Stack<HudLayer> HudLayers;
        public HudLayer HudFocus => HudLayers.Peek();
        public void UnLoad()
        {
            while (HudLayers.Count > 0)
            {
                HudFocus.UnLoad();
                HudLayers.Pop();
            }
        }

        public void Update(GameTime time)
        {
            while (HudLayers.Count > 0)
            {
                HudFocus.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }

        public HudRenderLayer()
        {
            HudLayers = new Stack<HudLayer>();
        }
    }
}
