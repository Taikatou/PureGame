using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PureGame.Render.Controlables;

namespace PureGame.Render.Controllers
{
    public class TouchScreenController : CameraController
    {
        private readonly List<GestureSample> _gestures;
        public TouchScreenController()
        {
            TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag | GestureType.DoubleTap | GestureType.Tap | GestureType.DragComplete;
            _gestures = new List<GestureSample>();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            _gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                _gestures.Add(gesture);
            }
        }

        public override void UpdateLayer(GameTime time, IControlableLayer layer)
        {
            foreach(var gesture in _gestures)
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

        public void DoubleTap(IControlableLayer layer)
        {
            layer.DoubleTap();
        }

        public void Drag(IControlableLayer layer, GestureSample gesture)
        {
            Drag(gesture.Position, layer);
        }

        public void Tap(GestureSample gesture, IControlableLayer layer)
        {
            layer.Tap(gesture.Position);
        }

        public void Pinch(IControlableLayer layer, GestureSample gesture)
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
