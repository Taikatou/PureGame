using Microsoft.Xna.Framework;
using PureGame.Engine.Controllers;
using PureGame.Engine.EntityData;
using System;
using System.Diagnostics;

namespace PureGame.Client.Controllers
{
    public class KeyBoardController : IController
    {
        public SmartButton [] Buttons;
        private InputManager input_manager;
        public const int CachedMovementResetValue = -1;
        public int CachedMovement = CachedMovementResetValue;
        public int Timer = 0;
        public int TimerResetValue = 50;

        public Direction GetMovementDirection()
        {
            // Return cached direction
            if(CachedMovement != CachedMovementResetValue && Buttons[CachedMovement].Active)
            {
                return (Direction)CachedMovement;
            }
            else
            {
                CachedMovement = CachedMovementResetValue;
            }
            // Else look for another button
            for(int i = 0; i < (int)Direction.None; i++)
            {
                if(Buttons[i].Active)
                {
                    CachedMovement = i;
                    Debug.WriteLine(i);
                    return (Direction)i;
                }
            }
            // Else return false
            return Direction.None;
        }

        public void Update(IEntity entity, GameTime time)
        {
            input_manager.Update(time);
            if(Buttons[(int)Controls.A].NewActive)
            {
                entity.RequestInteraction();
            }
            Direction CachedMoveDiection = GetMovementDirection();
            if(CachedMoveDiection != Direction.None)
            {
                bool entity_moving = game.World.EntityManager.EntityCurrentlyMoving(entity);
                if (!entity_moving && entity.FacingDirection != CachedMoveDiection)
                {
                    entity.FacingDirection = CachedMoveDiection;
                    Timer = TimerResetValue;
                }
                else if (Timer <= 0)
                {
                    entity.MovementDirection = CachedMoveDiection;
                    // this order is important move then request movement
                    entity.RequestMovement();
                }
                else
                {
                    Timer -= time.ElapsedGameTime.Milliseconds;
                }
            }
            entity.Running = Buttons[(int)Controls.B].NewActive;
        }
        IPureGame game;
        public KeyBoardController(IPureGame game)
        {
            this.game = game;
            input_manager = new InputManager(this);
            var ControlsCount = Enum.GetNames(typeof(Controls)).Length;
            Buttons = new SmartButton[ControlsCount];
            for(var i = 0; i < Buttons.Length; i++)
            {
                Buttons[i] = new SmartButton();
            }
        }
    }
}
