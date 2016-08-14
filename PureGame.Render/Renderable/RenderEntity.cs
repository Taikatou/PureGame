using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Render.Animate;
using PureGame.Render.Common;

namespace PureGame.Render.Renderable
{
    public class RenderEntity : IRenderable
    {
        private EntityObject base_entity;
        private Texture2D entity_texture;
        private WorldArea world;
        private RenderWorld parent;
        private Animation walkDown;
        private Animation currentAnimation;
        public RenderEntity(WorldArea world, EntityObject base_entity, RenderWorld parent, ContentManager content)
        {
            this.parent = parent;
            this.world = world;
            this.base_entity = base_entity;
            entity_texture = Loader.LoadTexture(content, base_entity.FileName);

        }
        private float rotation = 0.0f;

        public float Rotation
        {
            get
            {
                return rotation + base_entity.Rotation + parent.Rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public void Draw(SpriteBatch sprite_batch, Vector2 size)
        {
            Vector2 position_on_screen = parent.GetEntityPosition(base_entity, size);
            Rectangle rect = new Rectangle(position_on_screen.ToPoint(), size.ToPoint());
            sprite_batch.Draw(entity_texture, rect, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
