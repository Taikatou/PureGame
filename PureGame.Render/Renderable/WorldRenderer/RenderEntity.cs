﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Animate;
using PureGame.Render.Common;
using System;
using System.Diagnostics;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderEntity
    {
        private readonly EntityObject _baseEntity;
        private readonly Texture2D _entityTexture;
        private RenderWorld _parent;
        //Right Left Up Down
        private Animation[] _walking;
        private Animation[] _standing;

        private bool _standingTimer;

        private Animation _currentAnimation;
        private Vector2 _previousPosition;
        public RenderEntity(EntityObject baseEntity, RenderWorld parent, ContentManager content)
        {
            _parent = parent;
            _baseEntity = baseEntity;
            _entityTexture = AssetLoader.LoadTexture(content, baseEntity.FileName);
            _walking = new Animation[4];
            _walking[(int)Direction.Down] = new Animation();
            _walking[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Down].AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Down].AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Up] = new Animation();
            _walking[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Up].AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Up].AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _walking[(int)Direction.Left] = new Animation();
            _walking[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Left].AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Left].AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _walking[(int)Direction.Right] = new Animation();
            _walking[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Right].AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _walking[(int)Direction.Right].AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _standing = new Animation[4];
            _standing[(int)Direction.Down] = new Animation();
            _standing[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _standing[(int)Direction.Up] = new Animation();
            _standing[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _standing[(int)Direction.Left] = new Animation();
            _standing[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

            _standing[(int)Direction.Right] = new Animation();
            _standing[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            _previousPosition = baseEntity.Position;
            GetAnimation();
        }

        private Point OnScreen => _parent.GetEntityScreenPosition(_baseEntity);
        public Rectangle Rect => new Rectangle(OnScreen, _parent.TileSize.ToPoint());

        public void Draw(SpriteBatch spriteBatch)
        {
            var sourceRectangle = _currentAnimation.CurrentRectangle;
            spriteBatch.Draw(_entityTexture, Rect, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void Update(GameTime time)
        {
            GetAnimation();
            _currentAnimation.Update(time);
        }

        public void GetAnimation()
        {
            Point screenPosition = _parent.GetScreenPosition(_previousPosition);
            Point entityPosition = _parent.GetEntityScreenPosition(_baseEntity);
            int direction = (int)_baseEntity.FacingDirection;
            if (screenPosition == entityPosition)
            {
                //standing
                if (_standingTimer)
                {
                    _currentAnimation = _standing[direction];
                }
                else
                {
                    _standingTimer = true;
                    Debug.WriteLine("Start standing");
                }
            }
            else
            {
                //moving
                if (_standingTimer)
                {
                    _standingTimer = false;
                    Debug.WriteLine("Start walking");
                }
                else
                {
                    _currentAnimation = _walking[direction];
                }
            }
            _previousPosition = _baseEntity.Position;
        }
    }
}