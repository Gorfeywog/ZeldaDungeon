using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IDrawable
    {
        // maybe move directions out of LinkStateMachine; one enum for all the directions seems smart
        public void ChangeDirection(Direction nextDirection);
        public void StartWalking();
        public void StopWalking();
        public void TakeDamage();
        public void UseItem();
        public void Attack();
    }
}
