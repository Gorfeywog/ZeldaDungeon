using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI.Managers
{


    class ItemMenuManager
    {
        public static readonly int FIRST_PAUSE_ITEM_OFFSET_X = 129;
        private Game1 g;
        private PauseInventory pauseItems;

        public ItemMenuManager(Game1 g)
        {
            pauseItems = new PauseInventory(g);
            this.g = g;
        }

        public void Draw(SpriteBatch spriteBatch, Point pausePos)
        {
            Point itemsTopLeft = pausePos + new Point(SpriteUtil.SCALE_FACTOR * FIRST_PAUSE_ITEM_OFFSET_X, SpriteUtil.SCALE_FACTOR * SpriteUtil.PAUSE_ITEM_OFFSET_Y);
            pauseItems.Draw(spriteBatch, itemsTopLeft);

        }

        public void Update()
        {
            pauseItems.Update();
        }

    }
}
