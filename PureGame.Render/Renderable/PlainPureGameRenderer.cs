﻿using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Render.Controlables;
using PureGame.Render.Controllers;
using PureGame.Render.Controllers.GamePadController;
using PureGame.Render.Controllers.KeyBoard;
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
        public ControllerManager ControllerManager;
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
            var hudController = new HudControlableLayer(new HudRenderLayer());
            ControlLayers.AddController(hudController, 2);
            ControllerManager = new ControllerManager();
            ControllerManager.Add(new WorldClickController());
            ControllerManager.Add(new TouchScreenController());
            ControllerManager.Add(new WorldKeyBoardController());
            ControllerManager.Add(new GamePadController());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var render in ControlLayers.ControlLayers)
            {
                render.Draw(spriteBatch);
            }
        }

        public void Update(GameTime time)
        {
            ControllerManager.Update(time);
            ControlLayers.Update(time);
            foreach (var layer in ControlLayers.ControlLayers)
            {
                foreach (var controller in ControllerManager.Controllers)
                {
                    controller.UpdateLayer(time, layer);
                }
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
            var worldControl = new WorldControlableLayer(Render, _gameClient);
            ControlLayers.AddController(worldControl, 0);
            var textController = new TextControlableLayer(new TextRenderLayer(Render));
            ControlLayers.AddController(textController, 1);
        }
    }
}
