using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class EntityTextRenderer
    {
        protected BitmapFont Font { get; private set; }
        public EntityTextRenderer(ContentManager content)
        {
            Font = content.Load<BitmapFont>("Fonts/montserrat-32");
        }

        public void Draw(SpriteBatch spriteBatch, RenderEntity renderEntity)
        {
            if (renderEntity.BaseEntity.Talking)
            {
                var position = renderEntity.ScreenPosition;
                spriteBatch.DrawString(Font, "Interacting", position.ToVector2(), Color.Black);
            }
        }
    }
}
