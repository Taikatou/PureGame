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
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, EntityObject focusEntity, ContentManager content)
        {
            FocusEntity = focusEntity;
            World = world;
            _camera = new Camera2D(viewPort) {Zoom = 0.25f};
            _content = content;
            _map = world.Map.Map;
            TileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            _entitySprites = new Dictionary<string, RenderEntity>();
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
