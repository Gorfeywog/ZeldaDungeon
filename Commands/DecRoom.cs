using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class DecRoom : ICommand
    {
        private Game1 g;
        public DecRoom(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            int newIndex = g.CurrentRoomIndex - 1;
            if (newIndex == -1)
            {
                newIndex = g.RoomCount - 1;
            }
            g.TeleportToRoom(newIndex);
        }
    }
}
