using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class MoveLink : ICommand
    {
        private Game1 g;
        private Direction dir;
        public MoveLink(Game1 g, Direction dir)
        {
            this.g = g;
            this.dir = dir;
        }

        public void Execute()
        {
            g.Player.ChangeDirection(dir);
            g.Player.StartWalking();
        }
    }
}
