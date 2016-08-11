using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public class ExpiringKey <T>
    {
        protected T Key;
        public int TimeLeft { get; internal set; }

        public ExpiringKey(T Key, int TimeLeft)
        {
            this.Key = Key;
            this.TimeLeft = TimeLeft;
        }

        public void Update(GameTime time)
        {
            TimeLeft -= time.ElapsedGameTime.Milliseconds;
        }
    }
}
