using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.SpaceGame
{
    class Asteroid : RenderObject
    {
        private float _rotV;
        private PointFloat _vel = new PointFloat(0, 0);
        public Asteroid() : base()
        {
            Type = "SpaceGame.Asteroid";
            SetTexture(ResourceManager.GetTexture("asteroid"));
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            _vel.x = _vel.x - (_vel.x / 10);
            _vel.y = _vel.y - (_vel.y / 10);
            _rotV = _rotV - (_rotV / 10);

            SetPosAdd(_vel);
            SetRotAdd(_rotV);
        }
    }
}
