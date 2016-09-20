using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Engine.World.EntityMover;
using PureGame.Render.ControlLayers;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public TouchScreenController()
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap | GestureType.DragComplete;
        }

        public override void Update(GameTime time, IControlLayer layer)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                switch (gesture.GestureType)
                {
                    case GestureType.Pinch:
                        Pinch(layer, gesture);
                        break;
                    case GestureType.Tap:
                        Tap(gesture, layer);
                        break;
                    case GestureType.DoubleTap:
                        DoubleTap(layer);
                        break;
                    case GestureType.FreeDrag:
                        Drag(layer, gesture);
                        break;
                    case GestureType.DragComplete:
                        ReleaseDrag(layer);
                        break;
                }
            }
        }

        public void DoubleTap(IControlLayer layer)
        {
            layer.DoubleTap();
        }

        public void Drag(IControlLayer layer, GestureSample gesture)
        {
            Drag(gesture.Position, layer);
        }

        public void Tap(GestureSample gesture, IControlLayer layer)
        {
            layer.Tap(gesture.Position);
        }

        public void Pinch(IControlLayer layer, GestureSample gesture)
        {
            var dist = Vector2.Distance(gesture.Position, gesture.Position2);

            var aOld = gesture.Position - gesture.Delta;
            var bOld = gesture.Position2 - gesture.Delta2;
            var distOld = Vector2.Distance(aOld, bOld);

            var scale = (distOld - dist) / 500f;
            Zoom(scale, layer);
        }
    }
}
