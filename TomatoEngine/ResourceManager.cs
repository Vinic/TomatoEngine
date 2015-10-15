using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using SharpGL;

namespace TomatoEngine
{
    public class ResourceManager
    {
        private string _imageFolder = "";
        private string _audioFolder = "";
        private static  ImageTexture[] _textures;
        private static AudioLocation[] _sounds;
        public ResourceManager() {
            _imageFolder = Path.Combine(Path.GetDirectoryName( Application.ExecutablePath), @"Resources\Image");
            _audioFolder = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Resources\Audio");
            string[] imageLocations = Directory.GetFiles(_imageFolder);
            _textures = new ImageTexture[imageLocations.Length];
            for(int i=0;i<imageLocations.Length;i++){
                string imageLocation = imageLocations[i];
                try
                {
                    Bitmap bitmap = (Bitmap)Image.FromFile(imageLocation);
                    string name = Path.GetFileNameWithoutExtension(imageLocation);
                    var texture = new ImageTexture(bitmap, name);
                    _textures[i] = texture;
                }
                catch(Exception err){
                    System.Console.Out.WriteLine(err.Message);
                }
                
            }
            string[] audioLocations = Directory.GetFiles(_audioFolder);
            _sounds = new AudioLocation[audioLocations.Length];
            for(int i=0;i<audioLocations.Length;i++){
                string location = audioLocations[i];
                string name = Path.GetFileNameWithoutExtension(location);
                AudioLocation sound = new AudioLocation();
                sound.Name = name;
                sound.Location = location;
                _sounds[i] = sound;
            }
        }

        public void InitTextures(OpenGL gl){
            foreach(ImageTexture tex in _textures){
                tex.InitTexture(gl);
            }
        }

        public static ImageTexture GetTexture(string name)
        {
            foreach(ImageTexture tex in _textures){
                if(tex != null && tex.Name == name){
                    return tex;
                }
            }
            return null;
        }

        public static string GetSoundLocationByName(string name)
        {
            foreach(AudioLocation l in _sounds){
                if(l.Name == name){
                    return l.Location;
                }
            }
            return "";
        }

    }
    class AudioLocation{
        public string Location{get;set;}
        public string Name{get;set;}
    }
}
