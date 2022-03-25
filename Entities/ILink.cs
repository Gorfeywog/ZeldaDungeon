﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IEntity
    {
        public Point Center { get; }
        public Direction Direction { get; }
        public Room CurrentRoom { get; }

        public CollisionHeight Height { get; }
        public void ChangeDirection(Direction nextDirection);
        public void StartWalking();
        public void StopWalking();
        public void ChangeRoom(Room r);
        public void TakeDamage();
        public void PickUp(IPickup pickup);
        public bool CanPickUp();
        public void AddItem(IItem item);
        public void UseItem(IItem item);
        public bool HasItem(IItem item);
        public void Attack();
    }
}
