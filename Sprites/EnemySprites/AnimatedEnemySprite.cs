using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.EnemySprites
{
    class AnimatedEnemySprite : ISprite
    {
        private Texture2D spritesheet;
        private int width;
        private int height;
        private Rectangle[] sourceRectangles;
        private int frameNo; // index of current frame in the array
        private static readonly int waitTime = 8; // how many Updates to wait between cycling frame
        private int currentWait;
        public AnimatedEnemySprite(Texture2D spritesheet, int width, int height, Point[] topLefts)
        {
            this.spritesheet = spritesheet;
            this.width = width;
            this.height = height;
            sourceRectangles = new Rectangle[topLefts.Length];
            for (int i = 0; i < topLefts.Length; i++)
            {
                sourceRectangles[i] = new Rectangle(topLefts[i].X, topLefts[i].Y, width, height);
            }
            frameNo = 0;
            currentWait = waitTime;
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width * 2, height * 2);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangles[frameNo], Color.White);
        }

        public void Update()
        {
            if (currentWait == 1)
            {
                currentWait = waitTime;
                int maxFrames = sourceRectangles.Length;
                frameNo = (frameNo + 1) % maxFrames;
            }
            else
            {
                currentWait--;
            }
        }
    }
}
