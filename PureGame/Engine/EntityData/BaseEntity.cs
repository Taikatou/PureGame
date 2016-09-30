using System;
using Microsoft.Xna.Framework;
using PureGame.Engine.Communication;

namespace PureGame.Engine.EntityData
{
    public class BaseEntity : IEntity
    {
        public string Id { get; set; }
        private readonly int _walkingSpeed = 500;
        private readonly int _runningSpeed = 250;
        public string FileName { get; set; }
        //This is not supposed to be changed directly only through entityManager
        public Point Position { get; set; }
        public Direction MovementDirection { get; set; }
        public Direction FacingDirection { get; set; }
        public bool Running { get; set; }

        public bool Talking { get; set; }

        public int Speed => Running ? _runningSpeed : _walkingSpeed;
        public event EventHandler OnMoveEvent;

        public void MomentumChange() => OnMoveEvent?.Invoke(this, null);
        private ITextBox _interaction;
        public ITextBox Interaction
        {
            get
            {
                _interaction = new PlainTextBox(this, 5000);
                return _interaction;
            }
        }

        EventHandler IEntity.OnMoveEvent { get; set; }

        public override string ToString() => $"EntityObject Id:{Id}, FileName:{FileName}, Position:{Position}";
    }
}
