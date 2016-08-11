using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;

namespace PureGame.Render
{
    public class RenderEntity
    {
        private EntityObject base_entity;
        private Texture2D[] entity_texture;
        public RenderEntity(EntityObject base_entity, ContentManager content)
        {
            this.base_entity = base_entity;
            entity_texture = new Texture2D[4];
            string path_name = string.Format("Images/{0}", base_entity.FileName);
            string[] files = { "Right", "Left", "Up", "Down" };
            for (int i = 0; i < entity_texture.Length; i++)
            {
                string file_path_name = path_name + "/" + files[i];
                Texture2D texture = content.Load<Texture2D>(file_path_name);
                entity_texture[i] = texture;
            }
        }

        public void Draw(SpriteBatch sprite_batch, Rectangle rect)
        {
            Texture2D texture = entity_texture[0];
            sprite_batch.Draw(texture, rect, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
