using Microsoft.Xna.Framework;
using PureGame.Client.Controllers;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using System.Diagnostics;

namespace PureGame.Client.FocusLayers
{
    public class PureGameLayer : ILayer
    {
        public const int CachedMovementResetValue = -1;
        public int CachedMovement = CachedMovementResetValue;
        public int Timer = 0;
        public int TimerResetValue = 50;
        private EntityObject entity;
        private PureGame PureGame;

        public PureGameLayer(EntityObject entity, PureGame PureGame)
        {
            this.PureGame = PureGame;
            this.entity = entity;
        }

        public void UpdateController(IController controller, GameTime time)
        {
            if (controller.Buttons[(int)Controls.A].NewActive)
            {
                entity.RequestInteraction();
            }
            Direction CachedMoveDiection = GetMovementDirection(controller);
            if (CachedMoveDiection != Direction.None)
            {
                bool entity_moving = PureGame.World.EntityManager.EntityCurrentlyMoving(entity);
                if (!entity_moving && entity.FacingDirection != CachedMoveDiection)
                {
                    entity.FacingDirection = CachedMoveDiection;
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    entity.MovementDirection = CachedMoveDiection;
                    entity.RequestMovement();
                }
                else
                {
                    Timer -= time.ElapsedGameTime.Milliseconds;
                }
            }
            entity.Running = controller.Buttons[(int)Controls.B].Active;
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
                    Debug.WriteLine(i);
                    return (Direction)i;
                }
            }
            // Else return false
            return Direction.None;
        }
    }
}
