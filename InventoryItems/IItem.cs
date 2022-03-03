using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    // these are inventory-type items, not items on the ground
    public interface IItem {
        public void UseOn(ILink player);
    }
}
