using PureGame.Engine.EntityData;

namespace PureGame.Engine.World.Controllers
{
    public class BaseController : AbstractController
    {
        public override WorldArea CurrentWorld { get; }

        public BaseController(WorldArea world, IEntity entity) : base(entity)
        {
            CurrentWorld = world;
        }
    }
}
