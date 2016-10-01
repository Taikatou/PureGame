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
using System;
using PureGame.Client.Events;

namespace PureGame.Client.Renderable.WorldRenderer
{
    public class WorldRenderLayer : IUnSubscribe, IDisposable
    {
        public WorldArea World;
        private readonly Dictionary<string, EntityRender> _entitySprites;
        private readonly TiledMap _map;
        public readonly Camera2D Camera;
        public readonly ContainsList<EntityRender> ToDraw;
        private readonly ContentManager _content;
        private readonly Camera2D _tmpCamera;
        public readonly EntityPositionFinder PositionFinder;
        public FocusStack FocusStack;
        public EventManager EventManager;
        public WorldRenderLayer(WorldArea world, ViewportAdapter viewPort, IEntity player, float zoom)
        {
            EventManager = new EventManager();
            ToDraw = new ContainsList<EntityRender>();
            _content = ContentManagerManager.RequestContentManager();
            World = world;
            _tmpCamera = new Camera2D(viewPort) { Zoom=zoom };
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
                    EventHandler onMoveHandler = (sender, args) =>
                    {
                        RefreshEntity(entity);
                        Sort();
                    };
                    EventManager.AddEvent(entity.OnMoveEvent, onMoveHandler);
                }
            }
            EventHandler refreshEvent = (sender, args) => RefreshToDraw();
            EventHandler playerOnMoveHandler = (sender, args) => RefreshToDraw();
            EventManager.AddEvent(FocusStack.RefreshEvent, refreshEvent);
            EventManager.AddEvent(player.OnMoveEvent, playerOnMoveHandler);
            RefreshToDraw();
        }

        public void UnLoad()
        {
            _content.Unload();
            UnSubscribe();
            Dispose();
        }

        public void Dispose()
        {
            _content.Dispose();
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
            position = ScreenToWorld(position);
            var tileSize = PositionFinder.TileSize;
            var point = (position/tileSize).ToPoint();
            return point;
        }

        public Vector2 ScreenToWorld(Vector2 position)
        {
            return Camera.ScreenToWorld(position);
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
            _tmpCamera.Zoom = Camera.Zoom;
            _tmpCamera.LookAt(FocusStack.Focus.FinalPosition);
            var renderEntity = GetRenderEntity(e);
            var camerasContains = CameraFunctions.CamerasContains(renderEntity.Rect, Camera, _tmpCamera);
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

        public void UnSubscribe()
        {
            EventManager.UnSubscribe();
        }
    }
}
