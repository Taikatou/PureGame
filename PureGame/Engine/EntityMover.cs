using Microsoft.Xna.Framework;
using SmallGame.GameObjects;

namespace PureGame.Engine
{
    public class EntityMover : GameObject
    {
        private EntityObject entity;
        public EntityObject Entity
        {
            get
            {
                return entity;
            }
        }
        public EntityMover(EntityObject entity)
        {
            this.entity = entity;
        }

        public void Update(GameTime timer)
        {

        }

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
