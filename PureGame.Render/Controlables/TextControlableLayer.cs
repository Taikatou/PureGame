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
            var worldRender = RenderLayer.WorldRender;
            position = worldRender.ScreenToWorld(position);
            foreach (var r in worldRender.ToDraw.Elements)
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
