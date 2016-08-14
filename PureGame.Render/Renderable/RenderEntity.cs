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
    public class RenderEntity : IRenderable
    {
        private EntityObject base_entity;
        private Texture2D entity_texture;
        private WorldArea world;
        private RenderWorld parent;
        //Right Left Up Down
        private Animation [] walking;
        private Animation [] standing;

        private bool standing_timer = false;

        private Animation currentAnimation;
        private Vector2 previous_position;
        public RenderEntity(WorldArea world, EntityObject base_entity, RenderWorld parent, ContentManager content)
        {
            this.parent = parent;
            this.world = world;
            this.base_entity = base_entity;
            entity_texture = Loader.LoadTexture(content, base_entity.FileName);
            walking = new Animation[4];
            walking[(int)Direction.Down] = new Animation ();
            walking[(int)Direction.Down].AddFrame (new Rectangle (0, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walking[(int)Direction.Down].AddFrame (new Rectangle (16, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walking[(int)Direction.Down].AddFrame (new Rectangle (0, 0, 16, 16), TimeSpan.FromSeconds (.25));
            walking[(int)Direction.Down].AddFrame (new Rectangle (32, 0, 16, 16), TimeSpan.FromSeconds (.25));
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
            if (screen_position == entity_position)
            {
                //standing
                if(standing_timer)
                {
                    Debug.WriteLine("Standing: " + base_entity.Facing.ToString());
                    currentAnimation = standing[(int)base_entity.Facing];
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
                currentAnimation = walking[(int)base_entity.Facing];
            }
            previous_position = base_entity.Position;
        }
    }
}
