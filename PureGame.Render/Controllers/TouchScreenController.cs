using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Engine;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public bool Running;
        public bool InteractAfter;
        public TouchScreenController(RenderWorldLayer renderer, PureGameClient client) : base(renderer, client)
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap;
        }

        public override void Update(GameTime time)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                switch (gesture.GestureType)
                {
                    case GestureType.Pinch:
                        PinchZoom(gesture);
                        break;
                    case GestureType.Tap:
                        Tap(gesture);
                        break;
                    case GestureType.DoubleTap:
                        Running = true;
                        break;
                    case GestureType.FreeDrag:
                        MoveCamera(gesture);
                        break;
                }
            }
            if (PreviouslyMovingCamera)
            {
                var touchCol = TouchPanel.GetState();
                foreach (var touch in touchCol)
                {
                    if (touch.State == TouchLocationState.Released)
                    {
                        PreviouslyMovingCamera = false;
                        Renderer.FocusStack.EndMove();
                    }
                }
            }
        }

        public void MoveCamera(GestureSample gesture)
        {
            if (!PreviouslyMovingCamera)
            {
                Renderer.FocusStack.BeginMove();
                PreviouslyMovingCamera = true;
                DragPosition = gesture.Position;
            }
            else
            {
                MoveCameraBy(gesture.Position);
            }
        }

        public void Tap(GestureSample gesture)
        {
            var touchPosition = Renderer.WorldPosition(gesture.Position);
            MoveEntity(touchPosition);
        }

        public void MoveEntity(Point endPosition)
        {
            var entity = Client.Entity;
            Client.PureGame.AddEntityMover(entity, endPosition, Running);
        }

        public void PinchZoom(GestureSample gesture)
        {
            var dist = Vector2.Distance(gesture.Position, gesture.Position2);

            var aOld = gesture.Position - gesture.Delta;
            var bOld = gesture.Position2 - gesture.Delta2;
            var distOld = Vector2.Distance(aOld, bOld);

            var scale = (distOld - dist) / 500f;
            ZoomCamera(scale);
        }
    }
}
