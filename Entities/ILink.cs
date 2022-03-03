﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IEntity
    {
        public Point Center { get; }
        public Direction Direction { get; }
        public void ChangeDirection(Direction nextDirection);
        public void StartWalking();
        public void StopWalking();
        public void TakeDamage();
        public void PickUp(IPickup pickup);
        public void UseItem(IItem item);
        public void Attack();
    }
}
