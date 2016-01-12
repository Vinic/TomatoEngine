using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.TimedFireworks
{
    public class FireWork : RenderObject
    {
        public bool Explode = true;
        private Random r = TomatoMainEngine.GameRandom;
        private byte[] _color;
        private int time = 100;
        private Particle.ParticleSystem _explosion = new Particle.ParticleSystem("light");
        private Particle.ParticleSystem _smoke = new Particle.ParticleSystem("light");
        public FireWork(PointFloat pos, byte[] color) : base("FireWork")
        {
            SetTexture("bullet");
            float velx = (float)( r.NextDouble() -0.5);
            float vely = (float)( r.NextDouble() ) + 3.0f;
            SetVelocity(velx / 10f, vely / 6.0f);
            EnablePhysics(false);
            SetStaticObject(false);
            SetSize(0.2f,0.2f);
            SetPos(pos);
            _explosion.SetRandomSpeed(true,0.01f);
            _explosion.SetSpread(Helpers.PhysicsAndPositions.PI * 2);
            _explosion.SetLifeTime(30,100);
            _explosion.SetPhysics(false);
            _color = color;
            _smoke.SetSize(0.05f,0.05f);
            _smoke.SetPhysics(false);
            _smoke.SetSpread(1f);
            SetRotationVelocity( (float)r.Next(20, 60) / 100f );
            time = r.Next(50,100);
            SoundPool.PlaySound("lauch");
        }
        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            time--;
            SetVelocityAdd(0f,-0.007f);
            _smoke.SetPos(GetPosition());
            _smoke.SetRot(GetRotation());
            _smoke.Blow(0.01f,1,false);
            if(time - 1 < 0){
                SoundPool.PlaySound("explode");
                SetTexture("light");
                SetSize(1.0f,1.0f);
            }
            if(time < 0){
                if(Explode){
                    _explosion.SetColor(_color);
                    _explosion.SetPos(GetPosition());
                    _explosion.Blow(0.1f, 40, false);
                }
                TomatoMainEngine.RemoveRenderObject(EntityId);
            }
        }
    }
}
