using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SpaceGame
{
    public class SpaceShip : RenderObject
    {
        private float _rotV;
        private PointFloat _vel = new PointFloat(0,0);
        private Particle.ParticleSystem engineFirePar = new Particle.ParticleSystem("engineFire");
        public SpaceShip() : base()
        {
            SetTexture(ResourceManager.GetTexture("spaceShip"));
            engineFirePar.SetLifeTime(10, 50);
            engineFirePar.SetRandomSpeed(true, 0.1f);
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            _vel.x = _vel.x - ( _vel.x / 10 );
            _vel.y = _vel.y - ( _vel.y / 10 );
            _rotV = _rotV - ( _rotV / 10 );
            if ( ControlKeys.IsKeyDown("s") )
            {
                MoveBackward();
            }
            if(ControlKeys.IsKeyDown("w")){
                MoveForward();
            }

            if ( ControlKeys.IsKeyDown("d") )
            {
                _rotV = _rotV + (-0.1f - _rotV);
            }
            if ( ControlKeys.IsKeyDown("a") )
            {
                _rotV = _rotV + ( 0.1f - _rotV );
            }

            SetPosAdd(_vel);
            SetRotAdd(_rotV);
            engineFirePar.SetPos(GetPosition());
            engineFirePar.SetRot(GetRotation() + 3.14f);
        }
        private void MoveForward()
        {
            float xAdd = (float)Math.Sin((double)-GetRotation());
            float yAdd = (float)Math.Cos((double)-GetRotation());
            _vel.x = _vel.x + ( xAdd - _vel.x )/30;
            _vel.y = _vel.y + ( yAdd - _vel.y )/30;
            engineFirePar.Blow(0.4f, 10);
            
        }
        private void MoveBackward()
        {
            float xAdd = (float)Math.Sin((double)-GetRotation());
            float yAdd = (float)Math.Cos((double)-GetRotation());
            _vel.x = _vel.x + ( -xAdd - _vel.x )/30;
            _vel.y = _vel.y + ( -yAdd - _vel.y )/30;

        }

    }
}
