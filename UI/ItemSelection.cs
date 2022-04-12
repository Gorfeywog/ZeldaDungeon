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
    class ItemSelection
    {
        private Point activeItemTopLeft = new Point((int)SpriteUtil.ACTIVE_ITEM_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.ACTIVE_ITEM_Y * SpriteUtil.SCALE_FACTOR);
        private ISprite boomerang;
        private ISprite bow;
        private ISprite bomb;
        private ISprite specialBoomerang;
        private ISprite rCandle;
        private ISprite bCandle;
        private IItem[] usableItems;
        private IDictionary<IItem, int> itemDict;
        private LinkInventory inventory;
        private Game1 g;
        private bool redCandle = true;
        private bool special = true;
        public ItemSelection(Game1 g)
        {
            this.g = g;
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            rCandle = ItemSpriteFactory.Instance.CreateCandle(redCandle);
            bCandle = ItemSpriteFactory.Instance.CreateCandle(!redCandle);
            bomb = ItemSpriteFactory.Instance.CreateBomb();
            usableItems[0] = new BowItem();
            usableItems[1] = new BoomerangItem(g, !special);
            usableItems[2] = new BoomerangItem(g, special);
            usableItems[3] = new CandleItem(g, redCandle);
            usableItems[4] = new CandleItem(g, !redCandle);
            usableItems[5] = new CandleItem(g, redCandle);
            usableItems[6] = new BombItem(g);
            inventory = g.Player.GetInv();
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            itemDict = inventory.GetDict();
            


        }

        public void Update()
        {

        }
    }
}
