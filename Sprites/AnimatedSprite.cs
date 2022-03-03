using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    class AnimatedSprite : ISprite
    {
        private Texture2D spritesheet;
        private Rectangle[] sourceRectangles;
        private int frameNo; // index of current frame in the array
        private int currentWait;

        private static readonly Color[] damageColors = { Color.Red, Color.White };
        private static readonly int damageRepeatDelay = 5;
        private bool damaged;
        private int damageColorTimer = damageRepeatDelay;
        private int damageColorIndex = 0;
        public AnimatedSprite(Texture2D spritesheet, Rectangle[] sourceRectangles)
        {
            InitiateConstructor(spritesheet, sourceRectangles, false);
        }

        public AnimatedSprite(Texture2D spritesheet, Rectangle[] sourceRectangles, bool damaged)
        {
            InitiateConstructor(spritesheet, sourceRectangles, damaged);
        }
        private void InitiateConstructor(Texture2D spritesheet, Rectangle[] sourceRectangles, bool damaged)
        {
            this.spritesheet = spritesheet;
            this.sourceRectangles = sourceRectangles;
            frameNo = 0;
            currentWait = SpriteUtil.WAIT_TIME;

            this.damaged = damaged;
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle)
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

            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangles[frameNo], currentColor);
        }

        public void Update()
        {
            if (currentWait == 1)
            {
                currentWait = SpriteUtil.WAIT_TIME;
                int maxFrames = sourceRectangles.Length;
                frameNo = (frameNo + 1) % maxFrames;
            }
            else
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
