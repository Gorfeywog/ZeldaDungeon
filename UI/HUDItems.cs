using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaDungeon.UI
{
    class HUDItems
    {
        private ISprite sword;
        private ISprite boomerang;
        private ISprite bow;
        private ISprite specialBoomerang;
        public HUDItems()
        {
            sword = ItemSpriteFactory.Instance.CreateSword(Entities.Direction.Up);
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            int scaledWidth = (int)SpriteUtil.SpriteSize.SwordWidth * SpriteUtil.SCALE_FACTOR;
            int scaledHeight = (int)SpriteUtil.SpriteSize.SwordLength * SpriteUtil.SCALE_FACTOR;
            Point dest = itemTopLeft;
            Point size = new Point(scaledWidth, scaledHeight);
            Rectangle destRect = new Rectangle(dest, size);
            sword.Draw(spriteBatch, destRect);
        }

        public void Update()
        {

        }
    }
}
