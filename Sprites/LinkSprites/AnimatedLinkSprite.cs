using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.LinkSprites
{
    class AnimatedLinkSprite : ISprite
    {
        private Texture2D spritesheet;
        private int width;
        private int height;
        private Rectangle[] sourceRectangles;
        private bool damaged;
        private static readonly Color[] damageColors = { Color.Red, Color.White };
        private static readonly int damageRepeatDelay = 5;
        private static readonly int waitTime = 10; // how many Updates to wait between cycling frame
        public AnimatedLinkSprite(Texture2D spritesheet, int width, int height, Point[] topLefts, bool damaged = false)
        {
            this.spritesheet = spritesheet;
            this.width = width;
            this.height = height;
            this.damaged = damaged;
            sourceRectangles = new Rectangle[topLefts.Length];
            for (int i = 0; i < topLefts.Length; i++)
            {
                sourceRectangles[i] = new Rectangle(topLefts[i].X, topLefts[i].Y, width, height);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Color currentColor;
            if (damaged)
            {
                currentColor = damageColors[damageColorIndex];
            }
            else
            {
                currentColor = Color.White;
            }
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width * 2, height * 2);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangles[frameNo], currentColor);
        }

        private int frameNo = 0; // index of current frame in the array
        private int currentWait = waitTime;
        private int damageColorTimer = damageRepeatDelay;
        private int damageColorIndex = 0;
        public void Update()
        {
            if (currentWait == 1)
            {
                currentWait = waitTime;
                int maxFrames = sourceRectangles.Length;
                frameNo = (frameNo + 1) % maxFrames;
            } else
            {
                currentWait--;
            }
            if (damaged)
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
