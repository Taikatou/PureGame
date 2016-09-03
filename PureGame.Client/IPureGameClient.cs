using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PureGame.Engine.Controls;

namespace PureGame.Client
{
    public interface IPureGameClient
    {
        void Update(GameTime time);
        Stack<ILayer> Layers { get; }
    }
}
