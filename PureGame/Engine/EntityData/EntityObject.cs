﻿using System;
using Microsoft.Xna.Framework;
using PureGame.SmallGame;

namespace PureGame.Engine.EntityData
{
    public class EntityObject : IGameObject
    {
        public string Id { get; set; }
        public string Type { get; set; }
        private readonly int _walkingSpeed = 500;
        private readonly int _runningSpeed = 250;
        public string FileName;
        //This is not supposed to be changed directly only through entityManager
        public Vector2 Position { get; set; }
        public Direction MovementDirection;
        public Direction FacingDirection;
        public bool Running;
        public int Speed => Running ? _runningSpeed : _walkingSpeed;
        public event EventHandler<EventArgs> OnMoveEvent;

        public void MomentumChange() => OnMoveEvent?.Invoke(this, null);

        public override string ToString()
        {
            return $"EntityObject Id:{Id}, FileName:{FileName}, Position:{Position}";
        }
    }
}
