using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class ChangeBlock : ICommand
    {
        private Game1 g;
        private bool reverse;
        public ChangeBlock(Game1 g, bool reverse)
        {
            this.g = g;
            this.reverse = reverse;
        }

        public void Execute()
        {
            int currentIndex = g.CurrentBlockIndex;
            int newIndex;
            if (reverse)
            {
                if (currentIndex == 0)
                {
                    newIndex = g.BlockCount - 1;
                }
                else
                {
                    newIndex = currentIndex - 1;
                }
            }
            else
            {
                newIndex = (currentIndex + 1) % g.BlockCount;
            }
            g.CurrentBlockIndex = newIndex;
        }
    }
}
