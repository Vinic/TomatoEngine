using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Particle
{
    class Particle : RenderObject
    {
        private int _lifeTime = 10;
        private float _speed = 1;
        public Particle(ImageTexture texture, float x, float y, float _rotation, float speed, int lifetime, float sizex, float sizey) : base()
        {
            Type = "Particle.Particle";
            SetSize(sizex,sizey);
            SetTexture(texture);
            _lifeTime = lifetime;
            SetRotation(_rotation);
            SetPos(x,y);
            _speed = speed;
            EnablePhysics(true);
            EnableAirResistance(true);
        }

        public override void Update(GameSettings settings)
        {
            
            float xAdd = (float)Math.Sin((double)-GetRotation());
            float yAdd = (float)Math.Cos((double)-GetRotation());
            SetPosAdd(xAdd * _speed, yAdd * _speed);
            _lifeTime = _lifeTime - 1;
            if(_lifeTime < 0){
                _lifeTime = 0;
                TomatoMainEngine.RemoveRenderObject(EntityId);
            }
            base.Update(settings);
        }
    }
}
