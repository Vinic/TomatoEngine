using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
