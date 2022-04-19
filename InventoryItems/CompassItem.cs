using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class CompassItem : IItem
    {
        public void UseOn(ILink player) { }
        public bool CanUseOn(ILink player) { return false; }
        public bool Consumable { get => false; }
        public bool Equals(IItem other)
        {
            return other is CompassItem;
        }
        public override int GetHashCode()
        {
            return "compass".GetHashCode();
        }
        public bool Selectable => false;
    }
}
