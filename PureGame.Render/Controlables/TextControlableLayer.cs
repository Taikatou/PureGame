using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Client.Renderable.TextRenderer;

namespace PureGame.Client.Controlables
{
    class TextControlableLayer : IControlableLayer
    {
        public TextRenderLayer RenderLayer;

        public TextControlableLayer(TextRenderLayer renderLayer)
        {
            RenderLayer = renderLayer;
        }
        public bool Tap(Vector2 position)
        {
            var found = false;
            var WorldRender = RenderLayer.WorldRender;
            position = WorldRender.ScreenToWorld(position);
            foreach (var r in WorldRender.ToDraw.Elements)
            {
                if (r.BaseEntity.Talking)
                {
                    var textBox = RenderLayer.GetTextBox(r);
                    found = found || textBox.Tap(position);
                }
            }
            return found;
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

        public bool Interact()
        {
            return false;
        }

        public void UnLoad()
        {
            RenderLayer.UnLoad();
        }

        public void Dispose()
        {
            RenderLayer.Dispose();
        }
    }
}
