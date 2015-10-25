using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    class PhysEngine
    {
        private RenderObject _ApplyGrav, _Phys;
        public PhysEngine(RenderObject ApplyGrav)
        {
            _ApplyGrav = ApplyGrav;
            //_Phys = Phys;
        }

        private static void Collide(RenderObject Phys, RenderObject Phys2)
        {

        }
    }
}
