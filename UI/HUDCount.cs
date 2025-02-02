﻿using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.Entities.Link;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.UI
{
    class HUDCount
    {
        private ISprite x;
        private ISprite[] nums;
        private int xPos = 0;
        private int pos1 = 1;
        private int pos2 = 2;
        private readonly int TOTAL_NUM = 10;
        private readonly int SPRITE_NUM = 3;
        private LinkInventory inventory;
        private IDictionary<IItem, int> itemDict;
        private const int RADIX = 10;
        private Game1 g;
        public HUDCount(Game1 g)
        {
            inventory = g.Player.GetInv();
            x = UISpriteFactory.Instance.CreateX();
            nums = new ISprite[TOTAL_NUM];
            this.g = g;
            for (int i = 0; i < RADIX; i++)
            {
                nums[i] = UISpriteFactory.Instance.CreateNumber(i);
            }
            
            
        }

        public void Draw(SpriteBatch spriteBatch, Point topLeft, IItem item)
        {
            
            itemDict = inventory.GetDict();
            int scaledWidth = (int)SpriteUtil.SpriteSize.HUDNumberWidth * SpriteUtil.SCALE_FACTOR;
            Point dest = topLeft + new Point(xPos * scaledWidth, 0);
            Point size = new Point(scaledWidth);
            Rectangle destRect = new Rectangle(dest, size);
            x.Draw(spriteBatch, destRect);
            if (itemDict.ContainsKey(item))
            {
                int itemCount = itemDict[item];
                dest = topLeft + new Point(pos1 * scaledWidth, 0);
                destRect = new Rectangle(dest, size);
                nums[itemCount / 10].Draw(spriteBatch, destRect);
                dest = topLeft + new Point(pos2 * scaledWidth, 0);
                destRect = new Rectangle(dest, size);
                nums[itemCount % 10].Draw(spriteBatch, destRect);
            }
            else
            {
                for (int i = 1; i < SPRITE_NUM; i++)
                {
                    dest = topLeft + new Point(i * scaledWidth, 0);
                    destRect = new Rectangle(dest, size);
                    nums[0].Draw(spriteBatch, destRect);
                }
            }

        }

        public void Update()
        {
            inventory = g.Player.GetInv();
        }
    }
}
