using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Entities.Link;

namespace ZeldaDungeon.UI
{
    class PauseInventory
    {
        private ISprite sword;
        private ISprite boomerang;
        private ISprite bow;
        private ISprite specialBoomerang;
        private IDictionary<IItem, int> itemDict;
        private LinkInventory inventory;
        public PauseInventory()
        {
            sword = ItemSpriteFactory.Instance.CreateSword(Entities.Direction.Up);
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            inventory = g.Player.GetInv();
    }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft, IItem item)
        {
            itemDict = inventory.GetDict();

            int swordScaledWidth = (int)(SpriteUtil.SpriteSize.SwordWidth - 1) * SpriteUtil.SCALE_FACTOR;
            int swordScaledHeight = (int)(SpriteUtil.SpriteSize.SwordLength - 2) * SpriteUtil.SCALE_FACTOR;
            Point dest = itemTopLeft;
            Point swordSize = new Point(swordScaledWidth, swordScaledHeight);
            Rectangle destRect = new Rectangle(dest, swordSize);
            sword.Draw(spriteBatch, destRect);
        }

        public void Update()
        {

        }
    }
}
