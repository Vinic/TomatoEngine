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
            this.TextOutput = new System.Windows.Forms.TextBox();
            this.Pause_Toggle = new System.Windows.Forms.CheckBox();
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
            // DebugTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 261);
            this.ControlBox = false;
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
    }
}