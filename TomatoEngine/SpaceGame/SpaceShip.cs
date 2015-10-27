using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SpaceGame
{
    public class SpaceShip : RenderObject
    {
        private Particle.ParticleSystem engineFirePar = new Particle.ParticleSystem("engineFire");
        public SpaceShip() : base()
        {
            Type = "SpaceGame.SpaceShip";
            SetTexture(ResourceManager.GetTexture("spaceShip"));
            engineFirePar.SetLifeTime(3, 10);
            engineFirePar.SetRandomSpeed(true, 0.1f);
            EnablePhysics(true);
            SetStaticObject(false);
        }

        public override void Update(GameSettings settings)
        {
            
            PointFloat vel = GetVelocity();
            float rotV = GetRotationVelocity();
            rotV = rotV - ( rotV / 10 );
            if ( ControlKeys.IsKeyDown("s") )
            {
                vel = MoveBackward(vel);
            }
            if(ControlKeys.IsKeyDown("w")){
                vel = MoveForward(vel);
                //Console.WriteLine(GetPosition().x + " " + GetPosition().y);
            }

            if ( ControlKeys.IsKeyDown("d") )
            {
                rotV = rotV + (-0.1f - rotV);
            }
            if ( ControlKeys.IsKeyDown("a") )
            {
                rotV = rotV + ( 0.1f - rotV );
            }
            SetVelocity(vel);
            SetRotationVelocity(rotV);
            base.Update(settings);
            engineFirePar.SetPos(GetPosition());
            engineFirePar.SetRot(GetRotation() + 3.14f);
        }
        private PointFloat MoveForward(PointFloat vel)
        {
            float xAdd = (float)Math.Sin((double)-GetRotation());
            float yAdd = (float)Math.Cos((double)-GetRotation());
            vel.x = vel.x + ( xAdd - vel.x )/100;
            vel.y = vel.y + ( yAdd - vel.y )/100;
            engineFirePar.Blow(0.4f, 10);
            return vel;
        }
        private PointFloat MoveBackward(PointFloat vel)
        {
            float xAdd = (float)Math.Sin((double)-GetRotation());
            float yAdd = (float)Math.Cos((double)-GetRotation());
            vel.x = vel.x + ( -xAdd - vel.x )/100;
            vel.y = vel.y + ( -yAdd - vel.y )/100;
            return vel;
        }

    }
}
