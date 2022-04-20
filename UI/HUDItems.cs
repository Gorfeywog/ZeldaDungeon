using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.UI
{
    class HUDItems
    {
        private ISprite sword;
        private ISprite boomerang;
        private ISprite bow;
        private ISprite specialBoomerang;
        private ISprite rCandle;
        private ISprite bCandle;
        private ISprite bomb;
        public HUDItems()
        {
            sword = ItemSpriteFactory.Instance.CreateSword(Entities.Direction.Up);
            boomerang = ItemSpriteFactory.Instance.CreateWoodenBoomerang();
            bow = ItemSpriteFactory.Instance.CreateBow();
            specialBoomerang = EnemySpriteFactory.Instance.CreateStaticMagicBoomerangSprite();
            rCandle = ItemSpriteFactory.Instance.CreateCandle(true);
            bCandle = ItemSpriteFactory.Instance.CreateCandle(false);
            bomb = ItemSpriteFactory.Instance.CreateBomb();
        }

        public void Draw(SpriteBatch spriteBatch, Point itemTopLeft, Point swordTopLeft, IItem selection)
        {
            int scaledWidth = (int)SpriteUtil.SpriteSize.SwordWidth * SpriteUtil.SCALE_FACTOR;
            int scaledHeight = (int)SpriteUtil.SpriteSize.SwordLength * SpriteUtil.SCALE_FACTOR;
            Point size = new Point(scaledWidth, scaledHeight);
            Rectangle swordDestRect = new Rectangle(swordTopLeft, size);
            sword.Draw(spriteBatch, swordDestRect);
            if (selection == null) { return; } // only proceed if have an item selected!
            Rectangle itemDestRect = new Rectangle(itemTopLeft, size); // TODO - make it use its own size, not sword size
            ISprite sprite;
            if (selection is BoomerangItem b)
            {
                sprite = b.IsMagic ? specialBoomerang : boomerang;
            }
            else if (selection is BowItem)
            {
                sprite = bow;
            }
            else if (selection is CandleItem c)
            {
                sprite = c.IsRed ? rCandle : bCandle;
            }
            else if (selection is BombItem)
            {
                sprite = bomb;
            }
            else
            {
                return; // can't draw if we don't have a sprite for it
            }
            sprite.Draw(spriteBatch, itemDestRect);
        }

        public void Update()
        {

        }
    }
}
