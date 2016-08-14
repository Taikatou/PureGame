using System;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace PureGame.Client
{
    public class NetworkManager
    {
        public string Username { get; set; }

        public string GroupId { get; private set; }

        public bool Active { get; set; }

        private NetClient _client;
        public NetworkManager()
        {
            
        }

        public bool Start()
        {
            var random = new Random();
            _client = new NetClient(new NetPeerConfiguration("networkGame"));
            _client.FlushSendQueue();
            _client.Start();
            Username = "name_" + random.Next(0, 100);
            GroupId = "test";
            var outmsg = _client.CreateMessage();
            outmsg.Write(GroupId);
            outmsg.Write((byte)PacketType.Login);
            outmsg.Write(Username);
            _client.Connect("localhost", 14241, outmsg);
            return EsablishInfo();
        }

        public void Update(GameTime time)
        {
            throw new NotImplementedException();
        }
    }
}
