using SharpGL;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace TomatoEngine
{
    public class TomatoMainEngine
    {
        public bool StartupComplete = false;
        public ResourceManager resourceManager;
        public static List<RenderObject> GameObjects = new List<RenderObject>();
        public RenderEngine renderEngine = new RenderEngine();
        public GameSettings settings = new GameSettings(1f);
        private static Random GameRandom = new Random();
        private static List<RenderObject> trash = new List<RenderObject>();
        private static List<RenderObject> toAdd = new List<RenderObject>();
        public TomatoMainEngine()
        {

        }
        public void InitEngine(OpenGL gl)
        {

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            Random r = new Random();
            gl.Enable(OpenGL.GL_BLEND);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            Draw(gl);
            resourceManager = new ResourceManager();
            resourceManager.InitTextures(gl);
            StartupComplete = true;
            Levels.SpaceTest(this);
        }

        public void Update()
        {
            if(!StartupComplete){
                return;
            }
            if(trash.Count > 0){
                for ( int i=0; i<trash.Count; i++ )
                {
                    GameObjects.Remove(trash[i]);
                }
                trash.Clear();
            }
            if(toAdd.Count > 0){
                for ( int i=0; i<toAdd.Count; i++ )
                {
                    GameObjects.Add(toAdd[i]);
                }
                toAdd.Clear();
            }
            foreach(RenderObject obj in GameObjects){
                obj.Update(settings);
            }
        }


        public void Draw(OpenGL gl)
        {
            if(StartupComplete){
                renderEngine.RenderObjects(gl, GameObjects.ToArray());
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
            if ( key != Keys.F1 && key != Keys.F2 )
            {
                ControlKeys.KeyDown(key);
            }
        }
        public void KeyUp(Keys key)
        {
            if ( key != Keys.F1 && key != Keys.F2 )
            {
                ControlKeys.KeyUp(key);
            }
            else if ( key == Keys.F1 )
            {
                renderEngine.SetRenderMode(RenderMode.WireFrame);
            }
            else if ( key == Keys.F2 )
            {
                renderEngine.SetRenderMode(RenderMode.Normal);
            }
            
        }
        public static int GetNewEntityId()
        {
            bool good = false;
            int id = 0;
            while(!good){
                id = GameRandom.Next(100000000);
                bool isTaken = false;
                foreach(RenderObject obj in GameObjects){
                    if(obj.EntityId == id){
                        isTaken = true;
                    }
                }
                if(!isTaken){
                    good = true;
                }
            }
            return id;
        }
        public static RenderObject GetRenderObject(int entityId)
        {
            foreach ( RenderObject obj in GameObjects )
            {
                if ( obj.EntityId == entityId )
                {
                    return obj;
                }
            }
            return null;
        }

        public static bool RemoveRenderObject(int entityId)
        {
            for ( int i=0; i<GameObjects.Count; i++ )
            {
                if ( GameObjects[i].EntityId == entityId )
                {
                    trash.Add(GameObjects[i]);
                    return true;
                }
            }
            return false;
        }
        public static void AddGameObject(RenderObject obj)
        {
            toAdd.Add(obj);
        }
    }
}
