using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.Engine;
using ExitGames.Client.Photon;
using System;
using ExitGames.Client.Photon.LoadBalancing;

namespace PureGame.Client
{
    public class PureGameClient : IPhotonPeerListener, IPureGame
    {
        private IController controller;
        private PlayerEntity player_entity;

        private string AppId = "4d493591-adb3-4b41-b40f-0e0a4c4955ce";
        public PlayerEntity Player
        {
            get
            {
                return player_entity;
            }
        }
        private IPureGame parent;
        private IPureGame game;

        public WorldArea World
        {
            get
            {
                return game.World;
            }

            set
            {
                game.World = value;
            }
        }

        public IPureGame Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public void Update(GameTime time)
        {
            controller?.Update(player_entity, time);
            game.Update(time);
        }

        public PureGameClient(string AppId, IPureGame game)
        {
            this.game = game;
            game.Parent = this;
            Connect();
        }

        public void SetPlayer(PlayerEntity p, IController c)
        {
            player_entity = p;
            controller = c;
        }

        public void LoadWorld(string world_name, IFileReader reader)
        {
            game.LoadWorld(world_name, reader);
        }

        public void OnWorldChange()
        {
            parent.OnWorldChange();
        }

        public void DebugReturn(DebugLevel level, string message)
        {
            throw new NotImplementedException();
        }

        public void OnOperationResponse(OperationResponse operationResponse)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            throw new NotImplementedException();
        }

        public void OnEvent(EventData eventData)
        {
            throw new NotImplementedException();
        }

        LoadBalancingPeer peer;

        public bool Connect()
        {
            // A LoadBalancingPeer lets you connect and call operations on the server. Callbacks go to "this" listener instance and use UDP
            peer = new LoadBalancingPeer(this, ConnectionProtocol.Udp);
            if (peer.Connect("app.exitgamescloud.com:port", AppId))
            {
                return true;
            }

            // connect might fail right away if the address format is bad, e.g.
            return false;
        }
    }
}
