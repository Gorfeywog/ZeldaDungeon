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

        public void Execute()
        {
            Point size = pickup.CurrentLoc.Size;
            Point place = r.LinkDoorSpawn(Direction.Right); // right door area is valid in most rooms
            pickup.CurrentLoc = new Rectangle(place, size);
            r.RegisterEntity(pickup);
        }
    }
}
