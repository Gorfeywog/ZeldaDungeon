using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.EnemySprites
{
    class StaticEnemySprite : ISprite
    {
        private Texture2D spritesheet;
        private int width;
        private int height;
        private Rectangle sourceRectangle;
        public StaticEnemySprite(Texture2D spritesheet, int width, int height, Point topLeft)
        {
            this.spritesheet = spritesheet;
            this.width = width;
            this.height = height;
            sourceRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width * 2, height * 2);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update() { } // no animation, no update
    }
}
