using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using System.Collections.Generic;
using MonoGame.Extended.BitmapFonts;
using PureGame.Common;
using System;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorldLayer : IRenderable
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly TiledMap _map;
        private readonly Camera2D _camera;
        private List<RenderEntity> _toDraw;
        private readonly ContentManager _content;
        private readonly BitmapFont _bitmapFont;
        private readonly ViewportAdapter _viewPort;
        private readonly EntityPositionFinder _positionFinder;
        private readonly EntityObject _player;
        private readonly Stack<IFocusable> _focus;
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, EntityObject player, string fontName="montserrat-32")
        {
            _player = player;
            _content = ContentManagerManager.RequestContentManager();
            _viewPort = viewPort;
            World = world;
            _camera = new Camera2D(viewPort) {Zoom = 0.25f};
            _map = world.Map.Map;
            var tileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            _positionFinder = new EntityPositionFinder(world, tileSize);
            _entitySprites = new Dictionary<string, RenderEntity>();
            string fileName = $"Fonts/{fontName}";
            _bitmapFont = _content.Load<BitmapFont>(fileName);
            _focus = new Stack<IFocusable>();
            _focus.Push(new FocusEntity(_player, _positionFinder));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _camera.LookAt(_focus.Peek().Position);
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            spriteBatch.Draw(_map);
            foreach(var r in _toDraw)
            {
                r.Draw(spriteBatch);
            }
            spriteBatch.End();
            // Draw interactions
            if (World.CurrentlyInteracting(_player))
            {
                var text = "Interacting";
                var stringSize = _bitmapFont.MeasureString(text);
                var textPosition = new Vector2(_viewPort.ViewportWidth / 2.0f, _viewPort.ViewportHeight * 0.8f);
                textPosition.X -= stringSize.X / 2;
                spriteBatch.Begin();
                spriteBatch.DrawString(_bitmapFont, text, textPosition, Color.Black);
                spriteBatch.End();
            }
        }

        internal void MoveFocus(Vector2 focusVector)
        {
            _focus.Push(new FocusVector(_focus.Peek().Position + focusVector));
        }

        public void Update(GameTime time)
        {
            if (World.Updated)
            {
                World.Updated = false;
                RefreshToDraw();
            }
            foreach (var toDraw in _toDraw)
            {
                toDraw.Update(time);
            }
        }

        public Vector2 WorldPosition(Vector2 position)
        {
            position = _camera.ScreenToWorld(position);
            //we cnvert back and forth to round vector
            var point = (position / _positionFinder.TileSize).ToPoint();
            return point.ToVector2();
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new RenderEntity(e, _positionFinder, _content);
            }
            return _entitySprites[e.Id];
        }

        public void RefreshToDraw()
        {
            _toDraw = new List<RenderEntity>();
            foreach(var e in World.EntityManager.Entities)
            {
                var r = GetRenderEntity(e);
                if(_camera.Contains(r.Rect) != ContainmentType.Disjoint)
                {
                    _toDraw.Add(r);
                }
            }
        }

        public void ChangeFocus(IFocusable focus) => _focus.Push(focus);

        public void ReleaseFocus()
        {
            if (_focus.Count > 1)
            {
                _focus.Pop();
            }   
        }
    }
}
