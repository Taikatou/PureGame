using Microsoft.Xna.Framework;
using PureGame.AbstractEngine;

namespace PureGame.Engine
{
    public class Mover : AbstractMover
    {
        AbstractEntityObject entity;
        public Mover(AbstractEntityObject entity)
        {
            this.entity = entity;
        }

        public override void Update(GameTime timer)
        {

        }
    }
}
