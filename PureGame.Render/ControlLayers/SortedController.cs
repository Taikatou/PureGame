using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;

namespace PureGame.Render.ControlLayers
{
    public class SortedController : IControlLayer
    {
        public IControlLayer Layer;
        public int LayerDepth;

        public SortedController(IControlLayer layer, int depth)
        {
            Layer = layer;
            LayerDepth = depth;
        }

        public void Tap(Vector2 position)
        {
            Layer.Tap(position);
        }

        public void DoubleTap()
        {
            Layer.DoubleTap();
        }

        public void Zoom(float zoomBy)
        {
            Layer.Zoom(zoomBy);
        }

        public void Drag(Vector2 dragBy)
        {
            Layer.Drag(dragBy);
        }

        public void ReleaseDrag()
        {
            Layer.ReleaseDrag();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Layer.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            Layer.Update(time);
        }

        public void ControllerDPad(Direction direction)
        {
            Layer.ControllerDPad(direction);
        }

        public void Cancel(bool cancelValue)
        {
            Layer.Cancel(cancelValue);
        }

        public void Interact()
        {
            Layer.Interact();
        }

        public void UnLoad()
        {
            Layer.UnLoad();
        }
    }
}
