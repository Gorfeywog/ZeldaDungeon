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
        public Door(Point position, Direction dir, DoorState state)
        {
            CurrentPoint = position;
            this.Dir = dir;
            this.State = state;
            this.sprite = DoorSpriteFactory.Instance.CreateDoor(Dir, State);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentPoint);
        }
        public void Update() => sprite.Update();
    }
}
