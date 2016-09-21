using Microsoft.Xna.Framework;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers
{
    public abstract class CameraController : IController
    {
        public bool ChangeDrag;
        public Vector2 DragPosition;
        public Vector2 NewDragPosition;

        public virtual void Update(GameTime time)
        {
            if (ChangeDrag)
            {
                DragPosition = NewDragPosition;
                ChangeDrag = false;
            }
        }
        public abstract void UpdateLayer(GameTime time, IControlableLayer layers);

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
    }
}
