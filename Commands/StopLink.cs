using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class StopLink : ICommand
    {
        private ILink link;
        public StopLink(ILink link)
        {
            this.link = link;
        }

        public void Execute()
        {
            link.StopWalking();
        }
    }
}
