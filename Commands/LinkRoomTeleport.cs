using System;
using ZeldaDungeon.Entities;

namespace ZeldaDungeon.Commands
{
    public class LinkRoomTeleport : ICommand
    {
        private Game1 g;
        private Direction dir;
        private String sound;
        public LinkRoomTeleport(Game1 g, Direction dir, String sound = "")
        {
            this.g = g;
            this.dir = dir;
            this.sound = sound;
        }

        public void Execute()
        {
            int newIndex = g.DirToRoomIndex(dir);
            g.TeleportToRoom(newIndex);
            if (sound != "")
            {
                SoundManager.Instance.PlaySound(sound);
            }
        }
    }
}
