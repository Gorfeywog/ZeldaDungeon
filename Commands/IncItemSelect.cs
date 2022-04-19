using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class IncItemSelect : ICommand
    {
        private Game1 g;
        public IncItemSelect(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            g.Select.IncSelection();
        }
    }
}
