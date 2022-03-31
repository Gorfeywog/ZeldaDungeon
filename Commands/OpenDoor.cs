using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class OpenDoor : ICommand
    {
        private Game1 g;
        private Direction dir;
        public OpenDoor(Game1 g, Direction dir)
        {
            this.g = g;
            this.dir = dir;
        }

        public void Execute()
        {
            g.OpenRoomDoor(dir);
        }
    }
}
