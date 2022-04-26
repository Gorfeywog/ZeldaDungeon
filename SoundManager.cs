using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZeldaDungeon
{
    class SoundManager
    {
        private static SoundManager instance = new SoundManager();

        private Dictionary<String, SoundEffect> Sounds;
        private Dictionary<String, Song> Music;

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }

        private SoundManager() {
        }

        public void PlaySound(String sound)
        {
            Sounds[sound].Play();
        }

        public void PlayMusic(String song, bool isLooped)
        {
            MediaPlayer.Play(Music[song]);
            MediaPlayer.IsRepeating = isLooped;

        }

        public void LoadAllAudio(ContentManager content)
        {
            Sounds = new Dictionary<String, SoundEffect>();
            Music = new Dictionary<String, Song>();

            String root = "Content\\";
            String parentEffect = "SoundEffects\\";
            String fileExtension = ".xnb";
            String[] effects = Directory.GetFiles(root + parentEffect);

            for (int i = 0; i < effects.Length; i++)
            {
                String newName = effects[i].Substring(root.Length + parentEffect.Length);
                newName = newName.Substring(0, newName.Length - fileExtension.Length);

                Sounds.Add(newName, content.Load<SoundEffect>(parentEffect + newName));
            }


            String parentSong = "Music\\";
            String[] songs = Directory.GetFiles(root + parentSong);
 
            for (int i = 0; i < songs.Length; i++)
            {
                String newName = songs[i].Substring(root.Length + parentSong.Length);
                newName = newName.Substring(0, newName.Length - fileExtension.Length);

                if (!Music.ContainsKey(newName))
                {
                    Music.Add(newName, content.Load<Song>(parentSong + newName));
                }
            }


        }
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

    }
}
