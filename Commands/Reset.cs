using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class Reset : ICommand
    {
        private Game1 g;
        public Reset(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            g.SetupLists();
            g.SetupPlayer();
        }
    }
}
