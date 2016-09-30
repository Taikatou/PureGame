using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;
using PureGame.Engine.World.Controllers;

namespace PureGame.Client
{
    public class PureGameClient : AbstractController
    {
        public int Timer;
        public int TimerResetValue = 50;
        public PureGame PureGame;
        public override WorldArea CurrentWorld => PureGame.WorldManager.GetEntitysWorld(Entity);

        public void Update(GameTime time)
        {
            if (Timer >= 0)
            {
                Timer -= time.ElapsedGameTime.Milliseconds;
            }
        }

        public PureGameClient(IEntity entity, PureGame pureGame) : base(entity)
        {
            PureGame = pureGame;
        }

        public void ControllerDPad(Direction direction)
        {
            if (direction != Direction.None && !CurrentWorld.CurrentlyMoving(Entity))
            {
                var entityMoving = CurrentWorld.EntityManager.EntityCurrentlyMoving(Entity);
                if (!entityMoving && Entity.FacingDirection != direction)
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
