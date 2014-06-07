namespace Player
{
    partial class Converting_Progress
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
            this.Conversion_Progressbar = new System.Windows.Forms.ProgressBar();
            this.convertingIndicator_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Conversion_Progressbar
            // 
            this.Conversion_Progressbar.Location = new System.Drawing.Point(10, 5);
            this.Conversion_Progressbar.MarqueeAnimationSpeed = 1;
            this.Conversion_Progressbar.Name = "Conversion_Progressbar";
            this.Conversion_Progressbar.Size = new System.Drawing.Size(206, 39);
            this.Conversion_Progressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Conversion_Progressbar.TabIndex = 0;
            // 
            // convertingIndicator_label
            // 
            this.convertingIndicator_label.AutoSize = true;
            this.convertingIndicator_label.Location = new System.Drawing.Point(37, 60);
            this.convertingIndicator_label.Name = "convertingIndicator_label";
            this.convertingIndicator_label.Size = new System.Drawing.Size(154, 13);
            this.convertingIndicator_label.TabIndex = 2;
            this.convertingIndicator_label.Text = "Converting . . . Please Wait . . .";
            // 
            // Converting_Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 96);
            this.ControlBox = false;
            this.Controls.Add(this.convertingIndicator_label);
            this.Controls.Add(this.Conversion_Progressbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(234, 120);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(234, 120);
            this.Name = "Converting_Progress";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar Conversion_Progressbar;
        private System.Windows.Forms.Label convertingIndicator_label;

    }
}