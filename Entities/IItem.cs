using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IItem : IDrawable {
        public Point CurrentPoint { get; set; } // possibly unnecessary now?
        public void UseOn(ILink player);
    }
}
