using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;

namespace PureGame.Client.Controlables
{
    public class SortedControlableLayer : IControlableLayer
    {
        public IControlableLayer Layer;
        public int LayerDepth;

        public SortedControlableLayer(IControlableLayer layer, int depth)
        {
            Layer = layer;
            LayerDepth = depth;
        }

        public bool Tap(Vector2 position)
        {
            return Layer.Tap(position);
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

        public bool Interact()
        {
            return Layer.Interact();
        }

        public void UnLoad()
        {
            Layer.UnLoad();
        }

        public void Dispose()
        {
            Layer.Dispose();
        }
    }
}
