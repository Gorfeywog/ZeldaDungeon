using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEnemy : IEntity
    {
        CollisionHandler collision { get; set; }

        public void UpdateList(EntityList roomEntities);

        public void Attack();

        public void Move();

        public void TakeDamage();
    }
}
