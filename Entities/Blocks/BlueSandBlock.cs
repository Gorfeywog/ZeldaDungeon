using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Blocks
{
    public class BlueSandBlock : IBlock
    {
        private ISprite sprite = BlockSpriteFactory.Instance.CreateBlueSandBlock();
        public CollisionHeight Height { get => CollisionHeight.Floor; }

        public Rectangle CurrentLoc { get; set; }
        public BlueSandBlock(Point position)
        {
            int width = (int)SpriteUtil.SpriteSize.GenericBlockX;
			int height = (int)SpriteUtil.SpriteSize.GenericBlockY;
			CurrentLoc = new Rectangle(position, new Point(width * SpriteUtil.SCALE_FACTOR, height * SpriteUtil.SCALE_FACTOR));
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

