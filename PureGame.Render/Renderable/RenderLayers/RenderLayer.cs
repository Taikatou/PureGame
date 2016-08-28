using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client.FocusLayers;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable.RenderLayers
{
    public class RenderLayer : IRenderable
    {
        private readonly IRenderable _render;
        public RenderLayer(ILayer layer, ViewportAdapter viewPort, EntityObject focus)
        {
            PureGameLayer pg = (PureGameLayer)layer;
            _render = new RenderWorld(pg.PureGame.WorldManager.CurrentWorld, viewPort, focus);
        }

        public void Update(GameTime time)
        {
            _render.Update(time);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _render.Draw(spriteBatch);
        }
    }
}
