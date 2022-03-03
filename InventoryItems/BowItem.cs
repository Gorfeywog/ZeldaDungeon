using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.Sprites;

namespace ZeldaDungeon.InventoryItems
{
    public class BowItem : IItem
    {
        public bool Consumable { get => false; }
        public BowItem() { }
        public void UseOn(ILink player) { }
        public bool Equals(IItem other)
        {
            return other is BowItem;
        }
    }
}
