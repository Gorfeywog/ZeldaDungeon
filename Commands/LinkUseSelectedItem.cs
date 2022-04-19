using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;
using ZeldaDungeon.InventoryItems;

namespace ZeldaDungeon.Commands
{
    public class LinkUseSelectedItem : ICommand
    {
        private Game1 g;
        public LinkUseSelectedItem(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            var selected = g.Select.SelectedItem();
            if (selected == null) { return; }
            g.Player.UseItem(selected);
        }
    }
}
