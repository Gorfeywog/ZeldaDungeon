using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.LinkSprites
{
    class UIRightLink : ISprite
    {
        private Texture2D spritesheet;
        private static readonly int width = 16;
        private static readonly int height = 16;
        private Rectangle sourceRectangle;
        public UIRightLink(Texture2D spritesheet, Point topLeft)
        {
            this.spritesheet = spritesheet;
            sourceRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch, Point topLeft)
        {
            Rectangle destinationRectangle = new Rectangle(topLeft.X, topLeft.Y, width, height);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update() { } // no animation, no update
    }
}
