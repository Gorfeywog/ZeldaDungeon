using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Rooms
{
    public class Walls
    {
        private ISprite sprite = SpecialSpriteFactory.Instance.CreateWalls();
        public Point CurrentPoint { get; private set; }
        public Rectangle CurrentRect { get => new Rectangle(CurrentPoint.X, CurrentPoint.Y, 256, 176); }
        public Walls(Point position)
        {
            CurrentPoint = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentRect);
        }
        public void Update() => sprite.Update();
    }
}

