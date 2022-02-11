using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IDrawable
    {
        public void Draw();
        public void UpdateSprite(SpriteBatch _spriteBatch);
    }
}
