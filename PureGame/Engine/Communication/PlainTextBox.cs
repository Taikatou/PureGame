using Microsoft.Xna.Framework;
using PureGame.Engine.EntityData;

namespace PureGame.Engine.Communication
{
    public class PlainTextBox : ITextBox
    {
        private int _timer;
        private IEntity _entity;

        public PlainTextBox(IEntity entity, int timer)
        {
            _timer = timer;
            _entity = entity;
        }

        public bool Complete => _timer <= 0;

        public void Interact()
        {
            _entity.Talking = false;
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
