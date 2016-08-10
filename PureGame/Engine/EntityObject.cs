using System;
using Microsoft.Xna.Framework;
using PureGame.AbstractEngine;
using SmallGame.GameObjects;

namespace PureGame.Engine
{
    public class EntityObject : AbstractEntityObject
    {
        public string FileName;
        public Vector2 position;
        public EntityObject()
        {

        }

        public override Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
    }
}
