namespace TomatoEngine
{
    partial class DebugTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextOutput = new System.Windows.Forms.TextBox();
            this.Pause_Toggle = new System.Windows.Forms.CheckBox();
            this.ObjectsAmountText = new System.Windows.Forms.Label();
            this.UpdateInfoTimer = new System.Windows.Forms.Timer(this.components);
            this.ResetButton = new System.Windows.Forms.Button();
            this.PhysLoad = new System.Windows.Forms.ProgressBar();
            this.progressbartext = new System.Windows.Forms.Label();
            this.DrawTimeText = new System.Windows.Forms.Label();
            this.UpdateTimeText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextOutput
            // 
            this.TextOutput.Enabled = false;
            this.TextOutput.Location = new System.Drawing.Point(12, 12);
            this.TextOutput.Multiline = true;
            this.TextOutput.Name = "TextOutput";
            this.TextOutput.Size = new System.Drawing.Size(260, 237);
            this.TextOutput.TabIndex = 0;
            // 
            // Pause_Toggle
            // 
            this.Pause_Toggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.Pause_Toggle.AutoSize = true;
            this.Pause_Toggle.Location = new System.Drawing.Point(279, 12);
            this.Pause_Toggle.Name = "Pause_Toggle";
            this.Pause_Toggle.Size = new System.Drawing.Size(47, 23);
            this.Pause_Toggle.TabIndex = 1;
            this.Pause_Toggle.Text = "Pause";
            this.Pause_Toggle.UseVisualStyleBackColor = true;
            this.Pause_Toggle.CheckedChanged += new System.EventHandler(this.Pause_Toggle_CheckedChanged);
            // 
            // ObjectsAmountText
            // 
            this.ObjectsAmountText.AutoSize = true;
            this.ObjectsAmountText.Location = new System.Drawing.Point(279, 168);
            this.ObjectsAmountText.Name = "ObjectsAmountText";
            this.ObjectsAmountText.Size = new System.Drawing.Size(29, 13);
            this.ObjectsAmountText.TabIndex = 2;
            this.ObjectsAmountText.Text = "Wait";
            // 
            // UpdateInfoTimer
            // 
            this.UpdateInfoTimer.Interval = 20;
            this.UpdateInfoTimer.Tick += new System.EventHandler(this.UpdateInfoTimer_Tick);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(282, 41);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(44, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PhysLoad
            // 
            this.PhysLoad.Location = new System.Drawing.Point(282, 226);
            this.PhysLoad.Maximum = 1;
            this.PhysLoad.Name = "PhysLoad";
            this.PhysLoad.Size = new System.Drawing.Size(358, 23);
            this.PhysLoad.TabIndex = 4;
            // 
            // progressbartext
            // 
            this.progressbartext.AutoSize = true;
            this.progressbartext.Location = new System.Drawing.Point(282, 207);
            this.progressbartext.Name = "progressbartext";
            this.progressbartext.Size = new System.Drawing.Size(109, 13);
            this.progressbartext.TabIndex = 5;
            this.progressbartext.Text = "Physic engine activity";
            // 
            // DrawTimeText
            // 
            this.DrawTimeText.AutoSize = true;
            this.DrawTimeText.Location = new System.Drawing.Point(279, 71);
            this.DrawTimeText.Name = "DrawTimeText";
            this.DrawTimeText.Size = new System.Drawing.Size(64, 13);
            this.DrawTimeText.TabIndex = 6;
            this.DrawTimeText.Text = "Draw Time: ";
            // 
            // UpdateTimeText
            // 
            this.UpdateTimeText.AutoSize = true;
            this.UpdateTimeText.Location = new System.Drawing.Point(279, 88);
            this.UpdateTimeText.Name = "UpdateTimeText";
            this.UpdateTimeText.Size = new System.Drawing.Size(74, 13);
            this.UpdateTimeText.TabIndex = 7;
            this.UpdateTimeText.Text = "Update Time: ";
            // 
            // DebugTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 261);
            this.ControlBox = false;
            this.Controls.Add(this.UpdateTimeText);
            this.Controls.Add(this.DrawTimeText);
            this.Controls.Add(this.progressbartext);
            this.Controls.Add(this.PhysLoad);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ObjectsAmountText);
            this.Controls.Add(this.Pause_Toggle);
            this.Controls.Add(this.TextOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DebugTools";
            this.ShowInTaskbar = false;
            this.Text = "DebugTools";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.DebugTools_Activated);
            this.Deactivate += new System.EventHandler(this.DebugTools_Deactivate);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DebugTools_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextOutput;
        private System.Windows.Forms.CheckBox Pause_Toggle;
        private System.Windows.Forms.Label ObjectsAmountText;
        private System.Windows.Forms.Timer UpdateInfoTimer;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ProgressBar PhysLoad;
        private System.Windows.Forms.Label progressbartext;
        private System.Windows.Forms.Label DrawTimeText;
        private System.Windows.Forms.Label UpdateTimeText;
    }
}