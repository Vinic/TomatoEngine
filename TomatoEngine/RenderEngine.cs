using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine
{
    public class RenderEngine
    {
        private RenderMode _mode = RenderMode.Normal;
        public void RenderObjects(OpenGL gl, RenderObject[] objects)
        {
            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity matrix.
            gl.LoadIdentity();
            CamController.SetCam(gl);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            if(_mode == RenderMode.WireFrame){
                
                foreach(RenderObject obj in objects){
                    gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
                    obj.DrawWireFrame(gl);
                    obj.Draw(gl);
                }
            }
            else 
            {
                foreach ( RenderObject obj in objects )
                {
                    obj.Draw(gl);
                }
            }
        }
        public void SetRenderMode(RenderMode mode)
        {
            _mode = mode;
        }
    }

    public static class RenderLogics
    {
        public static PointFloat[] RectPoint(PointFloat pos,PointFloat size, float t){
            var res = new PointFloat[4];
            res[0] = new PointFloat(pos.x + (float)(Math.Cos(t + DegreeToRad(-45)) * size.x), pos.y + (float)(Math.Sin(t + DegreeToRad(-45)) * size.y));
            res[1] = new PointFloat(pos.x + (float)(Math.Cos(t + DegreeToRad(45)) * size.x), pos.y + (float)(Math.Sin(t + DegreeToRad(45)) * size.y));
            res[2] = new PointFloat(pos.x + (float)(Math.Cos(t + DegreeToRad(135)) * size.x), pos.y + (float)(Math.Sin(t + DegreeToRad(135)) * size.y));
            res[3] = new PointFloat(pos.x + (float)(Math.Cos(t + DegreeToRad(225)) * size.x), pos.y + (float)(Math.Sin(t + DegreeToRad(225)) * size.y));
            return res;
        }
        public static float DegreeToRad(float r)
        {
            return r * (float)Math.PI / 180;
        }

    }
    public class PointFloat
    {
        public float x, y;
        public PointFloat(float xVal,float yVal)
        {
            x = xVal;
            y = yVal;
        }
    }
    public enum RenderMode
    {
        Normal = 1,
        WireFrame = 2,
        NoTextures = 3
    }
}
