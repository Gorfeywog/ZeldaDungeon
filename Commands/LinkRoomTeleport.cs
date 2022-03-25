using System;
using System.Collections.Generic;
using System.Text;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class LinkRoomTeleport : ICommand
    {
        private Game1 g;
        private Direction dir;
        public LinkRoomTeleport(Game1 g, Direction dir)
        {
            this.g = g;
            this.dir = dir;
        }

        public void Execute()
        {
            int newIndex = g.DirToRoomIndex(d);
            g.TeleportToRoom(newIndex);
        }
    }
}
