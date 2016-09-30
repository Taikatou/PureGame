﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using PureGame.Engine;
using PureGame.Client.Renderable.WorldRenderer;
using Microsoft.Xna.Framework.Content;

namespace PureGame.Client.Renderable.TextRenderer
{
    public class TextRenderLayer
    {
        public WorldRenderLayer WorldRender;
        protected BitmapFont Font { get; }
        protected Texture2D TextBoxTexture;
        public Dictionary<EntityRender, TextBoxRenderer> TextBoxDict;
        private ContentManager _content;

        public TextRenderLayer(WorldRenderLayer worldRender)
        {
            WorldRender = worldRender;
            _content = ContentManagerManager.RequestContentManager();
            Font = _content.Load<BitmapFont>("Fonts/montserrat-84");
            TextBoxTexture = _content.Load<Texture2D>($"Images/{"outline"}");
            TextBoxDict = new Dictionary<EntityRender, TextBoxRenderer>();
        }

        public void Update(GameTime time)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = WorldRender.Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            foreach (var r in WorldRender.ToDraw.Elements)
            {
                if (r.BaseEntity.Talking)
                {
                    var textBox = GetTextBox(r);
                    textBox.Draw(spriteBatch, TextBoxTexture, Font);
                }
            }
            spriteBatch.End();
        }

        public TextBoxRenderer GetTextBox(EntityRender r)
        {
            if (!TextBoxDict.ContainsKey(r))
            {
                TextBoxDict[r] = new TextBoxRenderer(Font, r);
            }
            return TextBoxDict[r];
        }

        public void UnLoad()
        {
            _content.Unload();
        }

        public void Dispose()
        {
            _content.Dispose();
        }

        public bool Tap(Vector2 position)
        {
            var found = false;
            position = WorldRender.ScreenToWorld(position);
            foreach (var r in WorldRender.ToDraw.Elements)
            {
                if (r.BaseEntity.Talking)
                {
                    var textBox = GetTextBox(r);
                    found = found || textBox.Tap(position);
                }
            }
            return found;
        }
    }
}
