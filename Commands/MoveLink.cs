using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class MoveLink : ICommand
    {
        private Game1 g;
        private LinkStateMachine.LinkDirection dir;
        public MoveLink(Game1 g, LinkStateMachine.LinkDirection dir)
        {
            this.g = g;
            this.dir = dir;
        }

        public void Execute() => g.Player.ChangeDirection(dir);
    }
}
