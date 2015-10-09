using SharpGL;

namespace TomatoEngine
{
    public class TomatoMainEngine
    {
        public bool StartupComplete = false;
        public static ResourceManager resourceManager;
        public TomatoMainEngine()
        {
            
        }
        public void InitEngine(OpenGL gl)
        {
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
        }
        public void Draw(OpenGL gl)
        {
            if(StartupComplete){
                var tex = ResourceManager.GetTexture("test");
                
                gl.Begin(OpenGL.GL_QUADS);
                gl.Color(1f,1f,1f);
                tex.BindToGL(gl);
                gl.TexCoord(0, 0);
                gl.Vertex(1f, 2f);
                gl.TexCoord(0, 1);
                gl.Vertex(2f, 2f);
                gl.TexCoord(1, 1);
                gl.Vertex(2f, 1f);
                gl.TexCoord(1, 0);
                gl.Vertex(1f, 1f);
                gl.End();
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

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(0, 0, -5, 0, 0, 0, 0, 1, 0);

            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

    }
}
