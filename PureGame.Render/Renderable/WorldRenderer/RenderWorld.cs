using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using System.Collections.Generic;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class RenderWorld : IRenderable
    {
        public WorldArea World;
        private Dictionary<string, RenderEntity> entity_sprites;
        private ContentManager Content;
        private TiledMap Map;
        public Vector2 TileSize;
        public Vector2 Offset => TileSize / 2;
        public EntityObject FocusEntity;
        private Camera2D Camera;
        public RenderWorld(WorldArea World, ViewportAdapter ViewPort, EntityObject FocusEntity)
        {
            this.FocusEntity = FocusEntity;
            this.World = World;
            Camera = new Camera2D(ViewPort);
            Camera.Zoom = 0.25f;
            Content = ContentManagerManager.RequestContentManager();
            Map = World.Map.GetTiledMap(Content);
            TileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Point focus = GetEntityScreenPosition(FocusEntity);
            Camera.LookAt(focus.ToVector2() + Offset);
            sprite_batch.Begin(transformMatrix: Camera.GetViewMatrix());
            sprite_batch.Draw(Map);
            foreach (var e in World.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                r.Draw(sprite_batch);
            }
            sprite_batch.End();
        }

        public void Update(GameTime time)
        {
            foreach (var e in World.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                r.Update(time);
            }
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!entity_sprites.ContainsKey(e.Id))
            {
                entity_sprites[e.Id] = new RenderEntity(e, this, Content);
            }
            return entity_sprites[e.Id];
        }

        public Point GetEntityScreenPosition(EntityObject entity)
        {
            Vector2 position = entity.Position;
            var WorldData = World.EntityManager;
            if (WorldData.EntityCurrentlyMoving(entity))
            {
                float progress = WorldData.EntityToKey[entity].Progress;
                var FacingPosition = DirectionMapper.GetMovementFromDirection(entity.FacingDirection);
                position -= (FacingPosition * progress);
            }
            return GetScreenPosition(position);
        }

        public void UnLoad()
        {
            Content.Unload();
        }

        public Point GetScreenPosition(Vector2 pos)
        {
            Vector2 position = pos * TileSize;
            return position.ToPoint();
        }
    }
}
