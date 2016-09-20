using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PureGame.Engine;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable.TextRenderer
{
    public class TextRenderLayer
    {
        public WorldRenderLayer WorldRender;
        private readonly EntityTextRenderer _textRenderer;

        public TextRenderLayer(WorldRenderLayer worldRender)
        {
            WorldRender = worldRender;
            var content = ContentManagerManager.RequestContentManager();
            _textRenderer = new EntityTextRenderer(content);
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
                _textRenderer.Draw(spriteBatch, r);
            }
            spriteBatch.End();
        }

        public void UnLoad()
        {
            
        }
    }
}
