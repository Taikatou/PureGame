using Microsoft.Xna.Framework;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers
{
    public class CameraController
    {
        public bool ChangeDrag;
        public Vector2 DragPosition;
        public Vector2 NewDragPosition;

        public virtual void Update()
        {
            if (ChangeDrag)
            {
                DragPosition = NewDragPosition;
                ChangeDrag = false;
            }
        }

        public void Zoom(float zoomBy, IControlableLayer layer)
        {
            layer.Zoom(zoomBy);
        }

        public void ReleaseDrag(IControlableLayer layer)
        {
            layer.ReleaseDrag();
        }

        public void Drag(Vector2 newDragPosition, IControlableLayer layer)
        {
            var moveBy = newDragPosition - DragPosition;
            layer.Drag(moveBy);
            if (!ChangeDrag)
            {
                NewDragPosition = newDragPosition;
                ChangeDrag = true;
            }
        }

        public void MoveBy(Vector2 moveBy, IControlableLayer layer)
        {
            var newDragPosition = DragPosition + moveBy;
            Drag(newDragPosition, layer);
        }
    }
}
