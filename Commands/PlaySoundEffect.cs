using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaDungeon.Commands
{
    public class PlaySoundEffect : ICommand
    {
        private Game1 g;
        private String sound;
        public PlaySoundEffect(Game1 g, String sound)
        {
            this.g = g;
            this.sound = sound;
        }

        public void Execute()
        {
            SoundManager.Instance.PlaySound(sound);
        }
    }
}