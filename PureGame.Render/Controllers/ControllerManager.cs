using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PureGame.Render.Controllers
{
    public class ControllerManager
    {
        public List<IController> Controllers;

        public ControllerManager()
        {
            Controllers = new List<IController>();
        }

        public void Add(IController controller)
        {
            Controllers.Add(controller);
        }

        public void Update(GameTime time)
        {
            foreach (var controller in Controllers)
            {
                controller.Update(time);
            }
        }
    }
}
