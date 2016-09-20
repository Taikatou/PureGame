using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Render.Renderable.TextRenderer;

namespace PureGame.Render.ControlLayers
{
    class TextControlAbleLayer : IControlAbleLayer
    {
        public TextRenderLayer RenderLayer;

        public TextControlAbleLayer(TextRenderLayer renderLayer)
        {
            RenderLayer = renderLayer;
        }
        public void Tap(Vector2 position)
        {
        }

        public void DoubleTap()
        {
        }

        public void Zoom(float zoomBy)
        {
        }

        public void Drag(Vector2 dragBy)
        {
        }

        public void ReleaseDrag()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            RenderLayer.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            RenderLayer.Update(time);
        }

        public void ControllerDPad(Direction direction)
        {
        }

        public void Cancel(bool cancelValue)
        {
        }

        public void Interact()
        {
        }

        public void UnLoad()
        {
            RenderLayer.UnLoad();
        }
    }
}
