using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Client.Renderable.HudRenderer;

namespace PureGame.Client.Controlables
{
    class HudControlableLayer : IControlableLayer
    {
        public HudRenderLayer HudRenderer;

        public HudControlableLayer(HudRenderLayer hudRenderer)
        {
            HudRenderer = hudRenderer;
        }
        public void Cancel(bool cancelValue)
        {
            
        }

        public void ControllerDPad(Direction direction)
        {
            
        }

        public void Dispose()
        {
            HudRenderer.Dispose();
        }

        public void DoubleTap()
        {
            
        }

        public void Drag(Vector2 dragBy)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            HudRenderer.Draw(spriteBatch);
        }

        public bool Interact()
        {
            return false;
        }

        public void ReleaseDrag()
        {
            
        }

        public bool Tap(Vector2 position)
        {
            return false;
        }

        public void UnLoad()
        {
            HudRenderer.UnLoad();
        }

        public void Update(GameTime time)
        {
            HudRenderer.Update(time);
        }

        public void Zoom(float zoomBy)
        {
            
        }
    }
}
