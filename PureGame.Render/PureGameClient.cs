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

        public void Update(GameTime time)
        {
            if (Timer >= 0)
            {
                Timer -= time.ElapsedGameTime.Milliseconds;
            }
        }

        public PureGameClient(Entity entity, PureGame pureGame)
        {
            PureGame = pureGame;
            Player = entity;
        }

        public void ControllerA()
        {
            CurrentWorld.ProccessInteraction(Player);
        }

        public void FaceDirection(Direction direction)
        {
            Player.FacingDirection = direction;
        }

        public void MoveDirection(Direction direction)
        {
            Player.MovementDirection = direction;
            CurrentWorld.ProccessMovement(Player);
        }

        public void ControllerDPad(Direction direction)
        {
            if (direction != Direction.None && !CurrentWorld.CurrentlyMoving(Player))
            {
                var entityMoving = CurrentWorld.EntityManager.EntityCurrentlyMoving(Player);
                if (!entityMoving && Player.FacingDirection != direction)
                {
                    FaceDirection(direction);
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    MoveDirection(direction);
                }
            }
        }
        public void ControllerB(bool active)
        {
            Player.Running = active;
        }
        public void Click(Point position)
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
