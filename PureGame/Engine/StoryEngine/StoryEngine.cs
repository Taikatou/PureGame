using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.World;

namespace PureGame.Engine.StoryEngine
{
    public class StoryEngine
    {
        private readonly Dictionary<WorldArea, List<IStoryController>> _controllers;
        private readonly List<WorldArea> _worlds;
        public StoryEngine()
        {
            _controllers = new Dictionary<WorldArea, List<IStoryController>>();
            _worlds = new List<WorldArea>();
        }

        public void AddStoryController(IStoryController controller, WorldArea world)
        {
            if (!_controllers.ContainsKey(world))
            {
                _controllers[world] = new List<IStoryController>();
                _worlds.Add(world);
            }
            _controllers[world].Add(controller);
        }

        public void Update(GameTime time)
        {
            foreach (var world in _worlds)
            {
                foreach (var controller in _controllers[world])
                {
                    controller.Update(time);
                }
            }
        }
    }
}
