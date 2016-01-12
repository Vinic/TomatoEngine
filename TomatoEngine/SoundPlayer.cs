using System.Collections.Generic;
using System.Media;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace TomatoEngine
{
    class AudioObject
    {
        public string sLocation = "";
        private SoundEffect _tomatoPlayer;
        //private SoundEffect _effectPlayer;
        public AudioObject(string location, bool playAudio)
        {
            if(location != ""){
                sLocation = location;
                _tomatoPlayer = SoundEffect.FromStream(File.OpenRead(location));
                if (playAudio)
                {
                    _tomatoPlayer.Play();
                }
            }
        }
        public bool IsPlaying()
        {
            return false;
        }
        public void Play()
        {
            _tomatoPlayer.Play();
        }
    }



    public static class SoundPool
    {
        private static List<AudioObject> _AudioObjectList = new List<AudioObject>();
        private static MediaLibrary _playlist = new MediaLibrary();
        private static VisualizationData _visualizationData = new VisualizationData();
        static SoundPool()
        {
            FrameworkDispatcher.Update();
            MediaPlayer.IsVisualizationEnabled = true;
            
        }

        public static void PlaySound(string name)
        {
            

            string location = ResourceManager.GetSoundLocationByName(name);
            AudioObject ex = null;
            foreach(AudioObject x in _AudioObjectList)
            {
                if (x.sLocation.Equals(location))
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
        public static void PlaySoundAdd(string name)
        {


            string location = ResourceManager.GetSoundLocationByName(name);
            var a = new AudioObject(location, true);

        }

        public static void PlayBackgroundMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(_playlist.Songs[0]);
            
        }
        public static void PlayBackgroundMusic(string name)
        {
            string locationString = ResourceManager.GetSoundLocationByName(name);
            Uri location = new Uri(locationString);
            MediaPlayer.Stop();
            MediaPlayer.Play(Song.FromUri(name, location));

        }
        public static int GetBackgroundMusicPos()
        {
            return (int)MediaPlayer.PlayPosition.TotalMilliseconds;
        }
        public static VisualizationData GetBackgroundVisData()
        {

            MediaPlayer.GetVisualizationData(_visualizationData);
            return _visualizationData;
        }

    }
}
