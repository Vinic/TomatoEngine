using System.Collections.Generic;
using System.Media;

namespace TomatoEngine
{
    class AudioObject
    {
        public string Location = "";
        private SoundPlayer _tomatoPlayer;
        public AudioObject(string location, bool playAudio)
        {
            Location = location;
            _tomatoPlayer = new SoundPlayer(@location);
            if (playAudio)
            {
                _tomatoPlayer.Play();
            }
            else
            {
                _tomatoPlayer.Stop();
            }
            
        }
        public void Play()
        {
            _tomatoPlayer.Play();
        }
    }



    static class SoundPool
    {
        private static List<AudioObject> _AudioObjectList = new List<AudioObject>();
        static void PlaySound(string name, int volume)
        {
            string location = ResourceManager.GetSoundLocationByName(name);
            AudioObject ex = null;
            foreach(AudioObject x in _AudioObjectList)
            {
                if (x.Location.Equals(location))
                {
                    ex = x;
                }
            }
            if (ex == null)
            {
                _AudioObjectList.Add(new AudioObject(location, true));
            }
            else
            {
                ex.Play();
            }
        }
    }
}
