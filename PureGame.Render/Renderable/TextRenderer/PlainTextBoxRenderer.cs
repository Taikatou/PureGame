using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PureGame.Client.Renderable.WorldRenderer;
using PureGame.Engine.Communication;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class PlainTextBoxRenderer : BaseTextBoxRenderer
    {
        public override string Text => "Interacting";
        public IInteraction Interaction;

        public PlainTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            Font = font;
            Interaction = r.BaseEntity.Interaction;
            var textSpace = font.GetSize(Text);
            DialogBox = new DialogBox(r.ScreenPosition, textSpace);
        }

        public override bool Tap(Vector2 position)
        {
            var found = base.Tap(position);
            if (found)
            {
                Debug.WriteLine(position);
                Interaction.Interact();
            }
            return found;
        }

        public override void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            DialogBox.Draw(spriteBatch, textBoxTexture);
            spriteBatch.DrawString(Font, Text, DialogBox.Position.ToVector2(), Color.Black);
        }
    }
}
