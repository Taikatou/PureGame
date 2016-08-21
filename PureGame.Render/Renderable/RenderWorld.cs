using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using System.Collections.Generic;

namespace PureGame.Render.Renderable
{
    public class RenderWorld
    {
        public WorldArea World;
        private Dictionary<string, RenderEntity> entity_sprites;
        private ContentManager Content;
        private TiledMap Map;
        public Vector2 TileSize;
        public Vector2 Offset => TileSize / 2;

        public static IEntity FocusEntity { get; internal set; }

        public RenderWorld(WorldArea World)
        {
            this.World = World;
            Content = ContentManagerManager.RequestContentManager();
            Map = World.Map.GetTiledMap(Content);
            TileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void Draw(SpriteBatch sprite_batch, Camera2D camera)
        {
            Point focus = GetEntityScreenPosition(FocusEntity);
            camera.LookAt(focus.ToVector2() + Offset);
            sprite_batch.Begin(transformMatrix: camera.GetViewMatrix());
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

        public RenderEntity GetRenderEntity(IEntity e)
        {
            if (!entity_sprites.ContainsKey(e.Id))
            {
                entity_sprites[e.Id] = new RenderEntity(e, this, Content);
            }
            return entity_sprites[e.Id];
        }

        public Point GetEntityScreenPosition(IEntity entity)
        {
            Vector2 position = entity.Position;
            var WorldData = World.Data;
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
