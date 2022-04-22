using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;


namespace ZeldaDungeon.Commands
{
    public class DoNothing : ICommand
    {
        public DoNothing()
        {
        }

        public void Execute()
        {
            // used to ensure keys 1 and 2 stop being viable commands after the player leaves the main menu.
        }
    }
}
