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
        private readonly List<IControllable> _controllables;
        private readonly PureGameClient _gameLayer;
        public RenderWorldLayer Render;
        public List<IRenderable> ToRender;
        public RenderWorldLayer RenderWorld => ToRender[0] as RenderWorldLayer;
        private readonly EntityObject _player;
        private readonly List<IController> _controllers;
        public PlainPureGameRenderer(PureGameClient gameClient, ViewportAdapter viewPort, EntityObject player)
        {
            _player = player;
            _gameLayer = gameClient;
            _controllables = new List<IControllable> {gameClient};
            ViewPort = viewPort;
            ToRender = new List<IRenderable> {null};
            _controllers = new List<IController> {new KeyBoardController(), new ClickController(this)};
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var render in ToRender)
            {
                render.Draw(spriteBatch);
            }
        }

        public void AddControlable(IControllable controller)
        {
            _controllables.Add(controller);
        }

        public void Update(GameTime time)
        {
            for(var i = _controllables.Count - 1; i >= 0; i--)
            {
                foreach (var controller in _controllers)
                {
                    var toBreak = _controllables[i].Update(time, controller);
                    if (toBreak) break;
                }
            }
            foreach (var renderable in ToRender)
            {
                renderable.Update(time);
            }
        }

        public WorldArea CurrentWorld => _gameLayer.PureGameLayer.CurrentWorld;

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
            ToRender[0] = Render;
        }

        public void ChangeFocus(IFocusable focus) => Render.FocusStack.Push(focus);

        private bool _moving;
        public void BeginMove()
        {
            if (!_moving)
            {
                _moving = true;
                var focusStack = Render.FocusStack;
                var position = Render.Focus.Position;
                focusStack.Push(new FocusVector(position));
            }
        }

        public void MoveFocusBy(Vector2 moveBy)
        {
            if (_moving)
            {
                var focus = Render.Focus;
                var focusVector = focus as FocusVector;
                if (focusVector != null)
                {
                    moveBy /= Render.Camera.Zoom;
                    focusVector.Position -= moveBy;
                }
            }
        }

        public void EndMove()
        {
            if (_moving)
            {
                Render.FocusStack.Pop();
                _moving = false;
            }   
        }
    }
}
