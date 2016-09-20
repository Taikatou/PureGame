using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Render.Renderable.HudRenderer;

namespace PureGame.Render.ControlLayers
{
    {
        public HudRenderLayer HudRenderer;

        public HudControlLayer(HudRenderLayer hudRenderer)
        {
            HudRenderer = hudRenderer;
        }
        public void Cancel(bool cancelValue)
        {
            
        }

        public void ControllerDPad(Direction direction)
        {
            
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

        public void Interact()
        {
            
        }

        public void ReleaseDrag()
        {
            
        }

        public void Tap(Vector2 position)
        {
            
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
