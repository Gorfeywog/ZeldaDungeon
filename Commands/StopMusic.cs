using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class StopMusic : ICommand
    {
        private Game1 g;
        public StopMusic(Game1 g)
        {
            this.g = g;
        }

        public void Execute()
        {
            MediaPlayer.Stop();
        }
    }
}
