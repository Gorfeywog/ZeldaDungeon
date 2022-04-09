using System;
using ZeldaDungeon;
using ZeldaDungeon.Commands;

namespace ZeldaDungeon.Commands
{
    public class OpenMenu : ICommand
    {
        private Game1 g;
        public OpenMenu(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            g.PauseMenu();
        }
    }
}
