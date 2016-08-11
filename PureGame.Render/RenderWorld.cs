using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using System.Collections.Generic;

namespace PureGame.Render
{
    public class RenderWorld
    {
        private WorldArea world;
        private Dictionary<string, RenderEntity> entity_sprites;
        private ContentManager content;
        public string WorldName
        {
            get
            {
                return world.Name;
            }
        }
        public RenderWorld(WorldArea world, ContentManager content)
        {
            this.world = world;
            this.content = content;
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            foreach(var e in world.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                Rectangle rect = new Rectangle(e.Position.ToPoint(), new Point(200, 200));
                r.Draw(sprite_batch, rect);
            }
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!entity_sprites.ContainsKey(e.Id))
            {
                var entity_content = new ContentManager(content.ServiceProvider, content.RootDirectory);
                entity_sprites[e.Id] = new RenderEntity(e, entity_content);
            }
            return entity_sprites[e.Id];
        }

        public void UnLoad()
        {

        }
    }
}
