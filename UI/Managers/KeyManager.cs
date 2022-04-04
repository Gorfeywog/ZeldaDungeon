using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.UI
{
    class KeyManager
    {
        public static readonly int KEY_GRID_LENGTH = 8;
        public static readonly int HUD_KEY_OFFSET_X = 96;
        public static readonly int HUD_KEY_OFFSET_Y = 32;
        private Game1 g;
        private HUDCount hudKeyCount;
        private IItem item;
        public KeyManager(Game1 g)
        {
            this.g = g;
            hudKeyCount = new HUDCount(g);
            item = new KeyItem();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudMapTopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_KEY_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_KEY_OFFSET_Y);
            hudKeyCount.Draw(spriteBatch, hudMapTopLeft, item);
        }
        public void Update()
        {

            hudKeyCount.Update();
        }
    }
}
