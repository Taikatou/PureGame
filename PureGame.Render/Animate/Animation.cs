using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PureGame.Client.Animate
{
    public class Animation
    {
        private readonly List<AnimationFrame> _frames = new List<AnimationFrame>();
        private TimeSpan _timeIntoAnimation;

        public TimeSpan Duration
        {
            get
            {
                var totalSeconds = _frames.Sum(frame => frame.Duration.TotalSeconds);

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public void AddFrame(Rectangle rectangle, TimeSpan duration)
        {
            var newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration
            };

            _frames.Add(newFrame);
        }

        public void Update(GameTime gameTime)
        {
            var secondsIntoAnimation =
                _timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;


            var remainder = secondsIntoAnimation % Duration.TotalSeconds;

            _timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }

        public Rectangle CurrentRectangle
        {
            get
            {
                AnimationFrame currentFrame = null;

                // See if we can find the frame
                foreach (var frame in _frames)
                {
                    TimeSpan accumulatedTime;
                    if (accumulatedTime + frame.Duration >= _timeIntoAnimation)
                    {
                        currentFrame = frame;
                        break;
                    }
                    accumulatedTime += frame.Duration;
                }

                // If no frame was found, then try the last frame, 
                // just in case timeIntoAnimation somehow exceeds Duration
                if (currentFrame == null)
                {
                    currentFrame = _frames[_frames.Count - 1];
                }

                // If we found a frame, return its rectangle, otherwise
                // return an empty rectangle (one with no width or height)
                if (currentFrame != null)
                {
                    return currentFrame.SourceRectangle;
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }
    }
}
