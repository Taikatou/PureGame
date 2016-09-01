﻿using Microsoft.Xna.Framework;
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
        public Vector2 Position;
        public Direction MovementDirection;
        public Direction FacingDirection;
        public bool Running;
        public int Speed => Running ? _runningSpeed : _walkingSpeed;

        public EntityObject(Vector2 position, string fileName, Direction facingDirection = Direction.Down)
        {
            Position = position;
            FileName = fileName;
            FacingDirection = facingDirection;
            Id = IdFactory.NewId;
        }

        public EntityObject()
        {
        }
    }
}
