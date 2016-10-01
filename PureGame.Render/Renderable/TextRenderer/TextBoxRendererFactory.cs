using MonoGame.Extended.BitmapFonts;
using PureGame.Client.Renderable.WorldRenderer;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class TextBoxRendererFactory
    {
        public static ITextBoxRenderer MakeTextBoxRenderer(BitmapFont font, EntityRender renderEntity)
        {
            ITextBoxRenderer toReturn;
            var interaction = renderEntity.BaseEntity.Interaction;
            switch (interaction.Type)
            {
                case "OptionsInteraction":
                        toReturn = new OptionsTextBoxRenderer(font, renderEntity);
                    break;
                case "PlainTextInteraction":
                        toReturn = new PlainTextBoxRenderer(font, renderEntity);
                    break;
                default:
                        toReturn = new PlainTextBoxRenderer(font, renderEntity);
                    break;
            }
            return toReturn;
        }
    }
}
