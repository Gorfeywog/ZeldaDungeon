﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Sprites
{
    public interface ISprite
    {
        public void Draw(Rectangle pos, SpriteBatch _spriteBatch); // maybe change the SpriteBatch to be a parameter? unsure if it should be that or a private variable. It is decided - Josh
        public void Update();
    }
}
