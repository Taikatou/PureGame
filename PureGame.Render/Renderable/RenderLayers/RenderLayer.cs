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
        IRenderable Render;
        public RenderLayer(ILayer layer, ViewportAdapter ViewPort, EntityObject Focus)
        {
            PureGameLayer pg = (PureGameLayer)layer;
            Render = new RenderWorld(pg.PureGame.World, ViewPort, Focus);
        }

        public void Update(GameTime time)
        {
            Render.Update(time);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Render.Draw(sprite_batch);
        }
    }
}
