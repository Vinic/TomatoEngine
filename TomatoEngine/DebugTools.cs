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
        public void Hide() 
        {
            base.Hide();
            UpdateInfoTimer.Stop();
        }
        public void Show()
        {
            base.Show();
            UpdateInfoTimer.Start();
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

        private void UpdateInfoTimer_Tick(object sender, EventArgs e)
        {
            ObjectsAmountText.Text = "Active objects count: " + TomatoMainEngine.GameObjects.Count;
            if ( PhysLoad.Maximum < PhysEngine.PhysInteractions )
            {
                PhysLoad.Maximum = PhysEngine.PhysInteractions;
            }
            PhysLoad.Value = PhysEngine.PhysInteractions;
            DrawTimeText.Text = "Draw time: " + _engine.DrawTime + "ms";
            UpdateTimeText.Text = "Update time: " + _engine.UpdateTime + "ms";
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            TomatoMainEngine.GameObjects.Clear();
            Levels.SpaceTest(_engine);
        }
    }
}
