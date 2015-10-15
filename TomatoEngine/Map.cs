using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public class Map
    {
        private float[] heightMap;
        private ImageTexture _texture = ResourceManager.GetTexture("ground");
        public Map(int length)
        {
            Random r = new Random();
            heightMap = new float[length];
            float t = 0;
            float add = 0.3f;
            float heigth = 1f;
            for (int i = 0; i < length; i++ )
            {
                t = t + add/3;
                float heigthOut = (float)Math.Sin(t) * heigth;
                heightMap[i] = heigthOut;
                if (t > Math.PI*2)
                {
                    t = t - (float)(Math.PI * 2);
                    add = (float)(r.NextDouble() + 0.1);
                    heigth = (float)(r.NextDouble()*2);
                }
            }
        }
        public void Init(OpenGL gl)
        {
            _texture.InitTexture(gl);
        }

        public void Draw(OpenGL gl)
        {
            
            //_texture.BindToGL(gl);
            gl.PopMatrix();
            gl.Begin(OpenGL.GL_QUAD_STRIP);
            for(int i = 0; i < heightMap.Length; i++){

                gl.Vertex(((float)i) / 10, heightMap[i]);
                gl.Vertex(((float)i) / 10, -50);

                
            }
            gl.End();
            gl.PushMatrix();

            
        }
    }
}
