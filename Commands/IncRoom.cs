using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class IncRoom : ICommand
    {
        private Game1 g;
        public IncRoom(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            int newIndex = (g.CurrentRoomIndex + 1) % g.RoomCount;
            g.TeleportToRoom(newIndex);
        }
    }
}
