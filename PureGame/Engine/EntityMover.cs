using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using SmallGame;

namespace PureGame.Engine
{
    public class EntityMover : GameObject
    {
        private EntityObject entity;
        private IController controller;
        public static int EntityMoverID = 0;
        public EntityObject Entity
        {
            get
            {
                return entity;
            }
        }
        public EntityMover(EntityObject entity, IController controller) : base()
        {
            Type = "EntityMover";
            Id = "EntityMover-" + EntityMoverID;
            EntityMoverID++;
            this.entity = entity;
            this.controller = controller;
        }

        public EntityMover()
        {

        }

        public void Update(GameTime time)
        {
            controller.Update(entity, time);
        }

        public int Speed => controller.Speed;

        public Vector2 Position
        {
            get
            {
                return entity.Position;
            }
            set
            {
                entity.Position = value;
            }
        }
    }
}
