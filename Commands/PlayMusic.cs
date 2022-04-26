using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class PlayMusic : ICommand
    {
        private Game1 g;
        private String music;
        public PlayMusic(Game1 g, String music)
        {
            this.g = g;
            this.music = music;
        }

        public void Execute()
        {
            SoundManager.Instance.PlayMusic(music, true);
        }
    }
}