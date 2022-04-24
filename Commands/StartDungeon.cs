using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;


namespace ZeldaDungeon.Commands
{
    public class StartDungeon : ICommand
    {
        private Game1 g;
        private Direction dir;
        public StartDungeon(Game1 g, Direction dir)
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
