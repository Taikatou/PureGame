using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Render.Animate;
using PureGame.Render.Common;
using System;
using System.Diagnostics;

namespace PureGame.Render.Renderable
{
    public class RenderEntity
    {
        private EntityObject base_entity;
        private Texture2D entity_texture;
        private RenderWorld parent;
        private bool standing_timer = false;
        private Vector2 previous_position;

        public RenderEntity(EntityObject base_entity, RenderWorld parent, ContentManager content)
        {
            this.parent = parent;
            this.base_entity = base_entity;
            entity_texture = Loader.LoadTexture(content, base_entity.FileName);
            
            previous_position = base_entity.Position;
            GetAnimation();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Point position_on_screen = parent.GetEntityScreenPosition(base_entity);
            Rectangle rect = new Rectangle(position_on_screen, parent.TileSize.ToPoint());
            sprite_batch.Draw(entity_texture, rect, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void Update(GameTime time)
        {
            GetAnimation();
        }

        public void GetAnimation()
        {
            Point screen_position = parent.GetScreenPosition(previous_position);
            Point entity_position = parent.GetEntityScreenPosition(base_entity);
            if (screen_position == entity_position)
            {
                //standing
                if(standing_timer)
                {
                    Debug.WriteLine("Standing: " + base_entity.Facing.ToString());
                }
                else
                {
                    standing_timer = true;
                }
            }
            else
            {
                //moving
                if (standing_timer)
                {
                    standing_timer = false;
                }
                Debug.WriteLine("Walking: " + base_entity.Facing.ToString());
                Debug.WriteLine(screen_position + "!=" + entity_position);
            }
            previous_position = base_entity.Position;
        }
    }
}
