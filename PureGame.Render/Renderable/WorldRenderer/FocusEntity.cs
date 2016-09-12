using Microsoft.Xna.Framework;
using PureGame.Common;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable.WorldRenderer
{
    class FocusEntity : IFocusable
    {
        private readonly Entity _entity;
        private readonly EntityPositionFinder _positionFinder;
        public Vector2 Position
        {
            get
            {
                var focus = _positionFinder.GetEntityScreenPosition(_entity);
                return focus + _positionFinder.Offset;
            }
        }

        public Vector2 FinalPosition
        {
            get
            {
                var focus = _positionFinder.GetScreenPosition(_entity.Position);
                return focus + _positionFinder.Offset;
            }
        }

        public FocusEntity(Entity entity, EntityPositionFinder positionFinder)
        {
            _entity = entity;
            _positionFinder = positionFinder;
        }
    }
}
