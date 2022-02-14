using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class StopLink : ICommand
    {
        private Game1 g;
        public StopLink(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            g.Player.StopWalking();
        }
    }
}
