using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class LinkUsePickup : ICommand
    {
        private Game1 g;
        private IPickup item;
        public LinkUsePickup(Game1 g, IPickup item)
        {
            this.g = g;
            this.item = item;
        }

        public void Execute() => g.Player.UsePickup(item);
    }
}
