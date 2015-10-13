using SharpGL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TomatoEngine
{
    public class TomatoMainEngine
    {
        public bool StartupComplete = false;
        public ResourceManager resourceManager;
        public static List<RenderObject> Objects = new List<RenderObject>();
        public RenderEngine renderEngine = new RenderEngine();
        private float r = 0f;
        public TomatoMainEngine()
        {

        }
        public void InitEngine(OpenGL gl)
        {
            Random r = new Random();
            gl.Enable(OpenGL.GL_BLEND);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            resourceManager = new ResourceManager();
            var tex = ResourceManager.GetTexture("test");
            tex.InitTexture(gl);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            StartupComplete = true;
            SoundPool.PlaySound("piano2");
            for (var i = 0; i < 3000; i++ )
            {
                Objects.Add(new RenderObject(r.Next(-25, 25), r.Next(-15, 15), 1, 1));
            }
            


        }
        public void Draw(OpenGL gl)
        {
            if(StartupComplete){
                foreach(RenderObject o in Objects){
                    o.SetRot(r);
                }
                if(ControlKeys.IsKeyDown("A")){
                    r = r + 0.05f;
                }
                if (ControlKeys.IsKeyDown("D"))
                {
                    r = r - 0.05f;
                }
                CamController.X = r;
                
                renderEngine.RenderObjects(gl, Objects.ToArray());

            }
            else
            {
                gl.DrawText(100,100,1f,1f,1f,"verdana", 20, "Loading");
            }
        }
        public void Resized(OpenGL gl, double aspect){
            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            gl.Perspective(60.0f, aspect, 0.01, 100.0);
            CamController.Aspect = aspect;
            //  Use the 'look at' helper function to position and aim the camera.
            CamController.SetCam(gl);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        public void KeyDown(Keys key)
        {
            ControlKeys.KeyDown(key);
        }
        public void KeyUp(Keys key)
        {
            ControlKeys.KeyUp(key);
        }

    }
}
