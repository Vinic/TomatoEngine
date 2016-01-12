using SharpGL;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace TomatoEngine
{
    public class TomatoMainEngine
    {
        public bool StartupComplete = false;
        public ResourceManager resourceManager;
        public static List<GameObject> GameObjects = new List<GameObject>();
        public RenderEngine renderEngine = new RenderEngine();
        public GameSettings settings = new GameSettings(1f);
        public static Random GameRandom = new Random();
        private static List<GameObject> trash = new List<GameObject>();
        private static List<GameObject> toAdd = new List<GameObject>();
        public static DebugTools DebugTools;
        public bool Paused = false;
        private Stopwatch _starDrawTime = new Stopwatch();
        private Stopwatch _starUpdateTime = new Stopwatch();
        public int UpdateTime = 0;
        public int DrawTime = 0;
        public ILevel _level;
        private SharpGLForm _form;
        public TomatoMainEngine(SharpGLForm form)
        {
            _form = form;
            DebugTools = new DebugTools(this);
            DebugTools.LogToConsole("Start Up");
        }
        public void InitEngine(OpenGL gl)
        {
            try
            {
                DebugTools.LogToConsole("Prepareing GL");
                //setup open gl and activate modes
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
                gl.Enable(OpenGL.GL_BLEND);
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                //fffgl.Enable(OpenGL.GL_LIGHTING);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
                gl.ShadeModel(OpenGL.GL_SMOOTH);
                //Darw the first frame with a load test
                Draw(gl);

                DebugTools.LogToConsole("Loading Resources");
                //Loading the resources and bind to gl
                resourceManager = new ResourceManager();
                DebugTools.LogToConsole("Applying Textures");
                resourceManager.InitTextures(gl);
                DebugTools.LogToConsole("Loading Level");
                //Loading level
                //_level = new MasterMindGame.MasterMind();
                //_level.Build(this);
                _level = new MasterMindGame.MasterMind();
                _level.Build(this);
                GameObjects.Sort(
                    delegate(GameObject p1, GameObject p2)
                    {
                        return p1.Z_Index.CompareTo(p2.Z_Index);
                    }
                );
                //Startup is complete
                StartupComplete = true;
            }
            catch (Exception error)
            {
                DebugTools.LogError(error);
            }

        }

        public void Update()
        {
            _starUpdateTime.Reset();
            _starUpdateTime.Start();
            _level.Update(settings);
            PhysEngine.PhysInteractions = 0;
            if (!StartupComplete)
            {
                return;
            }
            if (trash.Count > 0)
            {
                for (int i = 0; i < trash.Count; i++)
                {
                    GameObjects.Remove(trash[i]);
                }
                trash.Clear();
            }
            if (toAdd.Count > 0)
            {
                for (int i = 0; i < toAdd.Count; i++)
                {
                    GameObjects.Add(toAdd[i]);
                }
                toAdd.Clear();
                GameObjects.Sort(
                    delegate(GameObject p1, GameObject p2)
                    {
                        return p1.Z_Index.CompareTo(p2.Z_Index);
                    }
                );
            }

            if (!Paused)
            {
                CamController.Update();
                foreach (GameObject obj in GameObjects)
                {
                    obj.Update(settings);
                }
            }
            _starUpdateTime.Stop();
            UpdateTime = (int)_starUpdateTime.ElapsedMilliseconds;
        }


        public void Draw(OpenGL gl)
        {
            _starDrawTime.Reset();
            _starDrawTime.Start();
            if (StartupComplete)
            {
                renderEngine.RenderObjects(gl, GameObjects.ToArray());
                if (Paused)
                {
                    gl.DrawText(100, 100, 1f, 1f, 1f, "verdana", 20, "Paused");
                }
            }
            else
            {
                gl.DrawText(100, 100, 1f, 1f, 1f, "verdana", 20, "Loading");
            }
            if (_level != null)
            {
                _level.Draw(gl);
            }
            _starDrawTime.Stop();
            DrawTime = (int)_starDrawTime.ElapsedMilliseconds;
        }

        public void Resized(OpenGL gl, double aspect)
        {

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
            if (key != Keys.F1 && key != Keys.F2 && key != Keys.F3 && key != Keys.F12)
            {
                ControlKeys.KeyDown(key);
            }
        }
        public void KeyUp(Keys key)
        {
            if (key != Keys.F1 && key != Keys.F2 && key != Keys.F3 && key != Keys.F12)
            {
                ControlKeys.KeyUp(key);
            }
            else if (key == Keys.F1)
            {
                renderEngine.SetRenderMode(RenderMode.WireFrame);
                DebugTools.LogToConsole("RenderMode: WireFrame");
            }
            else if (key == Keys.F2)
            {
                renderEngine.SetRenderMode(RenderMode.Normal);
                DebugTools.LogToConsole("RenderMode: Normal");
            }
            else if (key == Keys.F12)
            {
                if (DebugTools.Visible == false)
                {
                    DebugTools.Show();
                    DebugTools.LogToConsole("Console open");
                }
                else
                {
                    DebugTools.Hide();
                }


            }
            else if (key == Keys.F3)
            {
                renderEngine.SetRenderMode(RenderMode.Hitboxes);
                DebugTools.LogToConsole("RenderMode: Hitboxes");
            }

        }
        public static int GetNewEntityId()
        {
            bool good = false;
            int id = 0;
            while (!good)
            {
                id = GameRandom.Next(100000000);
                bool isTaken = false;
                foreach (GameObject obj in GameObjects)
                {
                    if (obj.EntityId == id)
                    {
                        isTaken = true;
                    }
                }
                if (!isTaken)
                {
                    good = true;
                }
            }
            return id;
        }
        public static GameObject GetRenderObject(int entityId)
        {
            foreach (GameObject obj in GameObjects)
            {
                if (obj.EntityId == entityId)
                {
                    return obj;
                }
            }
            return null;
        }

        public static bool RemoveRenderObject(int entityId)
        {

            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].EntityId == entityId)
                {
                    trash.Add(GameObjects[i]);
                    return true;
                }
            }
            return false;
        }
        public static void AddGameObject(GameObject obj)
        {
            toAdd.Add(obj);
        }
        public void SetSize(int width, int height)
        {
            _form.Width = width;
            _form.Height = height;
        }
        public void LockSize(bool locked)
        {
            settings.SizeLocked = locked;
            if(locked){
                _form.FormBorderStyle = FormBorderStyle.FixedSingle;
                _form.MaximizeBox = false;
            }
            else
            {
                _form.FormBorderStyle = FormBorderStyle.Sizable;
                _form.MaximizeBox = true;
            }
        }
    }
}
