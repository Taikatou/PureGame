using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Common;
using PureGame.Engine;
using PureGame.Render.Renderable.WorldRenderer;
using System.Collections.Generic;
using System.Diagnostics;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public List<Point> CurrentPath;
        public Vector2 NextPosition => CurrentPath[0].ToVector2();
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
                        Renderer.FocusStack.EndMove();
                    }
                }
            }
            var player = Client.Player;
            var currentlyMoving = Client.CurrentWorld.EntityManager.EntityCurrentlyMoving(player);
            if (CurrentPath != null && CurrentPath.Count > 0 && !currentlyMoving)
            {
                var directionVector = NextPosition - player.Position;
                Debug.WriteLine("Direction vector " + directionVector);
                var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
                if(direction != Direction.None)
                {
                    Client.MoveDirection(direction);
                    CurrentPath.RemoveAt(0);
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

        public void MoveEntity(GestureSample gesture)
        {
            var endPosition = Renderer.WorldPosition(gesture.Position);
            var player = Client.Player;
            var searchParams = new SearchParameters(player.Position, endPosition, Client.CurrentWorld);
            CurrentPath = PathFinderFactory.FindPath(searchParams);
            foreach(var c in CurrentPath)
            {
                Debug.WriteLine(c);
            }
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
