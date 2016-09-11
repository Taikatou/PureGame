using Microsoft.Xna.Framework;

namespace PureGame.Engine.World
{
    public class ExpiringKey <T>
    {
        public readonly T Key;
        public int TimeLeft { get; internal set; }
        public int TotalTime { get; }
        public float Progress => (float)TimeLeft / TotalTime;

        public ExpiringKey(T key, int time)
        {
            Key = key;
            TimeLeft = time;
            TotalTime = time;
        }

        public void Update(GameTime time)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= time.ElapsedGameTime.Milliseconds;
                if (TimeLeft < 0)
                {
                    TimeLeft = 0;
                }
            }
        }
    }
}
