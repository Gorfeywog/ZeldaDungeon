using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.LinkSprites
{
    class StaticLinkSprite : ISprite
    {
        private Texture2D spritesheet;
        private int width;
        private int height;
        private bool damage;
        private Rectangle sourceRectangle;
        private static readonly Color[] damageColors = { Color.Red, Color.White };
        private static readonly int damageRepeatDelay = 5;
        public StaticLinkSprite(Texture2D spritesheet, int width, int height, Point topLeft) : this(spritesheet, width, height, topLeft, false) { } // chained to the other one. use default arg instead maybe?
        public StaticLinkSprite(Texture2D spritesheet, int width, int height, Point topLeft, bool damage)
        {
            this.spritesheet = spritesheet;
            this.width = width;
            this.height = height;
            this.damage = damage;
            sourceRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width * 2, height * 2);
            Color currentColor;
            if (damage)
            {
                currentColor = damageColors[damageColorIndex];
            }
            else
            {
                currentColor = Color.White;
            }
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangle, currentColor);
        }

        private int damageColorTimer = damageRepeatDelay;
        private int damageColorIndex = 0;
        public void Update()
        {
            if (damage)
            {
                damageColorTimer--;
                if (damageColorTimer == 0)
                {
                    damageColorIndex = (damageColorIndex + 1) % damageColors.Length;
                    damageColorTimer = damageRepeatDelay;
                }
            }
        }
    }
}
