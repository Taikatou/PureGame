using Microsoft.Xna.Framework;
using PureGame.Render.ControlLayers;

namespace PureGame.Render.Controllers
{
    public abstract class CameraController : IController
    {
        public Vector2 DragPosition;

        public abstract void Update(GameTime time, IControlLayer layer);

        public void Zoom(float zoomBy, IControlLayer layer)
        {
            layer.Zoom(zoomBy);
        }

        public void ReleaseDrag(IControlLayer layer)
        {
            layer.ReleaseDrag();
        }

        public void Drag(Vector2 newDragPosition, IControlLayer layer)
        {
            var moveBy = newDragPosition - DragPosition;
            layer.Drag(moveBy);
            DragPosition = newDragPosition;
        }

        public void Click(Point position)
        {
            
        }
    }
}
