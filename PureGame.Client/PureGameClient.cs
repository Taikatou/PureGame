using SmallGame;
using Microsoft.Xna.Framework;
using ExitGames.Client.Photon.LoadBalancing;
using System;
using PureGame.Engine.EntityData;
using PureGame.Engine.Controllers;
using PureGame.Engine;

namespace PureGame.Client
{
    enum EventCodes { Move };
    public class PureGameClient : IPureGame
    {
        private LoadBalancingClient client;
        private string WorldName = "";
        private IController controller;
        private PlayerEntityObject player_entity;
        private IPureGame parent;
        private Vector2 previous_message;
        private IPureGame game;

        public WorldArea Current
        {
            get
            {
                return game.Current;
            }

            set
            {
                game.Current = value;
            }
        }

        public DataLoader DataLoader
        {
            get
            {
                return game.DataLoader;
            }

            set
            {
                game.DataLoader = value;
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

        void MoveRoom(string roomName, byte maxPlayers)
        {
            client.OpCreateRoom(roomName, new RoomOptions() { MaxPlayers = maxPlayers }, TypedLobby.Default);
        }

        public void Update(GameTime time)
        {
            controller?.Update(player_entity, time);
            if (WorldName != game.Current.Name)
            {
                WorldName = game.Current.Name;
                MoveRoom(WorldName, 8);
            }
            client.Service();
            game.Update(time);

            //Send movement
            bool player_moving = Current.EntityManager.Data.EntityCurrentlyMoving(player_entity);
            bool message_sent = previous_message == player_entity.Position;
            if (player_moving && !message_sent)
            {
                previous_message = player_entity.Position;
            }
        }

        public PureGameClient(IPureGame game)
        {
            this.game = game;
            game.Parent = this;
            client = new LoadBalancingClient();
            client.AppId = "4d493591-adb3-4b41-b40f-0e0a4c4955ce";  // edit this!
            // "eu" is the European region's token
            bool connectInProcess = client.ConnectToRegionMaster("eu");  // can return false for errors
            if(!connectInProcess)
            {
                throw new Exception("Could not connect to game");
            }
        }

        public void SetPlayer(PlayerEntityObject p, IController c)
        {
            player_entity = p;
            controller = c;
            Current.Entities.Add(player_entity);
        }

        void OnApplicationQuit()
        {
            client.Disconnect();
        }

        public void LoadWorld(string world_name, IFileReader reader)
        {
            game.LoadWorld(world_name, reader);
        }

        public void OnWorldChange()
        {
            parent.OnWorldChange();
        }
    }
}
