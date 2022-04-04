using System;
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
        public HUDCount(Game1 g)
        {
            inventory = g.Player.GetInv();
            x = HUDSpriteFactory.Instance.CreateX();
            nums = new ISprite[TOTAL_NUM];
            nums[0] = HUDSpriteFactory.Instance.CreateNumber0();
            nums[1] = HUDSpriteFactory.Instance.CreateNumber1();
            nums[2] = HUDSpriteFactory.Instance.CreateNumber2();
            nums[3] = HUDSpriteFactory.Instance.CreateNumber3();
            nums[4] = HUDSpriteFactory.Instance.CreateNumber4();
            nums[5] = HUDSpriteFactory.Instance.CreateNumber5();
            nums[6] = HUDSpriteFactory.Instance.CreateNumber6();
            nums[7] = HUDSpriteFactory.Instance.CreateNumber7();
            nums[8] = HUDSpriteFactory.Instance.CreateNumber8();
            nums[9] = HUDSpriteFactory.Instance.CreateNumber9();
            
            
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

                for (int i = 1; i < SPRITE_NUM; i++)
                {
                    dest = topLeft + new Point(i * scaledWidth, 0);
                    destRect = new Rectangle(dest, size);
                    if (itemCount > 19 && i == pos1)
                    {
                        nums[2].Draw(spriteBatch, destRect);
                        itemCount = itemCount - (TOTAL_NUM * 2);
                    }
                    else if (itemCount > 9 && i == pos1)
                    {
                        nums[1].Draw(spriteBatch, destRect);
                        itemCount = itemCount - TOTAL_NUM;
                    }
                    else if (i == pos1)
                    {
                        nums[0].Draw(spriteBatch, destRect);
                    }
                    else
                    {
                        nums[itemCount].Draw(spriteBatch, destRect);
                    }
                }
                
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

        }
    }
}
