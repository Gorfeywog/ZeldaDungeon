using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities.Link;
using ZeldaDungeon.InventoryItems;
using ZeldaDungeon.Rooms;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IEntity
    {
        public Point Center { get; }
        public Direction Direction { get; }
        public Room CurrentRoom { get; }
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
        public bool SwordIsThrown { get; set; }
        public CollisionHeight Height { get; }
        public void ChangeDirection(Direction nextDirection);
        public void StartWalking();
        public void StopWalking();
        public void ChangeRoom(Room r);
        public void TakeDamage(Direction direction, int amt = 1);
        public void Knockback(Direction direction);
        public void Heal(int amt = 1);
        public void Heal();
        public void UseHeartContainer();
        public void PickUp(IPickup pickup);
        public bool CanPickUp();
        public LinkInventory GetInv();
        public void AddItem(IItem item, int quantity = 1);
        public void UseItem(IItem item);
        public bool HasItem(IItem item);
        public void Attack();
    }
}
