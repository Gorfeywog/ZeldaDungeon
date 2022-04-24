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
        private static readonly int RADIX = 7;
        private ISprite[] sprites = new ISprite[RADIX];
        private IItem[] items = new IItem[RADIX];
        private Point[] dests = new Point[RADIX];
        private IDictionary<IItem, int> itemDict;
        private bool special = true;
        private LinkInventory inventory;
        private Game1 g;
        private Point mapTopLeft = new Point((int)SpriteUtil.MAP_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.MAP_POS_Y * SpriteUtil.SCALE_FACTOR);
        private Point compassTopLeft = new Point((int)SpriteUtil.COMPASS_POS_X * SpriteUtil.SCALE_FACTOR, (int)SpriteUtil.COMPASS_POS_Y * SpriteUtil.SCALE_FACTOR);
        private ItemSelect itemSelect;
        private int j, k, compassIndex = 3, mapIndex = 4;


        public PauseInventory(Game1 g)
        {
            this.g = g;
            sprites[0] = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            sprites[1] = ItemSpriteFactory.Instance.CreateBow(); 
            sprites[2] = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            sprites[3] = ItemSpriteFactory.Instance.CreateCompass();
            sprites[4] = ItemSpriteFactory.Instance.CreateMap(); 
            sprites[5] = ItemSpriteFactory.Instance.CreateCandle(false); 
            sprites[6] = ItemSpriteFactory.Instance.CreateBomb(); 

            items[0] = new BoomerangItem(g, !special);
            items[1] = new BowItem(g);
            items[2] = new BoomerangItem(g, special);  
            items[3] = new CompassItem();
            items[4] = new MapItem();
            items[5] = new CandleItem(g, false);
            items[6] = new BombItem(g);
            inventory = g.Player.GetInv();
            itemSelect = g.Select;
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            itemDict = inventory.GetDict();
            int scaledWidth, scaledHeight;
            Point size = new Point();
            Rectangle destRect;
            j = 0;
            k = 0;

            for (int i = 0; i < RADIX; i++)
            {
                if (itemDict.ContainsKey(items[i]) && i != compassIndex && i != mapIndex)  
                {
                    scaledWidth = (int)SpriteUtil.SpriteSize.InventoryItemX * SpriteUtil.SCALE_FACTOR;
                    scaledHeight = (int)SpriteUtil.SpriteSize.InventoryItemY * SpriteUtil.SCALE_FACTOR;
                    dests[i] = itemTopLeft + new Point(j * (int)SpriteUtil.PAUSE_ITEM_OFFSET_X, k * ((int)SpriteUtil.PAUSE_ITEM_OFFSET_Y + SpriteUtil.PAUSE_ITEM_Y_GAP));
                    size = new Point(scaledWidth, scaledHeight);
                    destRect = new Rectangle(dests[i], size);
                    sprites[i].Draw(spriteBatch, destRect);
                    j++;
                    if (j > 4)
                    {
                        j = 0;
                        k++;
                    }
                } else if (itemDict.ContainsKey(items[i]) && i == compassIndex)
                {
                    scaledWidth = (int)SpriteUtil.SpriteSize.CompassWidth * SpriteUtil.SCALE_FACTOR;
                    scaledHeight = (int)SpriteUtil.SpriteSize.CompassLength * SpriteUtil.SCALE_FACTOR;
                    dests[i] = itemTopLeft - compassTopLeft;
                    size = new Point(scaledWidth, scaledHeight);
                    destRect = new Rectangle(dests[i], size);
                    sprites[i].Draw(spriteBatch, destRect);
                } else if (itemDict.ContainsKey(items[i]) && i == mapIndex)
                {
                    scaledWidth = (int)SpriteUtil.SpriteSize.MapWidth * SpriteUtil.SCALE_FACTOR;
                    scaledHeight = (int)SpriteUtil.SpriteSize.MapLength * SpriteUtil.SCALE_FACTOR;
                    dests[i] = itemTopLeft - mapTopLeft;
                    size = new Point(scaledWidth, scaledHeight);
                    destRect = new Rectangle(dests[i], size);
                    sprites[i].Draw(spriteBatch, destRect);
                }
            }
            DrawSelectionCursor(spriteBatch, itemTopLeft);
        }
        private void DrawSelectionCursor(SpriteBatch spriteBatch, Point itemTopLeft)
        {
            ISprite cursor = ItemSpriteFactory.Instance.CreateSelectionIndicator();
            var selected = itemSelect.SelectedItem();
            Point loc = new Point();
            int scaledWidth = (int)SpriteUtil.SpriteSize.InventoryItemX * SpriteUtil.SCALE_FACTOR;
            int scaledHeight = (int)SpriteUtil.SpriteSize.InventoryItemY * SpriteUtil.SCALE_FACTOR;
            if (selected == null)
            {
                loc = itemTopLeft;
            }
            else
            {
                for (int i = 0; i < RADIX; i++)
                {
                    if (selected.Equals(items[i]))
                    {
                        loc = dests[i];
                    }
                }
            }
            var size = new Point(scaledWidth, scaledHeight);
            var rect = new Rectangle(loc, size);
            cursor.Draw(spriteBatch, rect);


        }
        public void Update()
        {
            inventory = g.Player.GetInv();
        }
    }
}
