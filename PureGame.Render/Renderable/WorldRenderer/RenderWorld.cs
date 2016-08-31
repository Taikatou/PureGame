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
    public class RenderWorld : IRenderable
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly ContentManager _content;
        private readonly TiledMap _map;
        public Vector2 TileSize;
        public Vector2 Offset => TileSize / 2;
        public EntityObject FocusEntity;
        private readonly Camera2D _camera;
        private List<RenderEntity> _toDraw;
        public RenderWorld(WorldArea world, ViewportAdapter viewPort, EntityObject focusEntity)
        {
            FocusEntity = focusEntity;
            World = world;
            _camera = new Camera2D(viewPort) {Zoom = 0.25f};
            _content = ContentManagerManager.RequestContentManager();
            _map = world.Map.Map;
            TileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            _entitySprites = new Dictionary<string, RenderEntity>();
            RefreshToDraw();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Point focus = GetEntityScreenPosition(FocusEntity);
            _camera.LookAt(focus.ToVector2() + Offset);
            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
            spriteBatch.Draw(_map);
            foreach(RenderEntity r in _toDraw)
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
            foreach (RenderEntity r in _toDraw)
            {
                r.Update(time);
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
            Vector2 position = entity.Position;
            var worldData = World.EntityManager;
            if (worldData.EntityCurrentlyMoving(entity))
            {
                float progress = worldData.EntityToKey[entity].Progress;
                var facingPosition = DirectionMapper.GetMovementFromDirection(entity.FacingDirection);
                position -= facingPosition * progress;
            }
            return GetScreenPosition(position);
        }

        public void UnLoad()
        {
            _content.Unload();
        }

        public Point GetScreenPosition(Vector2 pos)
        {
            Vector2 position = pos * TileSize;
            return position.ToPoint();
        }

        public void RefreshToDraw()
        {
            _toDraw = new List<RenderEntity>();
            foreach(EntityObject e in World.EntityManager.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                if(_camera.Contains(r.Rect) != ContainmentType.Disjoint)
                {
                    _toDraw.Add(r);
                }
            }
        }
    }
}
