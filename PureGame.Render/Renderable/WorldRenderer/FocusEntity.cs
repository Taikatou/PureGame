
using Microsoft.Xna.Framework;
using PureGame.Common;
using PureGame.Engine.EntityData;

namespace PureGame.Render.Renderable.WorldRenderer
{
    class FocusEntity : IFocusable
    {
        public Vector2 Position
        {
            get
            {
                var focus = _positionFinder.GetEntityScreenPosition(_entity);
                return focus.ToVector2() + Offset;
            }
        }

        public Vector2 Offset;
        private readonly EntityObject _entity;
        private readonly EntityPositionFinder _positionFinder;

        public FocusEntity(EntityObject entity, EntityPositionFinder positionFinder)
        {
            _entity = entity;
            _positionFinder = positionFinder;
            Offset = positionFinder.TileSize / 2;
        }
    }
}
