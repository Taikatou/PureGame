using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Render.ControlLayers;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        public TouchScreenController()
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap | GestureType.DragComplete;
        }

        public override void Update(GameTime time, List<IControlAbleLayer> layers)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                foreach (var layer in layers)
                {
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
        }

        public void DoubleTap(IControlAbleLayer layer)
        {
            layer.DoubleTap();
        }

        public void Drag(IControlAbleLayer layer, GestureSample gesture)
        {
            Drag(gesture.Position, layer);
        }

        public void Tap(GestureSample gesture, IControlAbleLayer layer)
        {
            layer.Tap(gesture.Position);
        }

        public void Pinch(IControlAbleLayer layer, GestureSample gesture)
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
