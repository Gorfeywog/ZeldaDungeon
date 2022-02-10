using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites.LinkSprites
{
    class UILeftLink : ISprite
    {
        private Texture2D spritesheet;
        private int width;
        private int height;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        public UILeftLink(Texture2D spritesheet)
        {
            this.spritesheet = spritesheet;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle = new Rectangle(0, 0, width, height);
            spriteBatch.Draw(spritesheet, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
            destinationRectangle = new Rectangle(0, 0, width, height);
        }
    }
}
