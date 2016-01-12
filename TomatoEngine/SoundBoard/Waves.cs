using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SoundBoard
{
    public class Waves : GameObject
    {
        private float[] _data;
        private float _x;
        private float _height = 1;
        public Waves() : base()
        {

        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            var data = SoundPool.GetBackgroundVisData();
            _data = data.Frequencies.ToArray();
        }
        public void SetHeight(float height)
        {
            _height = height;
        }
        public float GetHeight()
        {
            return _height;
        }

        public override void Draw(SharpGL.OpenGL gl)
        {
            base.Draw(gl);
            if(_data == null){

            }
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            gl.Begin(OpenGL.GL_QUAD_STRIP);
            for (int i = 0; i < _data.Length; i++ )
            {
                gl.Color(_data[i], _data[i], _data[i]);
                _x = ((GetSize().x / 256f) * i) + CamController.X - (GetSize().x / 2f);
                gl.Vertex(_x, GetPosition().y + _data[i] * _height);
                //gl.Vertex(_x, GetPosition().y + _data[i] * _height - 1.0f);
                gl.Vertex(_x, GetPosition().y);
            }
            gl.End();
            gl.Begin(OpenGL.GL_QUAD_STRIP);
            for (int i = 0; i < _data.Length; i++)
            {
                gl.Color(_data[i] - 0.5f, _data[i] - 0.5f, _data[i] - 0.5f); 
                _x = ((GetSize().x / 256f) * i) + CamController.X - (GetSize().x / 2f);
                gl.Vertex(_x, GetPosition().y - _data[i] * _height / 10.0f);
                //gl.Vertex(_x, GetPosition().y + _data[i] * _height - 1.0f);
                gl.Color(_data[i], _data[i], _data[i]);
                gl.Vertex(_x, GetPosition().y);
            }
            gl.End();
        }

    }
}
