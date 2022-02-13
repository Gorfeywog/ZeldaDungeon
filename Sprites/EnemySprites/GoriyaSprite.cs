using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.EnemySprites
{
    class GoriyaSprite : ISprite
    {
        private Texture2D spritesheet;
        private static readonly int width = 14;
        private static readonly int height = 16;
        private Rectangle[] sourceRectangles;
        private int frameNo; // index of current frame in the array
        private static readonly int waitTime = 10; // how many Updates to wait between cycling frame
        private int currentWait;
        public GoriyaSprite(Texture2D spritesheet, Point[] topLefts)
        {
            this.spritesheet = spritesheet;
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
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
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
