using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IPickup : IEntity {
        public void PickUp(ILink player);
    }
}
