using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Render.Controllers;
using PureGame.Render.ControlLayers;
using PureGame.Render.Renderable.HudRenderer;
using PureGame.Render.Renderable.TextRenderer;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        private readonly PureGameClient _gameClient;
        public WorldRenderLayer Render;
        public ControlLayerManager ControlLayers;
        public List<IController> Controllers;
        private readonly IEntity _player;
        private readonly float _baseZoom;

        public WorldArea CurrentWorld => _gameClient.CurrentWorld;

        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, IEntity player, float zoom)
        {
            _baseZoom = zoom;
            _player = player;
            _gameClient = gameClient;
            ViewPort = viewPort;
            ControlLayers = new ControlLayerManager();
            var hudController = new HudControlLayer(new HudRenderLayer());
            ControlLayers.AddController(hudController, 2);
            Controllers = new List<IController>
            {
                new WorldClickController(),
                new TouchScreenController(),
                new WorldKeyBoardController()
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var render in ControlLayers.ControlableDict.Values)
            {
                render.Draw(spriteBatch);
            }
        }

        public void Update(GameTime time)
        {
            foreach (var controller in Controllers)
            {
                controller.Update(time, ControlLayers.ControlLayers);
            }
            foreach (var controlLayer in ControlLayers.ControlableDict.Values)
            {
                controlLayer.Update(time);
            }
        }

        public void LoadWorld()
        {
            if (Render != null)
            {
                var zoom = Render.Camera.Zoom;
                Render = new WorldRenderLayer(CurrentWorld, ViewPort, _player, zoom);
            }
            else
            {
                Render = new WorldRenderLayer(CurrentWorld, ViewPort, _player, _baseZoom);
            }
            var worldControl = new WorldControlAbleLayer(Render, _gameClient);
            ControlLayers.AddController(worldControl, 0);
            var textController = new TextControlAbleLayer(new TextRenderLayer(Render));
            ControlLayers.AddController(textController, 1);
        }
    }
}
