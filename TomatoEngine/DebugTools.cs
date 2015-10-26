using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomatoEngine
{
    public partial class DebugTools : Form
    {
        private TomatoMainEngine _engine;
        private static bool _debugMode = true;
        public DebugTools(TomatoMainEngine engine)
        {
            InitializeComponent();
            _engine = engine;
        }

        public void LogToConsole(string text)
        {
            TextOutput.AppendText(text + System.Environment.NewLine);
        }
        public void LogError(Exception error)
        {
            if(_debugMode == true){
                this.Show();
            }
            if(this.Visible == true){
                LogToConsole("An Error is captured");
                LogToConsole(error.Source);
                LogToConsole(error.Message);
            }
        }

        private void DebugTools_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F12){
                this.Hide();
            }
        }

        private void DebugTools_Activated(object sender, EventArgs e)
        {
            this.Opacity = 100.0;
        }

        private void DebugTools_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 50.0;
        }

        private void Pause_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            _engine.Paused = Pause_Toggle.Checked;
        }
    }
}
