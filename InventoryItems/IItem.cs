using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.InventoryItems
{
    // these are inventory-type items, not items on the ground
    // we need functioning equality because of the dict used in LinkInventory
    public interface IItem : IEquatable<IItem>
    {
        public void UseOn(ILink player);
        public bool CanUseOn(ILink player);
        public bool Consumable { get; }
        public bool Selectable { get; }
    }
}
