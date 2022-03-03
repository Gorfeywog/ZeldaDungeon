using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    // these are inventory-type items, not items on the ground
    // we need functioning equality because of the dict used in LinkInventory
    public interface IItem : IEquatable<IItem> {
        public void UseOn(ILink player);
    }
}
