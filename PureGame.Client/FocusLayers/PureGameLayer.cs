using System.Diagnostics;
using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.Controls;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Client.FocusLayers
{
    public class PureGameLayer : ILayer
    {
        public int Timer;
        public int TimerResetValue = 50;
        private readonly EntityObject _entity;
        public PureGame PureGame;

        public string Name => CurrentWorld.Name;

        public WorldArea CurrentWorld => PureGame.WorldManager.GetEntitysWorld(_entity);

        public PureGameLayer(EntityObject entity, PureGame pureGame)
        {
            PureGame = pureGame;
            _entity = entity;
            Timer = 0;
        }

        public void ControllerA()
        {
            if (!CurrentlyInteracting)
            {
                CurrentWorld.ProccessInteraction(_entity);
            }
            else
            {
                CurrentWorld.ProgressInteraction(_entity);
            }
        }

        public void ControllerDPad(Direction cachedMoveDiection, GameTime time)
        {
            if (!CurrentlyInteracting && !CurrentWorld.CurrentlyMoving(_entity))
            {
                var entityMoving = CurrentWorld.EntityManager.EntityCurrentlyMoving(_entity);
                if (!entityMoving && _entity.FacingDirection != cachedMoveDiection)
                {
                    _entity.FacingDirection = cachedMoveDiection;
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    _entity.MovementDirection = cachedMoveDiection;
                    CurrentWorld.ProccessMovement(_entity);
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
                _entity.Running = active;
            }
        }

        public bool CurrentlyInteracting => CurrentWorld.CurrentlyInteracting(_entity);

        public void UpdateData(GameTime time)
        {
            PureGame.Update(time);
        }

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
