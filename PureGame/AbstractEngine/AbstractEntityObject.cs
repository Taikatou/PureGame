using SmallGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PureGame.AbstractEngine
{
    public abstract class AbstractEntityObject : GameObject
    {
        public abstract Vector2 Position { get; set; }
    }
}
