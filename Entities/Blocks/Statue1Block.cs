using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class Statue1Block : IBlock
    {

        private ISprite sprite = BlockSpriteFactory.Instance.CreateStatue1Block();
        public Rectangle CurrentLoc { get; set; }
        public Statue1Block(Point position)
        {
            CurrentLoc = new Rectangle(position, new Point(16, 16));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, CurrentLoc);
        }
        public void Update() => sprite.Update();
        public void DespawnEffect() { }
        public bool ReadyToDespawn => false;

    }
}

