using System;
using Microsoft.Xna.Framework;

namespace PureGame.Engine.Communication
{
    public class PlainTextBox : ITextBox
    {
        private int Timer;

        public PlainTextBox(int timer)
        {
            Timer = timer;
        }

        public bool Complete => Timer <= 0;

        public void Update(GameTime time)
        {
            if (!Complete)
            {
                Timer -= time.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
