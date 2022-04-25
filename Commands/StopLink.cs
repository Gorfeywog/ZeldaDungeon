using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class StopLink : ICommand
    {
        private Game1 g;
        private Direction dir;
        public StopLink(Game1 g, Direction d)
        {
            this.g = g;
            this.dir = d;
        }

        public void Execute()
        {
            if (g.Player.Direction == dir)
            {
                g.Player.StopWalking();
            }
        }
    }
}
