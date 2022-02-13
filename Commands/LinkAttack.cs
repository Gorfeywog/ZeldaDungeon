using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class LinkAttack : ICommand
    {
        private Game1 g;
        public LinkAttack(Game1 g)
        {
            this.g = g;
        }

        public void Execute() => g.Player.Attack();
    }
}
