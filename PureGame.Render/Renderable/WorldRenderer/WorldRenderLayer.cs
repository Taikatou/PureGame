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
using System.Linq;
using PureGame.Common;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class WorldRenderLayer
    {
        public WorldArea World;
        private readonly Dictionary<string, EntityRender> _entitySprites;
        private readonly TiledMap _map;
        public Camera2D Camera;
        public readonly ContainsList<EntityRender> ToDraw;
        private readonly ContentManager _content;
        private readonly ViewportAdapter _viewPort;
        public readonly EntityPositionFinder PositionFinder;
        public FocusStack FocusStack;
        public WorldRenderLayer(WorldArea world, ViewportAdapter viewPort, IEntity player, float zoom)
        {
            ToDraw = new ContainsList<EntityRender>();
            _content = ContentManagerManager.RequestContentManager();
            _viewPort = viewPort;
            World = world;
            Camera = new Camera2D(viewPort) { Zoom=zoom };
            _map = world.Map.Map;
            var tileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            PositionFinder = new EntityPositionFinder(world, tileSize);
            _entitySprites = new Dictionary<string, EntityRender>();
            FocusStack = new FocusStack(Camera);
            FocusStack.Push(new FocusEntity(player, PositionFinder));
            foreach (var entity in world.Entities)
            {
                if (entity != player)
                {
                    entity.OnMoveEvent += (sender, args) =>
                    {
                        RefreshEntity(entity);
                        Sort();
                    };
                }
            }
            FocusStack.RefreshEvent += (sender, args) => RefreshToDraw();
            player.OnMoveEvent += (sender, args) => RefreshToDraw();
            RefreshToDraw();
        }

        public void UnLoad()
        {
            _content.Unload();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            _map.Draw(transformMatrix);
            foreach (var r in ToDraw.Elements)
            {
                r.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void Update(GameTime time)
        {
            foreach (var toDraw in ToDraw.Elements)
            {
                toDraw.Update(time);
            }
            Camera.LookAt(FocusStack.Focus.Position);
        }

        public Point WorldPosition(Vector2 position)
        {
            position = Camera.ScreenToWorld(position);
            var tileSize = PositionFinder.TileSize;
            var point = (position/tileSize).ToPoint();
            return point;
        }

        public EntityRender GetRenderEntity(IEntity e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new EntityRender(e, PositionFinder, _content);
            }
            return _entitySprites[e.Id];
        }

        public void RefreshEntity(IEntity e)
        {
            var tmpCamera = new Camera2D(_viewPort) { Zoom = Camera.Zoom };
            tmpCamera.LookAt(FocusStack.Focus.FinalPosition);
            var renderEntity = GetRenderEntity(e);
            var camerasContains = CameraFunctions.CamerasContains(renderEntity.Rect, Camera, tmpCamera);
            ToDraw.AddOrRemove(renderEntity, camerasContains);
        }

        public void Sort()
        {
            ToDraw.Elements = ToDraw.Elements.OrderBy(x => x.BaseEntity.Position.Y).ToList();
        }

        public void RefreshToDraw()
        {
            foreach (var e in World.Entities)
            {
                RefreshEntity(e);
                Sort();
            }
        }
    }
}
