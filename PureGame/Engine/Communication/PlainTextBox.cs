using Microsoft.Xna.Framework;

namespace PureGame.Engine.Communication
{
    public class PlainTextBox : ITextBox
    {
        private int _timer;

        public PlainTextBox(int timer)
        {
            _timer = timer;
        }

        public bool Complete => _timer <= 0;

        public void Update(GameTime time)
        {
            if (!Complete)
            {
                _timer -= time.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
