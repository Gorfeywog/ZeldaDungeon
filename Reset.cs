using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class Reset : ICommand
    {
        private Game1 game;
        public Reset(Game1 game) { this.game = game; }
        public void Execute() // TODO - make this work
        {

        }
    }
}
