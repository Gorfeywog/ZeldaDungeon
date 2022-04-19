using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class DecItemSelect : ICommand
    {
        private Game1 g;
        public DecItemSelect(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            g.Select.DecSelection();
        }
    }
}
