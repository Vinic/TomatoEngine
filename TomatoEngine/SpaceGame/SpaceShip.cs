using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SpaceGame
{
    public class SpaceShip : RenderObject
    {
        private Particle.ParticleSystem engineFirePar = new Particle.ParticleSystem("engineFire");
        private PointFloat _particleOffset1 = new PointFloat(0, -2);
        public SpaceShip() : base()
        {
            Type = "SpaceGame.SpaceShip";
            SetTexture(ResourceManager.GetTexture("spaceShip"));
            engineFirePar.SetLifeTime(3, 30);
            engineFirePar.SetRandomSpeed(true, 0.1f);
            EnablePhysics(true);
            SetStaticObject(false);
            EnableAirResistance(true);
            SetPhysSize(0.5f);
            //Lights.Light _light = new Lights.Light(255, 0, 255, EntityId);
            //TomatoMainEngine.AddGameObject(_light);
        }

        public override void Update(GameSettings settings)
        {
            
            PointFloat vel = GetVelocity();
            float rotV = GetRotationVelocity();
            rotV = rotV - ( rotV / 50 );
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
                rotV = rotV + (0.05f - rotV);
            }
            if ( ControlKeys.IsKeyDown("a") )
            {
                rotV = rotV + ( -0.05f - rotV );
            }
            if ( ControlKeys.IsKeyDown("e") )
            {

                engineFirePar.SetLifeTime(100,200);
                engineFirePar.SetSpread((float)Math.PI * 2);
                engineFirePar.Blow(1f, 100);
                engineFirePar.SetSpread(0.3f);
                engineFirePar.SetLifeTime(3,30);
            }
            SetVelocity(vel);
            SetRotationVelocity(rotV);
            base.Update(settings);
            engineFirePar.SetPos(Helpers.PhysicsAndPositions.OffsetPosition(GetPosition(), 1.0f, GetRotation() + (float)Math.PI * 1.5f));
            engineFirePar.SetRot(GetRotation() + 3.14f);
            CamController.SetPos(GetPosition());
        }
        private PointFloat MoveForward(PointFloat vel)
        {
            float xAdd = (float)Math.Sin((double)GetRotation());
            float yAdd = (float)Math.Cos((double)GetRotation());
            vel.x = vel.x + ( xAdd - vel.x )/100;
            vel.y = vel.y + ( yAdd - vel.y )/100;
            engineFirePar.Blow(0.4f, 2);
            return vel;
        }
        private PointFloat MoveBackward(PointFloat vel)
        {
            float xAdd = (float)Math.Sin((double)GetRotation());
            float yAdd = (float)Math.Cos((double)GetRotation());
            vel.x = vel.x + ( -xAdd - vel.x )/100;
            vel.y = vel.y + ( -yAdd - vel.y )/100;
            return vel;
        }

    }
}
