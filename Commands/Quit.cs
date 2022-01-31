﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class Quit : ICommand
    {
        private Game1 g;
        public Quit(Game1 g)
        {
            this.g = g;
        }

        public void Execute() => g.Exit();
    }
}
