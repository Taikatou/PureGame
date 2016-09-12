using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Render
{
    public class PureGameClient
    {
        public int Timer;
        public int TimerResetValue = 50;
        public readonly Entity Player;
        public PureGame PureGame;
        public WorldArea CurrentWorld => PureGame.WorldManager.GetEntitysWorld(Player);

        public PureGameClient(Entity entity, PureGame pureGame)
        {
            PureGame = pureGame;
            Player = entity;
        }

        public void ControllerA()
        {
            if (!CurrentlyInteracting)
            {
                CurrentWorld.ProccessInteraction(Player);
            }
            else
            {
                CurrentWorld.ProgressInteraction(Player);
            }
        }

        public void ControllerDPad(Direction cachedMoveDiection, GameTime time)
        {
            if (!CurrentlyInteracting && !CurrentWorld.CurrentlyMoving(Player))
            {
                var entityMoving = CurrentWorld.EntityManager.EntityCurrentlyMoving(Player);
                if (!entityMoving && Player.FacingDirection != cachedMoveDiection)
                {
                    Player.FacingDirection = cachedMoveDiection;
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    Player.MovementDirection = cachedMoveDiection;
                    CurrentWorld.ProccessMovement(Player);
                }
                else
                {
                    Timer -= time.ElapsedGameTime.Milliseconds;
                }
            }
        }
        public void ControllerB(bool active)
        {
            if (!CurrentlyInteracting)
            {
                Player.Running = active;
            }
        }
        public bool CurrentlyInteracting => CurrentWorld.CurrentlyInteracting(Player);
        public void Click(Vector2 position)
        {
            var spatialHash = CurrentWorld.EntityManager.SpatialHash;
            var spatialTrigger = CurrentWorld.TriggerManager.SpatialTriggers;
            if (spatialHash.ContainsKey(position))
            {
                var entity = spatialHash[position];
                Debug.WriteLine(entity.ToString());
            }
            else if (spatialTrigger.ContainsKey(position))
            {
                var trigger = spatialTrigger[position];
                Debug.WriteLine(trigger.ToString());
            }
            else
            {
                Debug.WriteLine("Position clicked: " + position);
            }
        }
    }
}
