using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Render.Controllers;
using PureGame.Render.ControlLayers;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        private readonly PureGameClient _gameClient;
        public RenderWorldLayer Render;
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
            Controllers = new List<IController>
            {
                new WorldClickController(),
                new TouchScreenController(),
                new WorldKeyBoardController()
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var render in ControlLayers.Controllers)
            {
                render.Draw(spriteBatch);
            }
        }

        public void Update(GameTime time)
        {
            foreach (var controlLayer in ControlLayers.Controllers)
            {
                foreach (var controller in Controllers)
                {
                    controller.Update(time, controlLayer);
                }
            }
            foreach (var controlLayer in ControlLayers.Controllers)
            {
                controlLayer.Update(time);
            }
        }

        public void LoadWorld()
        {
            if (Render != null)
            {
                var zoom = Render.Camera.Zoom;
                Render = new RenderWorldLayer(CurrentWorld, ViewPort, _player, zoom);
            }
            else
            {
                Render = new RenderWorldLayer(CurrentWorld, ViewPort, _player, _baseZoom);
            }
            var worldControl = new WorldControlLayer(Render, _gameClient);
            ControlLayers.AddController(worldControl, 0);
        }
    }
}
