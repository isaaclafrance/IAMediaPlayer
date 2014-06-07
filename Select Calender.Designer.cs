namespace Player
{
    partial class Select_Calender
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
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.Choose_Date_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 3);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 1;
            // 
            // Choose_Date_button
            // 
            this.Choose_Date_button.Location = new System.Drawing.Point(66, 166);
            this.Choose_Date_button.Name = "Choose_Date_button";
            this.Choose_Date_button.Size = new System.Drawing.Size(94, 23);
            this.Choose_Date_button.TabIndex = 2;
            this.Choose_Date_button.Text = "Choose Date";
            this.Choose_Date_button.UseVisualStyleBackColor = true;
            this.Choose_Date_button.Click += new System.EventHandler(this.Choose_Date_button_Click);
            // 
            // Select_Calender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 192);
            this.ControlBox = false;
            this.Controls.Add(this.Choose_Date_button);
            this.Controls.Add(this.monthCalendar);
            this.MaximumSize = new System.Drawing.Size(245, 230);
            this.MinimumSize = new System.Drawing.Size(245, 230);
            this.Name = "Select_Calender";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select Calender";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Button Choose_Date_button;
    }
}