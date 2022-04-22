using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ZeldaDungeon.UI
{
    class HUDHealth
    {
        private ISprite fullHeart;
        private ISprite emptyHeart;
        private ISprite halfHeart;
        private int fullCount = 0;
        private int halfCount = 0;
        private int emptyCount = 0;
        public HUDHealth()
        {
            fullHeart = UISpriteFactory.Instance.CreateFullHeart();
            emptyHeart = UISpriteFactory.Instance.CreateEmptyHeart();
            halfHeart = UISpriteFactory.Instance.CreateHalfHeart();
        }

        public void Draw(SpriteBatch spriteBatch, Point heartTopLeft)
        {
            int heartCount = fullCount + halfCount + emptyCount;
            for (int i = 0; i < heartCount; i++)
            {
                int scaledWidth = (int)SpriteUtil.SpriteSize.HeartContainerWidth * SpriteUtil.SCALE_FACTOR;
                Point dest = heartTopLeft + new Point(i * scaledWidth, 0);
                Point size = new Point(scaledWidth);
                Rectangle destRect = new Rectangle(dest, size);
                if (i < fullCount)
                {
                    fullHeart.Draw(spriteBatch, destRect);
                }
                else if (i < fullCount + halfCount)
                {
                    halfHeart.Draw(spriteBatch, destRect);
                }
                else
                {
                    emptyHeart.Draw(spriteBatch, destRect);
                }
            }
            
        }

        public void Update(int fullCount, int halfCount, int emptyCount)
        {
            this.fullCount = fullCount;
            this.halfCount = halfCount;
            this.emptyCount = emptyCount;
        }
    }
}
