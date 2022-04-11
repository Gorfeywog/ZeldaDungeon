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
        public static readonly int PAUSE_ITEM_BIG_OFFSET_X = 129;
        public static readonly int PAUSE_ITEM_SMALL_OFFSET_X = 69;
        public static readonly int PAUSE_ITEM_OFFSET_Y = 49;
        private Game1 g;
        private PauseInventory pauseItems;
        private IItem item;

        public ItemMenuManager(Game1 g)
        {
            pauseItems = new PauseInventory(g);
            this.g = g;
        }

        public void Draw(SpriteBatch spriteBatch, Point pausePos)
        { 


        }

        public void Update()
        {
            pauseItems.Update();
        }

    }
}
