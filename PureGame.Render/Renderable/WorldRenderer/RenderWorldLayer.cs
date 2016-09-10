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

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorldLayer : IRenderable
    {
        public WorldArea World;
        private readonly Dictionary<string, RenderEntity> _entitySprites;
        private readonly TiledMap _map;
        public Camera2D Camera;
        private List<RenderEntity> _toDraw;
        private readonly ContentManager _content;
        private readonly BitmapFont _bitmapFont;
        private readonly ViewportAdapter _viewPort;
        public readonly EntityPositionFinder PositionFinder;
        private readonly EntityObject _player;
        public Stack<IFocusable> FocusStack;
        public IFocusable Focus => FocusStack.Peek();
        public RenderWorldLayer(WorldArea world, ViewportAdapter viewPort, EntityObject player, float zoom=0.25f, string fontName="montserrat-32")
        {
            _player = player;
            _content = ContentManagerManager.RequestContentManager();
            _viewPort = viewPort;
            World = world;
            Camera = new Camera2D(viewPort) {Zoom=zoom};
            _map = world.Map.Map;
            var tileSize = new Vector2(_map.TileWidth, _map.TileHeight);
            PositionFinder = new EntityPositionFinder(world, tileSize);
            _entitySprites = new Dictionary<string, RenderEntity>();
            var fileName = $"Fonts/{fontName}";
            _bitmapFont = _content.Load<BitmapFont>(fileName);
            FocusStack = new Stack<IFocusable>();
            FocusStack.Push(new FocusEntity(_player, PositionFinder));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Camera.LookAt(Focus.Position);
            spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix());
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
            position = Camera.ScreenToWorld(position);
            //we cnvert back and forth to round vector
            var point = (position / PositionFinder.TileSize).ToPoint();
            return point.ToVector2();
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!_entitySprites.ContainsKey(e.Id))
            {
                _entitySprites[e.Id] = new RenderEntity(e, PositionFinder, _content);
            }
            return _entitySprites[e.Id];
        }

        public void RefreshToDraw()
        {
            _toDraw = new List<RenderEntity>();
            var tmpCamera = new Camera2D(_viewPort) { Zoom=Camera.Zoom };
            tmpCamera.LookAt(Focus.FinalPosition);
            foreach (var e in World.Entities)
            {
                var r = GetRenderEntity(e);
                var camerasContains = Camera.Contains(r.Rect) != ContainmentType.Disjoint ||
                                      tmpCamera.Contains(r.Rect) != ContainmentType.Disjoint;
                if (camerasContains)
                {
                    _toDraw.Add(r);
                }
            }
        }
    }
}
