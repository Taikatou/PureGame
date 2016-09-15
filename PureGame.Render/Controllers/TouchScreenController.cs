using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Common;
using PureGame.Engine;
using PureGame.Render.Renderable.WorldRenderer;
using System.Collections.Generic;
using System.Diagnostics;
using PureGame.Common.PathFinding;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public List<Point> CurrentPath;
        public bool Running;
        public bool InteractAfter;
        public Point NextPosition => CurrentPath[0];
        public TouchScreenController(RenderWorldLayer renderer, PureGameClient client) : base(renderer, client)
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap;
        }

        public void TouchUpdate()
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

        public override void Update(GameTime time)
        {
            TouchUpdate();
            var player = Client.Player;
            var currentlyMoving = Client.CurrentWorld.EntityManager.EntityCurrentlyMoving(player);
            if (CurrentPath != null && CurrentPath.Count > 0 && !currentlyMoving)
            {
                var directionVector = NextPosition - player.Position;
                var direction = DirectionMapper.GetDirectionFromMovment(directionVector);
                if(direction != Direction.None)
                {
                    Client.Player.Running = Running;
                    Debug.WriteLine("Running: " + Client.Player.Running);
                    Client.MoveDirection(direction);
                }
                CurrentPath.RemoveAt(0);
                if (CurrentPath.Count == 0)
                {
                    if (Running)
                    {
                        Running = false;
                    }
                    if (InteractAfter)
                    {
                        Client.ControllerA();
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
            var player = Client.Player;
            var searchParams = new SearchParameters(player.Position, endPosition, Client.CurrentWorld);
            var pathFinder = PathFinderFactory.MakePathFinder(searchParams);
            CurrentPath = pathFinder.FindPath();
            foreach (var v in CurrentPath)
            {
                Debug.WriteLine(v.ToString());
            }
            InteractAfter = Client.CurrentWorld.HasEntity(endPosition);
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
