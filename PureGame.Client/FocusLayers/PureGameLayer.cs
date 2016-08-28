using Microsoft.Xna.Framework;
using PureGame.Client.Controllers;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;

namespace PureGame.Client.FocusLayers
{
    public class PureGameLayer : ILayer
    {
        public const int CachedMovementResetValue = -1;
        public int CachedMovement = CachedMovementResetValue;
        public int Timer;
        public int TimerResetValue = 50;
        private readonly EntityObject _entity;
        public PureGame PureGame;

        public PureGameLayer(EntityObject entity, PureGame pureGame)
        {
            PureGame = pureGame;
            _entity = entity;
            Timer = 0;
        }

        public void UpdateController(IController controller, GameTime time)
        {
            if (controller.Buttons[(int)Controls.A].NewActive)
            {
                _entity.RequestInteraction();
            }
            Direction cachedMoveDiection = GetMovementDirection(controller);
            if (cachedMoveDiection != Direction.None)
            {
                bool entityMoving = PureGame.WorldManager.CurrentWorld.EntityManager.EntityCurrentlyMoving(_entity);
                if (!entityMoving && _entity.FacingDirection != cachedMoveDiection)
                {
                    _entity.FacingDirection = cachedMoveDiection;
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    _entity.MovementDirection = cachedMoveDiection;
                    _entity.RequestMovement();
                }
                else
                {
                    Timer -= time.ElapsedGameTime.Milliseconds;
                }
            }
            _entity.Running = controller.Buttons[(int)Controls.B].Active;
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
            for (int i = 0; i < (int)Direction.None; i++)
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
    }
}
