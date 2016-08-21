using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using PureGame.Render.Animate;
using PureGame.Render.Common;
using System;
using System.Diagnostics;

namespace PureGame.Render.Renderable
{
    public class RenderEntity
    {
        private IEntity base_entity;
        private Texture2D entity_texture;
        private RenderWorld parent;
        //Right Left Up Down
        private Animation[] walking;
        private Animation[] standing;

        private bool standing_timer = false;

        private Animation currentAnimation;
        private Vector2 previous_position;
        public RenderEntity(IEntity base_entity, RenderWorld parent, ContentManager content)
        {
            this.parent = parent;
            this.base_entity = base_entity;
            entity_texture = AssetLoader.LoadTexture(content, base_entity.FileName);
            walking = new Animation[4];
            walking[(int)Direction.Down] = new Animation();
            walking[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Down].AddFrame(new Rectangle(16, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Down].AddFrame(new Rectangle(32, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Up] = new Animation();
            walking[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Up].AddFrame(new Rectangle(160, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Up].AddFrame(new Rectangle(176, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walking[(int)Direction.Left] = new Animation();
            walking[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Left].AddFrame(new Rectangle(64, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Left].AddFrame(new Rectangle(80, 0, 16, 16), TimeSpan.FromSeconds(.25));

            walking[(int)Direction.Right] = new Animation();
            walking[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Right].AddFrame(new Rectangle(112, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            walking[(int)Direction.Right].AddFrame(new Rectangle(128, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standing = new Animation[4];
            standing[(int)Direction.Down] = new Animation();
            standing[(int)Direction.Down].AddFrame(new Rectangle(0, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standing[(int)Direction.Up] = new Animation();
            standing[(int)Direction.Up].AddFrame(new Rectangle(144, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standing[(int)Direction.Left] = new Animation();
            standing[(int)Direction.Left].AddFrame(new Rectangle(48, 0, 16, 16), TimeSpan.FromSeconds(.25));

            standing[(int)Direction.Right] = new Animation();
            standing[(int)Direction.Right].AddFrame(new Rectangle(96, 0, 16, 16), TimeSpan.FromSeconds(.25));
            previous_position = base_entity.Position;
            GetAnimation();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Point position_on_screen = parent.GetEntityScreenPosition(base_entity);
            Rectangle rect = new Rectangle(position_on_screen, parent.TileSize.ToPoint());
            var sourceRectangle = currentAnimation.CurrentRectangle;
            sprite_batch.Draw(entity_texture, rect, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void Update(GameTime time)
        {
            GetAnimation();
            currentAnimation.Update(time);
        }

        public void GetAnimation()
        {
            Point screen_position = parent.GetScreenPosition(previous_position);
            Point entity_position = parent.GetEntityScreenPosition(base_entity);
            int direction = (int)base_entity.FacingDirection;
            if (screen_position == entity_position)
            {
                //standing
                if (standing_timer)
                {
                    currentAnimation = standing[direction];
                }
                else
                {
                    standing_timer = true;
                    Debug.WriteLine("Start standing");
                }
            }
            else
            {
                //moving
                if (standing_timer)
                {
                    standing_timer = false;
                    Debug.WriteLine("Start walking");
                }
                else
                {
                    currentAnimation = walking[direction];
                }
                //Debug.WriteLine("Walking: " + base_entity.Facing.ToString());
            }
            previous_position = base_entity.Position;
        }
    }
}