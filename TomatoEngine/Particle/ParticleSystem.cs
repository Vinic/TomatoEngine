using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Particle
{
    public class ParticleSystem
    {
        private int _maxlifetime = 100, _minlifetime = 100;
        private ImageTexture _texture;
        private PointFloat _pos = new PointFloat(0,0);
        private PointFloat _size = new PointFloat(0.2f, 0.2f);
        private float _rot = 0f, _spread = 0.3f, _speedSpread = 0.0f;
        private Random _random = new Random();
        private byte[] _color = new byte[3];
        private bool _ransomSpeed = false;
        public ParticleSystem(string texture)
        {
            _texture = ResourceManager.GetTexture(texture);
            _color[0] = 255;
            _color[1] = 255;
            _color[2] = 255;
        }
        public void SetTexture(ImageTexture texture)
        {
            _texture = texture;
        }
        public void SetLifeTime(int lifetime)
        {
            _maxlifetime = lifetime;
            _minlifetime = lifetime;
        }
        public void SetLifeTime(int minlifetime, int maxlifetime)
        {
            if(minlifetime > maxlifetime){
                minlifetime = maxlifetime;
            }
            _maxlifetime = maxlifetime;
            _minlifetime = minlifetime;
        }

        public void SetRot(float rot)
        {
            _rot = rot;
        }
        public void SetRotAdd(float add)
        {
            _rot = _rot + add;
        }
        public void SetPos(float x, float y)
        {
            _pos.x = x;
            _pos.y = y;
        }
        public void SetPos(PointFloat pos)
        {
            _pos.x = pos.x;
            _pos.y = pos.y;
        }
        public PointFloat GetPosition()
        {
            return _pos;
        }
        public void SetPosAdd(PointFloat add)
        {
            _pos.x = _pos.x + add.x;
            _pos.y = _pos.y + add.y;
        }
        public void SetPosAdd(float x, float y)
        {
            _pos.x = _pos.x + x;
            _pos.y = _pos.y + y;
        }
        public void SetSize(PointFloat size)
        {
            _size.x = size.x;
            _size.y = size.y;
        }
        public void SetSize(float sx, float sy)
        {
            _size.x = sx;
            _size.y = sy;
        }
        public void SetSpread(float spread)
        {
            _spread = spread;
        }
        public void SetRandomSpeed(bool on)
        {
            _ransomSpeed = on;
        }
        public void SetColor(byte[] color)
        {
            _color[0] = color[0];
            _color[1] = color[1];
            _color[2] = color[2];
        }
        public void SetColor(byte r, byte g, byte b)
        {
            _color[0] = r;
            _color[1] = g;
            _color[2] = b;
        }

        public void SetRandomSpeed(bool on, float amount)
        {
            _ransomSpeed = on;
            _speedSpread = amount;
        }
        private float RandomSpread(float input, float spread)
        {
            return input - ( spread / 2f ) + ( (float)_random.Next((int)( spread * 10000000 )) ) / 10000000;
        }

        public void Blow(float speed)
        {
            if(_ransomSpeed){
                speed = RandomSpread(speed, _speedSpread);
            }
            Particle p = new Particle(_texture, _pos.x, _pos.y, RandomSpread(_rot, _spread), speed, _random.Next(_minlifetime, _maxlifetime), _size.x, _size.y, _color);
            TomatoMainEngine.AddGameObject(p);
        }
        public void Blow(float speed, int amount)
        {
            for ( int a = 0; a < amount; a++ )
            {
                if ( _ransomSpeed )
                {
                    speed = RandomSpread(speed, _speedSpread);
                }
                Particle p = new Particle(_texture, _pos.x, _pos.y, RandomSpread(_rot, _spread), speed, _random.Next(_minlifetime, _maxlifetime), _size.x, _size.y, _color);
                TomatoMainEngine.AddGameObject(p);
            }
        }
    }
}
