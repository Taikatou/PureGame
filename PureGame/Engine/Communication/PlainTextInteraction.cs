using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public class PlainTextInteraction : IInteraction
    {
        private int _timer;
        public IEntity Entity { get; }
        public bool Complete => _timer <= 0;
        public virtual string Type => "PlainTextInteraction";
        public PlainTextInteraction(IEntity entity, int timer)
        {
            _timer = timer;
            Entity = entity;
        }

        public void Interact()
        {
            Entity.Talking = false;
        }

        public void Update(GameTime time)
        {
            if (!Complete)
            {
                _timer -= time.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
