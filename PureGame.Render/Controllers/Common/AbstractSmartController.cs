using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Client.Controlables;

namespace PureGame.Client.Controllers.Common
{
    public abstract class AbstractSmartController<T> : IController where T : SmartControl
    {
        public List<T> SmartControls;
        public List<T> DirectionalControls;
        public T CachedButton;
        public T BButton;
        public T EButton;
        public abstract void Update(GameTime time);

        protected AbstractSmartController()
        {
            SmartControls = new List<T>();
            DirectionalControls = new List<T>();
        }

        public void AddDirectionalControl(T controller)
        {
            DirectionalControls.Add(controller);
            SmartControls.Add(controller);
        }

        public virtual bool UpdateLayer(GameTime time, IControlableLayer layer)
        {
            var found = false;
            if (EButton.NewActive)
            {
                found = layer.Interact();
            }
            var d = GetMovementDirection();
            if (d != Direction.None)
            {
                layer.ControllerDPad(d);
            }
            if (BButton.Change)
            {
                var bActive = BButton.Active;
                layer.Cancel(bActive);
            }
            return found;
        }

        public Direction GetMovementDirection()
        {
            // Return cached direction
            if (CachedButton != null && CachedButton.Active)
            {
                return (Direction)CachedButton.Control;
            }
            CachedButton = null;
            // Else look for another button
            foreach (var button in DirectionalControls)
            {
                if (button.Active)
                {
                    CachedButton = button;
                    return (Direction)button.Control;
                }
            }
            // Else return false
            return Direction.None;
        }
    }
}
