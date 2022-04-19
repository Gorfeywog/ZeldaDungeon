using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    public class KeyItem : IItem
    {
        public void UseOn(ILink player) { }
        public bool CanUseOn(ILink player) { return true; }
        public bool Consumable { get => true; }
        public bool Equals(IItem other)
        {
            return other is KeyItem;
        }
        public override int GetHashCode()
        {
            return "key".GetHashCode();
        }
        public bool Selectable => false;
    }
}
