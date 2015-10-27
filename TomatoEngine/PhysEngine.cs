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

        public static void Collide(RenderObject Phys, RenderObject Phys2)
        {
            //Phys.RenderOuterScreen = true;
            //Phys2.RenderOuterScreen = true;
            //Console.WriteLine("Hit!");
        }
    }

    class PhysHandler : RenderObject
    {
        private int[] _colzone;
        
        public PhysHandler()
        {
            _colzone = new int[]
            { };
        }

        
    }
}
