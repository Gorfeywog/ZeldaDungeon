using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.InventoryItems;


namespace ZeldaDungeon.UI
{
    class RupeeManager
    {
        public static readonly int HEART_GRID_LENGTH = 8;
        public static readonly int HUD_RUPEE_OFFSET_X = 96;
        public static readonly int HUD_RUPEE_OFFSET_Y = 16;
        private Game1 g;
        private HUDCount hudRupeeCount;
        private IItem item;
        public RupeeManager(Game1 g)
        {
            this.g = g;
            hudRupeeCount = new HUDCount(g);
            item = new RupeeItem();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudMapTopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_RUPEE_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_RUPEE_OFFSET_Y);
            hudRupeeCount.Draw(spriteBatch, hudMapTopLeft, item);
        }

        public void Update()
        {
            hudRupeeCount.Update();
        }
    }
}
