using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Render.Controllers
{
    public class ControllerManager
    {
        public List<IController> Controllers => EnabledControllers;
        public List<IController> EnabledControllers;
        public List<IController> AllControllers;

        public ControllerManager()
        {
            EnabledControllers = new List<IController>();
            AllControllers = new List<IController>();
        }

        public void Add(IController controller)
        {
            EnabledControllers.Add(controller);
            AllControllers.Add(controller);
        }

        public void Update(GameTime time)
        {
            foreach (var controller in EnabledControllers)
            {
                controller.Update(time);
            }
        }

        public void EnableController(bool enable, IController controller)
        {
            var needToAdd = EnabledControllers.Contains(controller) == !enable;
            if (needToAdd && AllControllers.Contains(controller))
            {
                if (enable)
                {
                    EnabledControllers.Add(controller);
                }
                else
                {
                    EnabledControllers.Remove(controller);
                }
            }
        }
    }
}
