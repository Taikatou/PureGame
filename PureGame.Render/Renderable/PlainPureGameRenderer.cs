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
        public Stack<IControllable> Controllables;
        public RenderLayer Render { get; set; }
        public EntityObject FocusEntity;
        private readonly List<IController> _controllers;
        public PlainPureGameRenderer(IControllable gameClient, ViewportAdapter viewPort, EntityObject focusEntity)
        {
            Controllables = new Stack<IControllable>();
            Controllables.Push(gameClient);
            ViewPort = viewPort;
            FocusEntity = focusEntity;
            _controllers = new List<IController> {new KeyBoardController(), new ClickController(this)};
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Render.Draw(spriteBatch);
        }

        public void Update(GameTime time)
        {
            var focus = Controllables.Peek();
            foreach (var controller in _controllers)
            {
                focus.Update(time, controller);
            }
            Render.Update(time);
        }

        public void LoadWorld()
        {
            Render = new RenderLayer(Controllables.Peek().Layer, ViewPort, FocusEntity);
        }

        public void ChangeFocus(EntityObject e)
        {
            FocusEntity = e;
        }
    }
}
