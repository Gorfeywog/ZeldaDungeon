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
        public Walls(Point position)
        {
            CurrentPoint = position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Rectangle(CurrentPoint, new Point(256 * SpriteUtil.SCALE_FACTOR, 176 * SpriteUtil.SCALE_FACTOR)));
        }
        public void Update() => sprite.Update();
    }
}

