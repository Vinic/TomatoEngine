using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace TomatoEngine
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        public TomatoMainEngine engine;
        private bool _fullscreen = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        
        public SharpGLForm()
        {
            engine = new TomatoMainEngine(this);
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            //engine.Update();
            engine.Draw(gl);
        }



        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            engine.InitEngine(gl);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.

            engine.Resized(openGLControl.OpenGL, (double)Width / (double)Height);
        }

        /// <summary>
        /// The current rotation.
        /// </summary>

        private void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F11){
                ToggleFullScreen();
                return;
            }
            engine.KeyDown(e.KeyCode);
        }

        private void openGLControl_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void openGLControl_KeyUp(object sender, KeyEventArgs e)
        {
            engine.KeyUp(e.KeyCode);
        }

        private void GameUpdateTimer_Tick(object sender, EventArgs e)
        {
            engine.Update();
        }
        private void SetFullScreen()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            _fullscreen = true;
        }
        private void SetWindowed()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            _fullscreen = false;
        }
        public void ToggleFullScreen()
        {
            if ( _fullscreen )
            {
                SetWindowed();
            }
            else
            {
                SetFullScreen();
            }
        }
    }
}
