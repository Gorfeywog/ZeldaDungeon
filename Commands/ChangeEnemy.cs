using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class ChangeEnemy : ICommand
    {
        private Game1 g;
        private bool reverse;
        public ChangeEnemy(Game1 g, bool reverse)
        {
            this.g = g;
            this.reverse = reverse;
        }

        public void Execute()
        {
            int currentIndex = g.CurrentEnemyIndex;
            int newIndex;
            if (reverse)
            {
                if (currentIndex == 0)
                {
                    newIndex = g.EnemyCount - 1;
                }
                else
                {
                    newIndex = currentIndex - 1;
                }
            }
            else
            {
                newIndex = (currentIndex + 1) % g.EnemyCount;
            }
            g.CurrentEnemyIndex = newIndex;
        }
    }
}
