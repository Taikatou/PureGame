using Microsoft.Xna.Framework;

namespace PureGame.Engine
{
    public class ExpiringKey <T>
    {
        public readonly T Key;
        public int TimeLeft { get; internal set; }
        public int TotalTime { get; }
        public float Progress => (float)TimeLeft / TotalTime;

        public ExpiringKey(T Key, int Time)
        {
            this.Key = Key;
            TimeLeft = Time;
            TotalTime = Time;
        }

        public void Update(GameTime time)
        {
            TimeLeft -= time.ElapsedGameTime.Milliseconds;
        }
    }
}
