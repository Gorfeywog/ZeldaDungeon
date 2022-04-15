using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Commands
{
    public class SpawnPickup : ICommand
    {
        private Room r;
        private IPickup pickup;
        public SpawnPickup(Room r, IPickup pickup)
        {
            this.r = r;
            this.pickup = pickup;
        }

        public void Execute() => r.RegisterEntity(pickup);
    }
}
