using SharpGL;
using System.Drawing;

namespace TomatoEngine
{
    public class ImageTexture
    {
        public string Name = "";
        private Bitmap _image;
        private SharpGL.SceneGraph.Assets.Texture _texture;
        public ImageTexture(Bitmap image, string name)
        {
            _image = image;
            Name = name;
        }
        public void InitTexture(OpenGL gl)
        {
            _texture = new SharpGL.SceneGraph.Assets.Texture();
            _texture.Create(gl, _image);
        }
        public void BindToGL(OpenGL gl){
            _texture.Bind(gl);
        }
    }
}
