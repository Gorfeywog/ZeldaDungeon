﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.UI.Managers
{
    class ItemManager
    {
        public static readonly int BOMB_GRID_LENGTH = 8;
        public static readonly int HUD_ITEM1_OFFSET_X = 128;
        public static readonly int HUD_ITEM_OFFSET_Y = 24;
        public static readonly int HUD_ITEM2_OFFSET_X = 152;
        private Game1 g;
        private HUDItems hudItems;
        public ItemManager(Game1 g)
        {
            this.g = g;
            hudItems = new HUDItems();
        }
        public void Draw(SpriteBatch spriteBatch, Point hudPos)
        {
            Point hudItem1TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM1_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            Point hudItem2TopLeft = hudPos + new Point(SpriteUtil.SCALE_FACTOR * HUD_ITEM2_OFFSET_X, SpriteUtil.SCALE_FACTOR * HUD_ITEM_OFFSET_Y);
            hudItems.Draw(spriteBatch, hudItem1TopLeft);
            hudItems.Draw(spriteBatch, hudItem2TopLeft);

        }

        public void Update()
        {
            hudItems.Update();
        }
    }
}