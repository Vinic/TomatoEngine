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
        private int _timer = 0;
        public FlammingBall() : base(){
            Type = "Fball";
            EnableAirResistance(false);
            EnablePhysics(true);
            _fire.SetSpread(Helpers.PhysicsAndPositions.PI * 20.0f);
            _fire.SetLifeTime(20, 70);
            SetTexture(ResourceManager.GetTexture("ball"));
            Z_Index = 10;
            _fire.SetColor(0,200,0);
            _fire.SetParent(EntityId);
            _fire.SetPhysics(true);
            SetStaticObject(false);
            SetPhysSize(0.7f);
        }

        public override void Update(GameSettings settings)
        {
            
            _dir.x = 0;
            _dir.y = 0;
            PointFloat selfPos = GetPosition();
            foreach(RenderObject obj in TomatoMainEngine.GameObjects){
                if(obj.Type == Type && obj != this){
                    
                    PointFloat objPos = obj.GetPosition();
                    float x = selfPos.x - objPos.x;
                    float y = selfPos.y - objPos.y;
                    float h = -(float)Math.Atan2(x, y);
                    h = h + GetVelocity().GetSpeed() * 2;
                    _dir = _dir - Helpers.PhysicsAndPositions.GetDirection(h);
                }
            }
            float x0 = selfPos.x;
            float y0 = selfPos.y;
            float h0 = -(float)Math.Atan2(x0, y0);
            h0 = h0 + GetVelocity().GetSpeed() * 2;
            _dir = _dir - Helpers.PhysicsAndPositions.GetDirection(h0);
            SetVelocityAdd(_dir / 500);
            
            _fire.SetPos(GetPosition());
            _fire.Blow(0.03f,4);
            if (_timer  < 0)
            {
                SetVelocityAdd(Helpers.PhysicsAndPositions.GetDirection(GetRotation()) / 10.0f);
                _timer = 900;
            }
            _timer--;
            if(ControlKeys.IsKeyDown("e")){
                SetVelocityAdd(Helpers.PhysicsAndPositions.GetDirection(GetRotation()) / 10.0f);
            }
            base.Update(settings);
        }
        public override bool OnColision(RenderObject col, float inpact)
        {
            if(Type == col.Type){
                _fire.Blow(1.0f, 50);
            }
            return true;
        }
    }
}
