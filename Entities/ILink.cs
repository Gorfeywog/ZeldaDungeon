using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Entities
{
    public interface ILink : IDrawable
    {
        public void ChangeDirection(LinkStateMachine.LinkDirection nextDirection); // maybe move directions out of LinkStateMachine; one enum for all the directions seems sensible
        public void TakeDamage();
        public void UseItem();
        public void Attack();
    }
}
