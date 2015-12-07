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
            //clears the screen
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //St matrix mode to PROJECTION so we can move the camera
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity matrix.
            gl.LoadIdentity();
            //Move the camera
            CamController.SetCam(gl);
            //Set matrix mode back to Modelview so we can draw objects
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            if (_mode == RenderMode.WireFrame || _mode == RenderMode.Hitboxes)
            {
                foreach(RenderObject obj in objects){
                    //Draw the object with texture
                    obj.Draw(gl);
                    //Unbind texture so we can draw lines
                    gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
                    if ( _mode == RenderMode.Hitboxes)
                    {
                        obj.DrawVelocity(gl);
                        DrawCircle(gl, obj.GetPosition().x,obj.GetPosition().y, obj.GetPhysSize());
                    }
                    else
                    {
                        obj.DrawWireFrame(gl);
                    }
                }
            }
            else 
            {
                foreach ( RenderObject obj in objects )
                {
                    //Draw the object with texture
                    obj.Draw(gl);
                }
            }
            //Unbind texture
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }
        public void SetRenderMode(RenderMode mode)
        {
            _mode = mode;
        }

        public void DrawCircle(OpenGL gl, float x, float y, float s)
        {

            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1f, 1f, 1f);
            float xp, yp;
            float max = (float)(Math.PI * 2);
            for (float i = 0f; i < max; i = i + 0.5f)
            {
                xp = x + ((float)Math.Cos(i) * s);
                yp = y + ((float)Math.Sin(i) * s);
                gl.Vertex(xp,yp);
                xp = x + ( (float)Math.Cos(i+0.5f) * s );
                yp = y + ( (float)Math.Sin(i+0.5f) * s );
                gl.Vertex(xp, yp);
            }
            gl.Vertex(x, y);
            gl.End();
        }
    }

    public static class RenderLogics
    {
        public static PointFloat[] RectPoint(PointFloat pos,PointFloat size, float t){
            PointFloat[] res = new PointFloat[4];
            //res[0] = new PointFloat(pos.x + (float)(Math.Cos(-t + DegreeToRad(-45)) * size.x), pos.y + (float)(Math.Sin(-t + DegreeToRad(-45)) * size.y));
            //res[1] = new PointFloat(pos.x + (float)( Math.Cos(-t + DegreeToRad(45)) * size.x ), pos.y + (float)( Math.Sin(-t + DegreeToRad(45)) * size.y ));
            //res[2] = new PointFloat(pos.x + (float)(Math.Cos(-t + DegreeToRad(135)) * size.x), pos.y + (float)(Math.Sin(-t + DegreeToRad(135)) * size.y));
            //res[3] = new PointFloat(pos.x + (float)(Math.Cos(-t + DegreeToRad(225)) * size.x), pos.y + (float)(Math.Sin(-t + DegreeToRad(225)) * size.y));
            for ( int i=0; i < res.Length; i++ )
            {
                res[i] = new PointFloat(pos.x + (float)( Math.Cos(-t + DegreeToRad(-45 + 90 * i)) * size.x ) * 1.41421359f, pos.y + (float)( Math.Sin(-t + DegreeToRad(-45 + 90 * i)) * size.y ) * 1.41421359f);
                //res[i] = new PointFloat((pos.x * (float)Math.Cos(t + DegreeToRad(-45 + 90 * i)) - pos.y * (float)Math.Sin(t + DegreeToRad(-45 + 90 * i))) /10.0f,
                //    (pos.x * (float)Math.Cos(t + DegreeToRad(-45 + 90 * i)) + pos.y * (float)Math.Sin(t + DegreeToRad(-45 + 90 * i))) / 10.0f);
            }            
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
        public void Reset()
        {
            x = 0;
            y = 0;
        }
        public bool HasValue()
        {
            if(x != 0 || y != 0){
                return true;
            }
            return false;
        }
        public static PointFloat operator + (PointFloat a, PointFloat b){
            a.x = a.x + b.x;
            a.y = a.y + b.y;
            return a;  
        }
        public static PointFloat operator -(PointFloat a, PointFloat b)
        {
            a.x = a.x - b.x;
            a.y = a.y - b.y;
            return a;
        }
        public static PointFloat operator *(PointFloat a, PointFloat b)
        {
            a.x = a.x * b.x;
            a.y = a.y * b.y;
            return a;
        }
        public static PointFloat operator /(PointFloat a, PointFloat b)
        {
            a.x = a.x / b.x;
            a.y = a.y / b.y;
            return a;
        }
        public static PointFloat operator +(PointFloat a, float b)
        {
            a.x = a.x + b;
            a.y = a.y + b;
            return a;
        }
        public static PointFloat operator -(PointFloat a, float b)
        {
            a.x = a.x - b;
            a.y = a.y - b;
            return a;
        }
        public static PointFloat operator *(PointFloat a, float b)
        {
            a.x = a.x * b;
            a.y = a.y * b;
            return a;
        }
        public static PointFloat operator /(PointFloat a, float b)
        {
            a.x = a.x / b;
            a.y = a.y / b;
            return a;
        }
        public float GetDirection()
        {
            return (float)Math.Atan2(x,y);
        }
        public float GetSpeed()
        {
            return Math.Abs(x) + Math.Abs(y);
        }
        public void Max(float max)
        {
            if(x > max){
                x = max;
            }
            else if(x < -max)
            {
                x = -max;
            }
            if ( y > max )
            {
                y = max;
            }
            else if ( y < -max )
            {
                y = -max;
            }
        }
    }
    public enum RenderMode
    {
        Normal = 1,
        WireFrame = 2,
        Hitboxes = 3
    }
}
