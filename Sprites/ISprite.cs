using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    public interface ISprite
    {
        public void Draw(SpriteBatch spriteBatch, Rectangle destRectangle);
        public void Update();
    }
}
