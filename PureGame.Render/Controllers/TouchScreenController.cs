using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public TouchScreenController(RenderWorldLayer renderer, PureGameClient client) : base(renderer, client)
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap;
        }

        public override void Update(GameTime time)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                switch(gesture.GestureType)
                {
                    case GestureType.Pinch:
                        PinchZoom(gesture);
                        break;
                    case GestureType.Tap:
                        MoveEntity(gesture);
                        break;
                    case GestureType.DoubleTap:
                        Client.Player.Running = true;
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
                        Renderer.EndMove();
                    }
                }
            }
        }

        public void MoveCamera(GestureSample gesture)
        {
            if (!PreviouslyMovingCamera)
            {
                Renderer.BeginMove();
                PreviouslyMovingCamera = true;
                DragPosition = gesture.Position;
            }
            else
            {
                MoveCameraBy(gesture.Position);
            }
        }

        public void MoveEntity(GestureSample gesture)
        {
            var position = Renderer.WorldPosition(gesture.Position);
            Debug.WriteLine("Move entity to: " + position);
            Client.Player.Position = position;
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
