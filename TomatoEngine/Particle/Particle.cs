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
        public Particle(ImageTexture texture, float x, float y, float _rotation, float speed, int lifetime, float sizex, float sizey, byte[] color) : base()
        {
            Type = "Particle.Particle";
            SetSize(sizex,sizey);
            SetTexture(texture);
            _lifeTime = lifetime;
            SetRotation(_rotation);
            SetPos(x,y);
            _speed = speed;
            EnablePhysics(true);
            SetStaticObject(false);
            EnableAirResistance(false);
            SetIsParticle(true);
            HasMass(false);
            float xAdd = (float)Math.Sin((double)GetRotation());
            float yAdd = (float)Math.Cos((double)GetRotation());
            SetVelocity(xAdd * _speed, yAdd * _speed);
            SetPhysSize(0.1f);
            SetColor(color);
        }

        public override void Update(GameSettings settings)
        {
            
            
            _lifeTime = _lifeTime - 1;
            if(_lifeTime < 0){
                _lifeTime = 0;
                TomatoMainEngine.RemoveRenderObject(EntityId);
            }
            base.Update(settings);
        }
        public override void Draw(SharpGL.OpenGL gl)
        {
            base.Draw(gl);
        }
        public override void DrawVelocity(SharpGL.OpenGL gl)
        {
            base.DrawVelocity(gl);
        }
        public override void DrawWireFrame(SharpGL.OpenGL gl)
        {
            base.DrawWireFrame(gl);
        }
    }
}
