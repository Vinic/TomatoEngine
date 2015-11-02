using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.ParticleBallDemo
{
    public class FlammingBall : RenderObject
    {
        private Particle.ParticleSystem _fire = new Particle.ParticleSystem("light");
        private PointFloat _dir = new PointFloat(0,0);
        public FlammingBall() : base(){
            Type = "Fball";
            EnableAirResistance(false);
            EnablePhysics(true);
            _fire.SetSpread(Helpers.PhysicsAndPositions.PI * 20.0f);
            _fire.SetLifeTime(20, 70);
            SetTexture(ResourceManager.GetTexture("ball"));
            Z_Index = 10;
            _fire.SetColor(0,200,0);
            SetStaticObject(false);
            SetPhysSize(0.7f);
        }

        public override void Update(GameSettings settings)
        {
            
            _dir.x = 0;
            _dir.y = 0;
            foreach(RenderObject obj in TomatoMainEngine.GameObjects){
                if(obj.Type == Type && obj != this){
                    PointFloat selfPos = GetPosition();
                    PointFloat objPos = obj.GetPosition();
                    float x = selfPos.x - objPos.x;
                    float y = selfPos.y - objPos.y;
                    float h = -(float)Math.Atan2(x, y);
                    _dir = _dir - Helpers.PhysicsAndPositions.GetDirection(h);
                }
            }

            SetVelocityAdd(_dir / 500);
            
            _fire.SetPos(GetPosition());
            _fire.Blow(0.01f,1);
            base.Update(settings);
        }
    }
}
