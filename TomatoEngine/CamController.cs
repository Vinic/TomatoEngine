using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    static class CamController
    {
        public static float X, Y;
        public static double Aspect;

        public static void SetCam(OpenGL gl)
        {
            gl.Perspective(60.0f, Aspect, 0.01, 100.0);
            gl.LookAt(X, Y, 30, X, Y, 0, 0, 1, 0);
        }
    }
}
