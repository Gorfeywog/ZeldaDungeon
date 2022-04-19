using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI.Managers
{
    class ItemHUDManager
    {
        public static readonly int BOMB_GRID_LENGTH = 8;
        public static readonly int HUD_ITEM1_OFFSET_X = 128;
        public static readonly int HUD_ITEM_OFFSET_Y = 24;
        public static readonly int HUD_ITEM2_OFFSET_X = 152;

        private Game1 g;
        private HUDItems hudItem;


        public ItemHUDManager(Game1 g)
        {
            this.g = g;
            hudItem = new HUDItems();
            
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudItem1TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM1_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            Point hudItem2TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM2_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            hudItem.Draw(spriteBatch, hudItem1TopLeft, hudItem2TopLeft, g.Select.SelectedItem());

        }

        public void Update()
        {
            hudItem.Update();
        }
    }
}
