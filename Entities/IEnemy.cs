using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEnemy : IEntity
    {
        public CollisionHeight Height { get; } 

        public bool isFriendly { get; }

        public void Attack();

        public void Move();

        public void TakeDamage();
    }
}
