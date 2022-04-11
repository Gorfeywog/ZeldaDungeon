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
        private ISprite boomerang;
        private ISprite bow;
        private ISprite specialBoomerang;
        private ISprite compass;
        private ISprite map;
        private ISprite candle;
        private bool special = true;
        private bool redCandle = true;
        private IDictionary<IItem, int> itemDict;
        private LinkInventory inventory;
        private Game1 g;
        public PauseInventory(Game1 g)
        {
            this.g = g;
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            compass = ItemSpriteFactory.Instance.CreateCompass();
            map = ItemSpriteFactory.Instance.CreateMap();
            candle = ItemSpriteFactory.Instance.CreateCandle(redCandle);
            candle = ItemSpriteFactory.Instance.CreateCandle(!redCandle);
            inventory = g.Player.GetInv();
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            itemDict = inventory.GetDict();
            
            if (itemDict.ContainsKey(new BowItem()))
            {
                int scaledWidth = (int)SpriteUtil.SpriteSize.BowWidth * SpriteUtil.SCALE_FACTOR;
                int scaledHeight = (int)SpriteUtil.SpriteSize.BowLength * SpriteUtil.SCALE_FACTOR;
                Point dest = itemTopLeft;
                Point size = new Point(scaledWidth, scaledHeight);
                Rectangle destRect = new Rectangle(dest, size);
                bow.Draw(spriteBatch, destRect);
            }
            // check for normal boomerang
            if (itemDict.ContainsKey(new BoomerangItem(g, !special)))
            {

            }
            // check for special boomerang
            if (itemDict.ContainsKey(new BoomerangItem(g, special)))
            {

            }
            if (itemDict.ContainsKey(new CompassItem()))
            {

            }
            if (itemDict.ContainsKey(new MapItem()))
            {

            }
            if (itemDict.ContainsKey(new CandleItem(g, redCandle)))
            {

            }
            if (itemDict.ContainsKey(new CandleItem(g, !redCandle)))
            {

            }
            
            
        }

        public void Update()
        {

        }
    }
}
