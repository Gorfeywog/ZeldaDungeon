using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Rooms
{
    public class Door : IEntity
    {
        private ISprite sprite;
        public Direction Dir { get; private set; }
        public Rectangle CurrentLoc { get; set; }
        public bool ReadyToDespawn { get => false; }
        public void DespawnEffect() { }
        public DrawLayer Layer { get => DrawLayer.VeryHigh; } 
        private DoorState state;
        public DoorState State 
        {
            get => state; 
            private set 
            {
                state = value;
                this.sprite = DoorSpriteFactory.Instance.CreateDoor(Dir, value);
            }
        }
        public bool CanPass { get => State == DoorState.Open || State == DoorState.Hole; }
        public Door(Point position, Direction dir, DoorState state)
        {
            CurrentLoc = new Rectangle(position, new Point((int)SpriteUtil.SpriteSize.DoorX * SpriteUtil.SCALE_FACTOR, 
                (int)SpriteUtil.SpriteSize.DoorY * SpriteUtil.SCALE_FACTOR));
            this.Dir = dir;
            this.State = state;
            this.sprite = DoorSpriteFactory.Instance.CreateDoor(Dir, State);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();

        public bool Unlock()
        {
            if (State == DoorState.Locked)
            {
                State = DoorState.Open;
                return true;
            }
            return false;
        }
        public bool Explode()
        {
            if (State == DoorState.BlockedHole)
            {
                State = DoorState.Hole;
                return true;
            }
            return false;
        }

        public bool Open()
        {
            if (State == DoorState.Closed)
            {
                State = DoorState.Open;
                return true;
            }
            return false;
        }
    }
}

