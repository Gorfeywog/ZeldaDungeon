using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class LinkUseItem : ICommand
    {
        private Game1 g;
        private IItem item;
        public LinkUseItem(Game1 g, IItem item)
        {
            this.g = g;
            this.item = item;
        }

        public void Execute() => g.Player.UseItem(item);
    }
}
