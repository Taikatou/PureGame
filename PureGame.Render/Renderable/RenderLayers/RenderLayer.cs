using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client.FocusLayers;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable.RenderLayers
{
    public class RenderLayer : IRenderable
    {
        private readonly IRenderable _render;
        private readonly ContentManager _content;
        public RenderLayer(ILayer layer, ViewportAdapter viewPort, EntityObject focus)
        {
            _content = ContentManagerManager.RequestContentManager();
            var pg = (PureGameLayer)layer;
            var worldArea = pg.PureGame.WorldManager.GetEntitysWorld(focus);
            _render = new RenderWorldLayer(worldArea, viewPort, focus, _content);
        }

        public void Update(GameTime time)
        {
            _render.Update(time);
        }

        public void UnLoad()
        {
            _content.Unload();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _render.Draw(spriteBatch);
        }
    }
}
