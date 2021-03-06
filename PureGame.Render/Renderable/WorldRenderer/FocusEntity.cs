﻿using Microsoft.Xna.Framework;
using PureGame.Common;
using PureGame.Engine.EntityData;

namespace PureGame.Client.Renderable.WorldRenderer
{
    public class FocusEntity : IFocusable
    {
        private readonly IEntity _entity;
        private readonly EntityPositionFinder _positionFinder;
        public Vector2 Position
        {
            get
            {
                var focus = _positionFinder.GetEntityScreenPosition(_entity);
                return focus.ToVector2() + _positionFinder.Offset;
            }
        }

        public Vector2 FinalPosition
        {
            get
            {
                var focus = _positionFinder.GetScreenPosition(_entity.Position);
                return focus.ToVector2() + _positionFinder.Offset;
            }
        }

        public FocusEntity(IEntity entity, EntityPositionFinder positionFinder)
        {
            _entity = entity;
            _positionFinder = positionFinder;
        }
    }
}
