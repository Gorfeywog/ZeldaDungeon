using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class DamageLink : ICommand
    {
        private Game1 g;
        public DamageLink(Game1 g)
        {
            this.g = g;
        }

        public void Execute() => g.Player.TakeDamage(Entities.Direction.Down);
    }
}
