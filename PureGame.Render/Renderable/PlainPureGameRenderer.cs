using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Client.Controllers;
using PureGame.Common;
using PureGame.Engine.Controls;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Render.Common;
using PureGame.Render.Renderable.WorldRenderer;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer
    {
        public ViewportAdapter ViewPort;
        public Stack<IControllable> Controllables;
        private readonly PureGameClient _gameLayer;
        public RenderWorldLayer Render { get; set; }
        private readonly EntityObject _player;
        private readonly List<IController> _controllers;
        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, EntityObject player)
        {
            _player = player;
            _gameLayer = gameClient;
            Controllables = new Stack<IControllable>();
            Controllables.Push(gameClient);
            ViewPort = viewPort;
            _controllers = new List<IController> {new KeyBoardController(), new ClickController(this)};
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Render.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            var focus = Controllables.Peek();
            focus.Update(time);
            foreach (var controller in _controllers)
            {
                focus.UpdateController(time, controller);
            }
            Render.Update(time);
        }

        public WorldArea CurrentWorld => _gameLayer.PureGameLayer.CurrentWorld;

        public void LoadWorld()
        {
            Render = new RenderWorldLayer(CurrentWorld, ViewPort, _player);
        }

        public void ChangeFocus(IFocusable focus) => Render.ChangeFocus(focus);

        public void MoveFocus(Vector2 focusVector) => Render.MoveFocus(focusVector);
    }
}
