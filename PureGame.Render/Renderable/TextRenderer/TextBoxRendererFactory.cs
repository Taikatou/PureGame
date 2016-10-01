using MonoGame.Extended.BitmapFonts;
using PureGame.Client.Renderable.WorldRenderer;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class TextBoxRendererFactory
    {
        public static ITextBoxRenderer MakeTextBoxRenderer(BitmapFont font, EntityRender renderEntity)
        {
            PlainTextBoxRenderer toReturn;
            var interaction = renderEntity.BaseEntity.Interaction;
            switch (interaction.Type)
            {
                default: toReturn = new PlainTextBoxRenderer(font, renderEntity);
                    break;
            }
            return toReturn;
        }
    }
}
