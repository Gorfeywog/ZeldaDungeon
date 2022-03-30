using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    public interface ISprite
    {
        public bool damaged { get; set; }
        public void Draw(SpriteBatch spriteBatch, Rectangle sourceRectangle);
        public void Update();
    }
}
