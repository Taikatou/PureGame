using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using PureGame.Client;
using PureGame.Engine.EntityData;
using PureGame.MessageBus;
using PureGame.Render.Renderable.WorldRenderer;
using PureGame.Render.Renderable.RenderLayers;
using PureGame.Client.FocusLayers;

namespace PureGame.Render.Renderable
{
    public class PlainPureGameRenderer : ISubscriber
    {
        public ViewportAdapter ViewPort;
        public PureGameClient GameClient;
        public RenderLayer Render;
        public EntityObject FocusEntity;
        public PlainPureGameRenderer(PureGameClient GameClient, ViewportAdapter ViewPort, EntityObject FocusEntity)
        {
            this.GameClient = GameClient;
            this.ViewPort = ViewPort;
            this.FocusEntity = FocusEntity;
            MessageManager.Instance.Subscribe(GameClient.PureGame.Subscription, this);
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Render.Draw(sprite_batch);
        }

        public void Update(GameTime time)
        {
            GameClient.Update(time);
            Render.Update(time);
        }

        public void RecieveMessage(Message m)
        {
            PureGame.MessageCode code = (PureGame.MessageCode)m.Code;
            switch (code)
            {
                case PureGame.MessageCode.LayerChanged:
                    break;
                case PureGame.MessageCode.WorldChanged:
                    ILayer layer = GameClient.Layers.Peek();
                    Render = new RenderLayer(layer, ViewPort, FocusEntity);
                    break;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            MessageManager.Instance.UnSubscribe(GameClient.PureGame.Subscription, this);
        }
    }
}
