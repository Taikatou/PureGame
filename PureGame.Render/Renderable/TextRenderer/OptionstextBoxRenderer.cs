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
        public List<BaseTextBoxRenderer> OptionRenderer;
        public Dictionary<BaseTextBoxRenderer, InteractionOption> Option;

        public OptionsTextBoxRenderer(BitmapFont font, EntityRender r)
        {
            _font = font;
            _interaction = r.BaseEntity.Interaction as OptionsInteraction;
            var textSpace = GetGreatestTextSize();
            var extendBy = 1.2f;
            OptionRenderer = new List<BaseTextBoxRenderer>();
            Option = new Dictionary<BaseTextBoxRenderer, InteractionOption>();
            if (_interaction != null)
            {
                var count = 0;
                var optionsTextSpace = new Point((int)(textSpace.X * extendBy), textSpace.Y);
                foreach (var option in _interaction.Options)
                {
                    var offset = textSpace.Y*count;
                    var optionScreenPosition = r.ScreenPosition + new Point(0, offset);
                    var plainBox = new BaseTextBoxRenderer(font, optionScreenPosition, textSpace, option.Text);
                    OptionRenderer.Add(plainBox);
                    Option[plainBox] = option;
                    count++;
                }
                var rows = textSpace.Y * (_interaction.Options.Count + 1);
                var optionsTextBox = new Point((int)(textSpace.X * extendBy), textSpace.Y + rows);
                var screenPosition = r.ScreenPosition + new Point(0, rows);
                DialogBox = new DialogBox(screenPosition, optionsTextBox);
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
                        _interaction.Interact();
                        break;
                    }
                }
            }
            return found;
        }
    }
}
