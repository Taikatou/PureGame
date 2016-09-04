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

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorldLayer : IRenderable
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly TiledMap _map;
        public Vector2 TileSize;
        public Vector2 Offset => TileSize / 2;
        public EntityObject FocusEntity;
        private readonly Camera2D _camera;
        private List<RenderEntity> _toDraw;
        private readonly ContentManager _content;
        private readonly BitmapFont _bitmapFont;
        private readonly ViewportAdapter _viewPort;
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, EntityObject focusEntity, ContentManager content, string fontName="montserrat-32")
        {
            FocusEntity = focusEntity;
            _viewPort = viewPort;
            World = world;
            _camera = new Camera2D(viewPort) {Zoom = 0.25f};
            _content = content;
            _map = world.Map.Map;
            TileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            _entitySprites = new Dictionary<string, RenderEntity>();
            string fileName = $"Fonts/{fontName}";
            _bitmapFont = _content.Load<BitmapFont>(fileName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var focus = GetEntityScreenPosition(FocusEntity);
            _camera.LookAt(focus.ToVector2() + Offset);
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            spriteBatch.Draw(_map);
            foreach(var r in _toDraw)
            {
                r.Draw(spriteBatch);
            }
            spriteBatch.End();
            // Draw interactions
            if (World.CurrentlyInteracting(FocusEntity))
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
            Point point = (position / TileSize).ToPoint();
            return point.ToVector2();
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new RenderEntity(e, this, _content);
            }
            return _entitySprites[e.Id];
        }

        public Point GetEntityScreenPosition(EntityObject entity)
        {
            var position = entity.Position;
            var worldData = World.EntityManager;
            if (worldData.EntityCurrentlyMoving(entity))
            {
                var progress = worldData.EntityToKey[entity].Progress;
                var facingPosition = DirectionMapper.GetMovementFromDirection(entity.FacingDirection);
                position -= facingPosition * progress;
            }
            return GetScreenPosition(position);
        }

        public Point GetScreenPosition(Vector2 pos)
        {
            var position = pos * TileSize;
            return position.ToPoint();
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
    }
}
