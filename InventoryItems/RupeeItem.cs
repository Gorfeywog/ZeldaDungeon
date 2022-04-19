using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class RupeeItem : IItem
    {
        public void UseOn(ILink player) { }
        public bool CanUseOn(ILink player) { return false; }
        public bool Consumable { get => true; }
        public bool Equals(IItem other)
        {
            return other is RupeeItem;
        }
        public override int GetHashCode()
        {
            return "rupee".GetHashCode();
        }
        public bool Selectable => false;
    }
}
