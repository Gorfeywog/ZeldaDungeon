using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;
using ZeldaDungeon.InventoryItems;


namespace ZeldaDungeon.UI
{
    class BombManager
    {
        public static readonly int BOMB_GRID_LENGTH = 8;
        public static readonly int HUD_RUPEE_OFFSET_X = 96;
        public static readonly int HUD_RUPEE_OFFSET_Y = 40;
        private Game1 g;
        private HUDCount hudBombCount;
        private IItem item;
        public BombManager(Game1 g)
        {
            this.g = g;
            hudBombCount = new HUDCount(g);
            item = new BombItem(g);
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudBombTopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_RUPEE_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_RUPEE_OFFSET_Y);
            hudBombCount.Draw(spriteBatch, hudBombTopLeft, item);
        }

        public void Update()
        {
            hudBombCount.Update();
        }
    }
}
