using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface IEnemy
    {
        public void Attack();

        public void Move(SpriteBatch spriteBatch);

        public void TakeDamage();
    }
}
