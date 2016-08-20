using Microsoft.Xna.Framework;
using ExitGames.Client.Photon.LoadBalancing;
using PureGame.Engine.EntityData;
using PureGame.Engine.Controllers;
using PureGame.Engine;

namespace PureGame.Client
{
    /// <summary>Delegate to get notified of joining/leaving players (see OnEventJoin and OnEventLeave).</summary>
    public delegate void EventPlayerListChangeDelegate(PlayerEntity particlePlayer);
    public class PureGameClient : LoadBalancingClient, IPureGame
    {
        private IController controller;
        public EventPlayerListChangeDelegate OnEventJoin;

        /// <summary>Can be used to be notified when a player leaves the room (Photon: EvLeave).</summary>
        public EventPlayerListChangeDelegate OnEventLeave;
        private PlayerEntity player_entity;
        //public new PlayerEntity LocalPlayer { get { return (PlayerEntity)base.LocalPlayer; } }
        //AppId = "4d493591-adb3-4b41-b40f-0e0a4c4955ce";
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

        public PureGameClient(IPureGame game)
        {
            this.game = game;
            game.Parent = this;
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
    }
}
