using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    static class CamController
    {
        //Set or get the position(not the real position)
        public static float X, Y;
        //Delay for the cam to move around
        public static float Delay = 20.0f;
        //Aspect ratio
        public static double Aspect;
        //RealPosition
        private static float _x, _y;

        public static void Update(){
            if(Delay == 0.0f){
                _x = X;
                _y = Y;
            }
            else
            {
                _x = _x + ((X - _x) / Delay);
                _y = _y + (( Y - _y ) / Delay);
            }
        }

        public static void SetCam(OpenGL gl)
        {
            gl.Perspective(60.0f, Aspect, 0.01, 100.0);
            gl.LookAt(_x, _y, 30, _x, _y, 0, 0, 1, 0);
        }

        public static void SetPos(PointFloat pos)
        {
            X = pos.x;
            Y = pos.y;
        }
    }
}
