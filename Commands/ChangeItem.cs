using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class ChangeItem : ICommand
    {
        private Game1 g;
        private bool reverse;
        public ChangeItem(Game1 g, bool reverse)
        {
            this.g = g;
            this.reverse = reverse;
        }

        public void Execute()
        {
            int currentIndex = g.CurrentItemIndex;
            int newIndex;
            if (reverse)
            {
                if (currentIndex == 0)
                {
                    newIndex = g.ItemCount - 1;
                }
                else
                {
                    newIndex = currentIndex - 1;
                }
            }
            else
            {
                newIndex = (currentIndex + 1) % g.ItemCount;
            }
            g.CurrentItemIndex = newIndex;
        }
    }
}
