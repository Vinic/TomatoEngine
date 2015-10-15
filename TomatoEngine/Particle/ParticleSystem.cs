using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Particle
{
    public class ParticleSystem
    {
        private int _lifetime = 100;
        private ImageTexture _texture;
        private PointFloat _pos = new PointFloat(0,0);
        private PointFloat _size = new PointFloat(0.2f, 0.2f);
        private float _rot = 0f;
        public ParticleSystem(string texture)
        {
            _texture = ResourceManager.GetTexture(texture);
        }

        public void SetLifeTime(int lifetime)
        {
            _lifetime = lifetime;
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
        public void Blow(float speed)
        {
            Particle p = new Particle(_texture,_pos.x,_pos.y,_rot,speed,_lifetime, _size.x, _size.y);
            TomatoMainEngine.AddGameObject(p);
        }
    }
}
