using Microsoft.Xna.Framework;
using PureGame.Engine;
using PureGame.Engine.Controls;
using PureGame.Engine.EntityData;
using PureGame.Engine.World;

namespace PureGame.Client.FocusLayers
{
    public class PureGameLayer : ILayer, IWorldController
    {
        public const int CachedMovementResetValue = -1;
        public int CachedMovement = CachedMovementResetValue;
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

        private void UpdateCharacter(IController controller, GameTime time)
        {
            if (controller.Buttons[(int)Controls.A].NewActive)
            {
                CurrentWorld.ProccessInteraction(_entity);
            }
            var cachedMoveDiection = GetMovementDirection(controller);
            if (cachedMoveDiection != Direction.None)
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
            _entity.Running = controller.Buttons[(int)Controls.B].Active;
        }

        public void UpdateController(IController controller, GameTime time)
        {
            if (!_entity.CurrentlyInteracting)
            {
                UpdateCharacter(controller, time);
            }
            else
            {
                
            }
        }

        public void UpdateData(GameTime time)
        {
            PureGame.Update(time);
        }

        public Direction GetMovementDirection(IController controller)
        {
            // Return cached direction
            if (CachedMovement != CachedMovementResetValue && controller.Buttons[CachedMovement].Active)
            {
                return (Direction)CachedMovement;
            }
            else
            {
                CachedMovement = CachedMovementResetValue;
            }
            // Else look for another button
            for (var i = 0; i < (int)Direction.None; i++)
            {
                if (controller.Buttons[i].Active)
                {
                    CachedMovement = i;
                    return (Direction)i;
                }
            }
            // Else return false
            return Direction.None;
        }

        public void Interact(IWorldController worldController)
        {
            
        }
    }
}
