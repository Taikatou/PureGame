using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using PureGame.Engine;
using System.Collections.Generic;
using System;

namespace PureGame.Render.Renderable
{
    public class RenderWorld : IRenderable
    {
        private WorldArea world;
        private Dictionary<string, RenderEntity> entity_sprites;
        private ContentManager Content;
        private TiledMap Map;
        public Vector2 TileSize;
        public Vector2 Offset => TileSize / 2;
        private PlainPureGameRenderer parent;
        public string WorldName
        {
            get
            {
                return world.Name;
            }
        }

        public float rotation = 0.0f;

        public float Rotation
        {
            get
            {
                return rotation + parent.Rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public RenderWorld(WorldArea world, PlainPureGameRenderer parent, ContentManager Content)
        {
            this.parent = parent;
            this.world = world;
            this.Content = Content;
            Map = world.Maps[0].GetTiledMap(Content);
            TileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void Draw(SpriteBatch sprite_batch, Camera2D camera)
        {
            camera.LookAt(GetEntityScreenPosition(world.WorldEntities.Data.Entities[0]) + Offset);
            sprite_batch.Begin(transformMatrix: camera.GetViewMatrix());
            sprite_batch.Draw(Map);
            foreach (var e in world.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                r.Draw(sprite_batch, TileSize);
            }
            sprite_batch.End();
        }

        public void Update(GameTime time)
        {
            foreach (var e in world.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                r.Update(time);
            }
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!entity_sprites.ContainsKey(e.Id))
            {
                var entity_content = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
                entity_sprites[e.Id] = new RenderEntity(world, e, this, entity_content);
            }
            return entity_sprites[e.Id];
        }

        public Vector2 GetEntityScreenPosition(EntityObject entity)
        {
            Vector2 position = entity.Position;
            var EntityToKey = world.WorldEntities.Data.EntityToKey;
            if (EntityToKey.ContainsKey(entity))
            {
                float progress = EntityToKey[entity].Progress;
                position -= (entity.FacingPosition * progress);
            }
            return GetScreenPosition(position);
        }

        public void UnLoad()
        {
            Content.Unload();
        }

        public Vector2 GetScreenPosition(Vector2 pos)
        {
            return pos * TileSize;
        }
    }
}
