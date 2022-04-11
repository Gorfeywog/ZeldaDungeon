using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI.Managers
{
    class ItemManager
    {
        public static readonly int BOMB_GRID_LENGTH = 8;
        public static readonly int HUD_ITEM1_OFFSET_X = 128;
        public static readonly int HUD_ITEM_OFFSET_Y = 24;
        public static readonly int HUD_ITEM2_OFFSET_X = 152;
        public static readonly int PAUSE_ITEM_BIG_OFFSET_X = 129;
        public static readonly int PAUSE_ITEM_SMALL_OFFSET_X = 69;
        public static readonly int PAUSE_ITEM_OFFSET_Y = 49;
        private Game1 g;
        private HUDItems hudItems;
        private PauseInventory pauseItems;
        private IItem item;
        public ItemManager(Game1 g)
        {
            this.g = g;
            hudItems = new HUDItems();
            pauseItems = new PauseInventory();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos, Point pausePos)
        {
            int itemSlot = 0;
            Point hudItem1TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM1_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            Point hudItem2TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM2_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            hudItems.Draw(spriteBatch, hudItem1TopLeft);
            hudItems.Draw(spriteBatch, hudItem2TopLeft);

            Point pauseItemsSmallTopLeft = pausePos + new Point(PAUSE_ITEM_SMALL_OFFSET_X * SpriteUtil.SCALE_FACTOR, PAUSE_ITEM_OFFSET_Y * SpriteUtil.SCALE_FACTOR);
            pauseItems.Draw(spriteBatch, pauseItemsSmallTopLeft, item);

            for (int i = 0; i <= itemSlot; i++)
            {
                if (i <= 3)
                {
                    Point pauseItemsBigTopLeft = pausePos + new Point(i * (100) + PAUSE_ITEM_BIG_OFFSET_X * SpriteUtil.SCALE_FACTOR, PAUSE_ITEM_OFFSET_Y * SpriteUtil.SCALE_FACTOR);
                    pauseItems.Draw(spriteBatch, pauseItemsBigTopLeft, item);
                }
                else if (i <= 7)
                {
                    Point pauseItemsBigTopLeft = pausePos + new Point((i - 4) * (100) + PAUSE_ITEM_BIG_OFFSET_X * SpriteUtil.SCALE_FACTOR, (18 + PAUSE_ITEM_OFFSET_Y) * SpriteUtil.SCALE_FACTOR);
                    pauseItems.Draw(spriteBatch, pauseItemsBigTopLeft, item);
                }
                
            }
            

        }

        public void Update()
        {
            hudItems.Update();
            pauseItems.Update();
        }
    }
}
