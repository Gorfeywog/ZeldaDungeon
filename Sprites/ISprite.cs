using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    public interface ISprite
    {
        public void Draw(); // maybe change the SpriteBatch to be a parameter? unsure if it should be that or a private variable
        public void Update();
    }
}
