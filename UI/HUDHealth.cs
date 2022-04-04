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
        private readonly int HEART_NUM = 3;
        public HUDHealth()
        {
            fullHeart = HUDSpriteFactory.Instance.CreateFullHeart();
            emptyHeart = HUDSpriteFactory.Instance.CreateEmptyHeart();
            halfHeart = HUDSpriteFactory.Instance.CreateHalfHeart();
        }

        public void Draw(SpriteBatch spriteBatch, Point heartTopLeft)
        {
            for (int i = 0; i < HEART_NUM; i++)
            {
                int scaledWidth = (int)SpriteUtil.SpriteSize.HeartContainerWidth * SpriteUtil.SCALE_FACTOR;
                Point dest = heartTopLeft + new Point(i * scaledWidth, 0);
                Point size = new Point(scaledWidth);
                Rectangle destRect = new Rectangle(dest, size);
                fullHeart.Draw(spriteBatch, destRect);
            }
            
        }

        public void Update()
        {

        }
    }
}
