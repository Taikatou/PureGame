using System.Diagnostics;
using Microsoft.Xna.Framework;
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
            var screenPositionOffset = r.ScreenPosition - new Point(0, textSpace.Height);
            DialogBox = new DialogBox(screenPositionOffset, textSpace, Text);
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
    }
}
