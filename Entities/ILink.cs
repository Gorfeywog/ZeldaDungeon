using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IEntity
    {
        public Point Center { get; }
        public Direction Direction { get; }
        public EntityList roomEntities { get; set; }

        public void ChangeDirection(Direction nextDirection);
        public void StartWalking();
        public void StopWalking();
        public void UpdateList(EntityList roomEntities);
        public void TakeDamage();
        public void PickUp(IPickup pickup);
        public bool CanPickUp();
        // could eliminate AddItem and HasItem if we exposed the inventory, but that would maybe
        // be bad for coupling?
        public void AddItem(IItem item);
        public void UseItem(IItem item);
        public bool HasItem(IItem item);
        public void Attack();
    }
}
