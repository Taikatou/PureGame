using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PureGame.Client.Renderable.WorldRenderer;
using PureGame.Engine.Communication;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class OptionsTextBoxRenderer : ITextBoxRenderer
    {
        public string Text => "Hi";
        private readonly OptionsInteraction _interaction;
        private readonly BitmapFont _font;
        public DialogBox DialogBox;
        public List<PlainTextBoxRenderer> OptionRenderer;
        public Dictionary<PlainTextBoxRenderer, InteractionOption> Option;

        public OptionsTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            _font = font;
            _interaction = r.BaseEntity.Interaction as OptionsInteraction;
            var textSpace = GetGreatestTextSize();
            OptionRenderer = new List<PlainTextBoxRenderer>();
            Option = new Dictionary<PlainTextBoxRenderer, InteractionOption>();
            if (_interaction != null)
            {
                foreach (var option in _interaction.Options)
                {
                    var plainBox = new PlainTextBoxRenderer(font, r.ScreenPosition, textSpace, _interaction);
                    OptionRenderer.Add(plainBox);
                    Option[plainBox] = option;
                }
                var rows = textSpace.Y * (_interaction.Options.Count + 1);
                var optionsTextBox = new Point(textSpace.X, textSpace.Y + rows);
                DialogBox = new DialogBox(r.ScreenPosition, optionsTextBox);
            }
            else
            {
                throw new Exception("Interaction is not of type OptionsInteraction");
            }
        }

        public Point GetGreatestTextSize()
        {
            var greatestSize = _font.GetSize(Text);
            foreach (var option in _interaction.Options)
            {
                var size = _font.GetSize(option.Text);
                if (size.Width > greatestSize.Width)
                {
                    greatestSize = size;
                }
            }
            return greatestSize;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D textBoxTexture)
        {
            DialogBox.Draw(spriteBatch, textBoxTexture);
            spriteBatch.DrawString(_font, Text, DialogBox.Position.ToVector2(), Color.Black);
            foreach (var option in OptionRenderer)
            {
                option.Draw(spriteBatch, textBoxTexture);
            }
        }

        public bool Tap(Vector2 position)
        {
            var found = DialogBox.Contains(position);
            if (found)
            {
                foreach (var option in OptionRenderer)
                {
                    if (option.Tap(position))
                    {
                        Option[option].Click();
                        break;
                    }
                }
            }
            return found;
        }
    }
}
