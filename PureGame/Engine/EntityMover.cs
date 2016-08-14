using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using SmallGame;

namespace PureGame.Engine
{
    public class EntityMover
    {
        public EntityObject Entity;
        private IController controller;
        public static int EntityMoverID = 0;
        public EntityMover(EntityObject Entity, IController controller) : base()
        {
            EntityMoverID++;
            this.Entity = Entity;
            this.controller = controller;
        }

        public void Update(GameTime time)
        {
            controller.Update(Entity, time);
        }

        public int Speed => controller.Speed;
    }
}
