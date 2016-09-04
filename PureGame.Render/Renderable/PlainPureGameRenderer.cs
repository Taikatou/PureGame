using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Client.Controllers;
using PureGame.Engine.Controls;
using PureGame.Engine.EntityData;
using PureGame.Render.Common;
using PureGame.Render.Renderable.RenderLayers;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : IPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        public PureGameClient GameClient;
        public RenderLayer Render { get; set; }
        public EntityObject FocusEntity;
        private string _layerName;
        private readonly List<IController> _controllers;
        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, EntityObject focusEntity)
        {
            GameClient = gameClient;
            ViewPort = viewPort;
            FocusEntity = focusEntity;
            _layerName = "";
            _controllers = new List<IController> {new KeyBoardController(), new ClickController(this)};
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Render.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            foreach (var controller in _controllers)
            {
                GameClient.Update(time, controller);
            }
            var layer = GameClient.PureGameLayer;
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
