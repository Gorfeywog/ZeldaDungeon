using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class StartTower : ICommand
    {
        private Game1 g;
        private Direction dir;
        public StartTower(Game1 g, Direction dir) 
        {
            this.g = g;
            this.dir = dir;
        }
        public void Execute() 
        {
            g.UseRoomDoor(dir);
        }
    }
}
