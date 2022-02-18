using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEntity
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Update();
    }
}
