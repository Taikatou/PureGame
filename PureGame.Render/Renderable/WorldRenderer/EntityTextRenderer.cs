using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace PureGame.Render.Renderable.WorldRenderer
{
    public class EntityTextRenderer
    {
        protected BitmapFont Font { get; }
        public EntityTextRenderer(ContentManager content)
        {
            Font = content.Load<BitmapFont>("Fonts/montserrat-84");
        }

        public void Draw(SpriteBatch spriteBatch, EntityRender renderEntity)
        {
            if (renderEntity.BaseEntity.Talking)
            {
                var position = renderEntity.ScreenPosition.ToVector2() - renderEntity.PositionFinder.Offset;
                spriteBatch.DrawString(Font, "Interacting", position, Color.Black);
            }
        }
    }
}
