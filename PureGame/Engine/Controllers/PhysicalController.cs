using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;
using System;
using System.Diagnostics;

namespace PureGame.Engine.Controllers
{
    public class PhysicalController : IController
    {
        public SmartButton [] Buttons;
        private InputManager input_manager;
        public int CachedMovement = -1;

        public Direction MoveDirection
        {
            get
            {
                // Return cached direction
                if(CachedMovement >= 0 && Buttons[CachedMovement].Active)
                {
                    return (Direction)CachedMovement;
                }
                else
                {
                    CachedMovement = -1;
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
        }

        public void Update(PlayerEntityObject entity, GameTime time)
        {
            input_manager.Update(time);
            entity.RequestInteraction = Buttons[(int)Controls.A].NewActive;
            Direction CachedMoveDiection = MoveDirection;
            entity.RequestMovement = CachedMoveDiection != Direction.None;
            if (entity.RequestMovement)
            {
                entity.MovementDirection = CachedMoveDiection;
                entity.Running = Buttons[(int)Controls.B].Active;
            }
        }

        public PhysicalController()
        {
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
