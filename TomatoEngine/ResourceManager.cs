using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TomatoEngine
{
    public class ResourceManager
    {
        private string _imageFolder = "";
        private string _audioFolder = "";
        private ImageTexture[] _textures;
        private 
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
                    
                }
                
            }
        }

        public ImageTexture GetTexture(string name)
        {
            foreach(ImageTexture tex in _textures){
                if(tex != null && tex.Name == name){
                    return tex;
                }
            }
            return null;
        }



    }
}
