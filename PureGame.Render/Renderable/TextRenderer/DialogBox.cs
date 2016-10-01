using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Client.Renderable.WorldRenderer;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class DialogBox
    {
        public readonly EntityRender RenderEntity;
        public readonly Point Size;
        public readonly Point Offset;
        public Point Position
        {
            get
            {
                var position = RenderEntity.ScreenPosition - Offset;
                return position;
            }
        }

        public Rectangle TextBoxSpace => new Rectangle(Position, Size);

        public bool Contains(Point point)
        {
            return TextBoxSpace.Contains(point);
        }

        public DialogBox(EntityRender renderEntity, Point size)
        {
            Size = size;
            RenderEntity = renderEntity;
            var offset = renderEntity.PositionFinder.Offset;
            var xPos = Size.X/2 - (int)offset.X;
            Offset = new Point(xPos, Size.Y);
        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            spriteBatch.Draw(textBoxTexture, TextBoxSpace, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
