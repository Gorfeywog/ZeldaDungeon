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
        private Color color;
        private Rectangle sourceRectangle;
        public StaticLinkSprite(Texture2D spritesheet, int width, int height, Point topLeft)
        {
            InitializeConstructor(spritesheet, width, height, topLeft, Color.White);
        }
        public StaticLinkSprite(Texture2D spritesheet, int width, int height, Point topLeft, Color color)
        {
            InitializeConstructor(spritesheet, width, height, topLeft, color);
        }

        private void InitializeConstructor(Texture2D spritesheet, int width, int height, Point topLeft, Color color)
        {
            this.spritesheet = spritesheet;
            this.width = width;
            this.height = height;
            this.color = color;
            sourceRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangle, color);
        }

        public void Update() { } // no animation, no update
    }
}
