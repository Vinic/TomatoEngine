using SharpGL;
using System.Drawing;

namespace TomatoEngine
{
    public class ImageTexture
    {
        public string Name = "";
        public uint Id = 0;
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
            Id = _texture.TextureName;
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, _texture.TextureName);
        }

        public void UseTexure(OpenGL gl)
        {
            _texture.Bind(gl);
            gl.ActiveTexture(_texture.TextureName);
        }
    }
}
