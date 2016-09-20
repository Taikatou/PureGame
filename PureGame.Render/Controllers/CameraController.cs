using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Render.ControlLayers;

namespace PureGame.Render.Controllers
{
    public abstract class CameraController : IController
    {
        public Vector2 DragPosition;

        public abstract void Update(GameTime time, List<IControlAbleLayer> layers);

        public void Zoom(float zoomBy, IControlAbleLayer layer)
        {
            layer.Zoom(zoomBy);
        }

        public void ReleaseDrag(IControlAbleLayer layer)
        {
            layer.ReleaseDrag();
        }

        public void Drag(Vector2 newDragPosition, IControlAbleLayer layer)
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
