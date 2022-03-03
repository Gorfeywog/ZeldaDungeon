using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IPickup : IEntity {
        public void PickUp(ILink player);
        // maybe add a bool for whether Link holds it above his head on pickup?
        // like, he should clearly hold up the candle, but probably not each heart or rupee
    }
}
