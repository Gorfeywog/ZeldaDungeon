using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Rooms
{
    public class Door // should this and Walls be IEntities?
    {
        private ISprite sprite;
        public Direction Dir { get; private set; }
        public DoorState State { get; private set; }
        public Point CurrentPoint { get; private set; }
        public bool CanPass { get => State == DoorState.Open || State == DoorState.Hole; }
        public Door(Point position, Direction dir, DoorState state)
        {
            CurrentPoint = position;
            this.Dir = dir;
            this.State = state;
            this.sprite = DoorSpriteFactory.Instance.CreateDoor(Dir, State);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Rectangle(CurrentPoint.X, CurrentPoint.Y, (int)SpriteUtil.SpriteSize.DoorX * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.SpriteSize.DoorY * SpriteUtil.SCALE_FACTOR));
        }
        public void Update() => sprite.Update();
        public bool Unlock()
        {
            if (State == DoorState.Locked)
            {
                State = DoorState.Open;
            }
            return State != DoorState.Locked;
            // Just wanted the code to be compilable, don't actually know what you wanted here.
        }

        internal bool Explode()
        {
            throw new NotImplementedException();
            // Just wanted the code to be compilable, don't actually know what you wanted here.
        }
    }
}

