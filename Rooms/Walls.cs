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
        public Rectangle CurrentLoc { get; set; }
        public Walls(Point position)
        {
            CurrentLoc = new Rectangle(position, new Point(256 * SpriteUtil.SCALE_FACTOR, 176 * SpriteUtil.SCALE_FACTOR));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
    }
}

