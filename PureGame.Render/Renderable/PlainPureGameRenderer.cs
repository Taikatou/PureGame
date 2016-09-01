using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Engine.EntityData;
using PureGame.Render.Renderable.RenderLayers;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        public PureGameClient GameClient;
        public RenderLayer Render;
        public EntityObject FocusEntity;
        private string _layerName;
        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, EntityObject focusEntity)
        {
            GameClient = gameClient;
            ViewPort = viewPort;
            FocusEntity = focusEntity;
            _layerName = "";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Render.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            GameClient.Update(time);
            var layer = GameClient.Layers.Peek();
            if (_layerName != layer.Name)
            {
                Render?.UnLoad();
                Render = new RenderLayer(layer, ViewPort, FocusEntity);
                _layerName = layer.Name;
            }
            Render.Update(time);
        }

        public void ChangeFocus(EntityObject e)
        {
            FocusEntity = e;
        }
    }
}
