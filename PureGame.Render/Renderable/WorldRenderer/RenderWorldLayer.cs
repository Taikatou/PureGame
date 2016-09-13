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
using PureGame.Render.Common;
using System.Linq;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorldLayer : RenderLayer
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly TiledMap _map;
        public Camera2D Camera;
        private ContainsList<RenderEntity> _toDraw;
        private readonly ContentManager _content;
        private readonly ViewportAdapter _viewPort;
        public readonly EntityPositionFinder PositionFinder;
        public FocusStack FocusStack;
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, Entity player, float zoom=0.25f)
        {
            _toDraw = new ContainsList<RenderEntity>();
            _content = ContentManagerManager.RequestContentManager();
            _viewPort = viewPort;
            World = world;
            Camera = new Camera2D(viewPort) { Zoom=zoom };
            _map = world.Map.Map;
            var tileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            PositionFinder = new EntityPositionFinder(world, tileSize);
            _entitySprites = new Dictionary<string, RenderEntity>();
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            _map.Draw(transformMatrix);
            foreach (var r in _toDraw.Elements)
            {
                r.Draw(spriteBatch);
            }
            foreach (var r in _toDraw.Elements)
            {
                if(r.BaseEntity.Talking)
                {
                    //draw text boxes
                }
            }
            spriteBatch.End();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            foreach (var toDraw in _toDraw.Elements)
            {
                toDraw.Update(time);
            }
            Camera.LookAt(FocusStack.Focus.Position);
        }

        public Vector2 WorldPosition(Vector2 position)
        {
            position = Camera.ScreenToWorld(position);
            var point = (position/PositionFinder.TileSize).ToPoint();
            return point.ToVector2();
        }

        public RenderEntity GetRenderEntity(Entity e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new RenderEntity(e, PositionFinder, _content);
            }
            return _entitySprites[e.Id];
        }

        public void RefreshEntity(Entity e)
        {
            var tmpCamera = new Camera2D(_viewPort) { Zoom = Camera.Zoom };
            tmpCamera.LookAt(FocusStack.Focus.FinalPosition);
            var renderEntity = GetRenderEntity(e);
            var camerasContains = CameraContains(renderEntity.Rect, tmpCamera) ||
                                  CameraContains(renderEntity.FinalRect, tmpCamera);
            _toDraw.AddOrRemove(renderEntity, camerasContains);
        }

        public bool CameraContains(Rectangle r, Camera2D tmpCamera)
        {
            var cameraContains = Camera.Contains(r) != ContainmentType.Disjoint ||
                                 tmpCamera.Contains(r) != ContainmentType.Disjoint;
            return cameraContains;
        }

        public void Sort()
        {
            _toDraw.Elements = _toDraw.Elements.OrderBy(x => x.BaseEntity.Position.Y).ToList();
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
