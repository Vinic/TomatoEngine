﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SpaceGame
{
    class Asteroid : GameObject
    {
        private float _rotV;
        private PointFloat _vel = new PointFloat(0, 0);
        public Asteroid() : base()
        {
            Type = "SpaceGame.Asteroid";
            SetTexture(ResourceManager.GetTexture("asteroid"));
            EnablePhysics(true);
            EnableAirResistance(false);
            SetStaticObject(false);
            SetSize(2.0f,2.0f);
            SetPhysSize(1.5f);
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            _vel.x = _vel.x - (_vel.x / 10);
            _vel.y = _vel.y - (_vel.y / 10);
            _rotV = _rotV - (_rotV / 10);

            SetPosAdd(_vel);
            SetRotationAdd(_rotV);
        }
    }
}
