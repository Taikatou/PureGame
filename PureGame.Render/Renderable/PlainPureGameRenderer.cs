using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Render.Controllers;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        private readonly PureGameClient _gameClient;
        public RenderWorldLayer Render;
        public List<RenderLayer> ToRender;
        private readonly Entity _player;
        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, Entity player)
        {
            _player = player;
            _gameClient = gameClient;
            ViewPort = viewPort;
            ToRender = new List<RenderLayer>{ null };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var render in ToRender)
            {
                render.Draw(spriteBatch);
            }
        }

        public void Update(GameTime time)
        {
            foreach (var renderable in ToRender)
            {
                renderable.Update(time);
            }
        }

        public WorldArea CurrentWorld => _gameClient.CurrentWorld;

        public void LoadWorld()
        {
            if (Render != null)
            {
                var zoom = Render.Camera.Zoom;
                Render = new RenderWorldLayer(CurrentWorld, ViewPort, _player, zoom);
            }
            else
            {
                Render = new RenderWorldLayer(CurrentWorld, ViewPort, _player);
            }
            Render.Controllers.Add(new WorldKeyBoardController(_gameClient));
            Render.Controllers.Add(new WorldClickController(Render, _gameClient));
            Render.Controllers.Add(new TouchScreenController(Render, _gameClient));
            ToRender[0] = Render;
        }
    }
}
