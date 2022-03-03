using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.Entities.Items
{
    public class BowItem : IItem
    {
        public BowItem() { }
        public void UseOn(ILink player) { }
        public bool Equals(IItem other)
        {
            return other is BowItem;
        }
    }
}
