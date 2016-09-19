using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Engine.World.EntityMover;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public TouchScreenController(RenderWorldLayer renderer, PureGameClient client) : base(renderer, client)
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap | GestureType.DragComplete;
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
                        DoubleTap();
                        break;
                    case GestureType.FreeDrag:
                        MoveCamera(gesture);
                        break;
                    case GestureType.DragComplete:
                        Release();
                        break;
                }
            }
        }

        public void Release()
        {
            Renderer.FocusStack.EndMove();
        }

        public void DoubleTap()
        {
            var entityMover = GetEntitymover();
            if (entityMover != null)
            {
                entityMover.Entity.Running = true;
            }
        }

        public void MoveCamera(GestureSample gesture)
        {
            MoveCameraBy(gesture.Position);
        }

        public void Tap(GestureSample gesture)
        {
            var entityMover = GetEntitymover();
            if (entityMover == null || entityMover.Complete)
            {
                Client.Entity.Running = false;
            }
            var touchPosition = Renderer.WorldPosition(gesture.Position);
            MoveEntity(touchPosition);
        }

        public EntityMover GetEntitymover()
        {
            var entityDict = Client.PureGame.EntitiyMover.EntityMoverDict;
            var player = Client.Entity;
            EntityMover toReturn = null;
            if (entityDict.ContainsKey(player))
            {
                toReturn = entityDict[player];
            }
            return toReturn;
        }

        public void MoveEntity(Point endPosition)
        {
            var entity = Client.Entity;
            Client.PureGame.AddEntityMover(entity, endPosition);
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
