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
using System.Diagnostics;
using PureGame.Common;
using PureGame.Render.Common;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorldLayer : IRenderable
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly TiledMap _map;
        public Camera2D Camera;
        private readonly ContainsList<RenderEntity> _toDraw;
        private readonly ContentManager _content;
        private readonly ViewportAdapter _viewPort;
        public readonly EntityPositionFinder PositionFinder;
        private readonly EntityObject _player;
        public Stack<IFocusable> FocusStack;
        public IFocusable Focus => FocusStack.Peek();
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, EntityObject player, float zoom=0.25f)
        {
            _toDraw = new ContainsList<RenderEntity>();
            _player = player;
            _content = ContentManagerManager.RequestContentManager();
            _viewPort = viewPort;
            World = world;
            Camera = new Camera2D(viewPort) {Zoom=zoom};
            _map = world.Map.Map;
            var tileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            PositionFinder = new EntityPositionFinder(world, tileSize);
            _entitySprites = new Dictionary<string, RenderEntity>();
            FocusStack = new Stack<IFocusable>();
            FocusStack.Push(new FocusEntity(_player, PositionFinder));
            foreach (var entity in world.Entities)
            {
                if (entity != player)
                {
                    entity.OnMoveEvent += (sender, args) => RefreshEntity(entity);
                }
            }
            player.OnMoveEvent += (sender, args) => RefreshToDraw();
            RefreshToDraw();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.LookAt(Focus.Position);
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            _map.Draw(transformMatrix);
            foreach (var r in _toDraw.Elements)
            {
                r.Draw(spriteBatch);
            }
            spriteBatch.End();
            // Draw interactions
            if (World.CurrentlyInteracting(_player))
            {

            }
        }

        public void Update(GameTime time)
        {
            foreach (var toDraw in _toDraw.Elements)
            {
                toDraw.Update(time);
            }
        }

        public Vector2 WorldPosition(Vector2 position)
        {
            position = Camera.ScreenToWorld(position);
            //we cnvert back and forth to round vector
            var point = position/PositionFinder.TileSize;
            return point;
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new RenderEntity(e, PositionFinder, _content);
            }
            return _entitySprites[e.Id];
        }

        public void RefreshEntity(EntityObject e)
        {
            var tmpCamera = new Camera2D(_viewPort) { Zoom = Camera.Zoom };
            tmpCamera.LookAt(Focus.FinalPosition);
            var r = GetRenderEntity(e);
            var camerasContains = CameraContains(r.Rect, tmpCamera) ||
                                  CameraContains(r.FinalRect, tmpCamera);
            if (camerasContains)
            {
                _toDraw.Add(r);
            }
        }

        public bool CameraContains(Rectangle r, Camera2D tmpCamera)
        {
            var camerasContains = Camera.Contains(r) != ContainmentType.Disjoint ||
                                  tmpCamera.Contains(r) != ContainmentType.Disjoint;
            return camerasContains;
        }

        public void RefreshToDraw()
        {
            foreach (var e in World.Entities)
            {
                RefreshEntity(e);
            }
        }
    }
}
