using Microsoft.Xna.Framework;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public abstract class CameraController : IController
    {
        public RenderWorldLayer Renderer;
        public PureGameClient Client;
        public Vector2 DragPosition;

        protected CameraController(RenderWorldLayer renderer, PureGameClient client)
        {
            Renderer = renderer;
            Client = client;
        }

        public abstract void Update(GameTime time);

        public void ZoomCamera(float zoomBy)
        {
            var camera = Renderer.Camera;
            var zoom = camera.Zoom;
            zoom += zoomBy;
            if (zoom >= camera.MinimumZoom && zoom <= camera.MaximumZoom)
            {
                camera.Zoom = zoom;
                Renderer.RefreshToDraw();
            }
        }

        public void MoveCameraBy(Vector2 newDragPosition)
        {
            var moveBy = newDragPosition - DragPosition;
            Renderer.FocusStack.MoveFocusBy(moveBy);
            DragPosition = newDragPosition;
        }
    }
}
