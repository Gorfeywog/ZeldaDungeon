using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    public interface ISprite
    {
        public void Draw(SpriteBatch spriteBatch, Point topLeft); // maybe change the SpriteBatch to be a parameter? unsure if it should be that or a private variable
        public void Update();
    }
}
