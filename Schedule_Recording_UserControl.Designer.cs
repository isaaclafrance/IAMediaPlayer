namespace Player
{
    partial class Schedule_Recording_UserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Start_Schedule_Recording_GroupBox = new System.Windows.Forms.GroupBox();
            this.Time_of_Day_comboBox = new System.Windows.Forms.ComboBox();
            this.Schedule_Recording_checkBox = new System.Windows.Forms.CheckBox();
            this.Scheduling_Minute_TextBox = new System.Windows.Forms.TextBox();
            this.Scheduling_Hour_TextBox = new System.Windows.Forms.TextBox();
            this.Is_Scheduling_CheckBox = new System.Windows.Forms.CheckBox();
            this.Stop_Scheduling_Button = new System.Windows.Forms.Button();
            this.Start_Scheduling_Button = new System.Windows.Forms.Button();
            this.Schedule_Recording_timer = new System.Windows.Forms.Timer(this.components);
            this.Schedule_Recording_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Start_Schedule_Recording_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Start_Schedule_Recording_GroupBox
            // 
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Time_of_Day_comboBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Schedule_Recording_checkBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Scheduling_Minute_TextBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Scheduling_Hour_TextBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Is_Scheduling_CheckBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Stop_Scheduling_Button);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Start_Scheduling_Button);
            this.Start_Schedule_Recording_GroupBox.ForeColor = System.Drawing.Color.Red;
            this.Start_Schedule_Recording_GroupBox.Location = new System.Drawing.Point(3, 0);
            this.Start_Schedule_Recording_GroupBox.Name = "Start_Schedule_Recording_GroupBox";
            this.Start_Schedule_Recording_GroupBox.Size = new System.Drawing.Size(282, 71);
            this.Start_Schedule_Recording_GroupBox.TabIndex = 1;
            this.Start_Schedule_Recording_GroupBox.TabStop = false;
            this.Start_Schedule_Recording_GroupBox.Text = "Step 1:   Start Schedule  Recording";
            // 
            // Time_of_Day_comboBox
            // 
            this.Time_of_Day_comboBox.FormattingEnabled = true;
            this.Time_of_Day_comboBox.Items.AddRange(new object[] {
            "PM",
            "AM"});
            this.Time_of_Day_comboBox.Location = new System.Drawing.Point(79, 16);
            this.Time_of_Day_comboBox.Name = "Time_of_Day_comboBox";
            this.Time_of_Day_comboBox.Size = new System.Drawing.Size(48, 21);
            this.Time_of_Day_comboBox.TabIndex = 9;
            this.Time_of_Day_comboBox.Text = "PM";
            // 
            // Schedule_Recording_checkBox
            // 
            this.Schedule_Recording_checkBox.AutoSize = true;
            this.Schedule_Recording_checkBox.Location = new System.Drawing.Point(133, 24);
            this.Schedule_Recording_checkBox.Name = "Schedule_Recording_checkBox";
            this.Schedule_Recording_checkBox.Size = new System.Drawing.Size(15, 14);
            this.Schedule_Recording_checkBox.TabIndex = 8;
            this.Schedule_Recording_checkBox.UseVisualStyleBackColor = true;
            this.Schedule_Recording_checkBox.CheckedChanged += new System.EventHandler(this.Schedule_Recording_checkBox_CheckedChanged);
            this.Schedule_Recording_checkBox.MouseHover += new System.EventHandler(this.Schedule_Recording_checkBox_MouseHover);
            // 
            // Scheduling_Minute_TextBox
            // 
            this.Scheduling_Minute_TextBox.Location = new System.Drawing.Point(41, 17);
            this.Scheduling_Minute_TextBox.Name = "Scheduling_Minute_TextBox";
            this.Scheduling_Minute_TextBox.Size = new System.Drawing.Size(32, 20);
            this.Scheduling_Minute_TextBox.TabIndex = 7;
            this.Scheduling_Minute_TextBox.Text = "Min";
            this.Scheduling_Minute_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Scheduling_Minute_TextBox.Click += new System.EventHandler(this.Scheduling_Minute_TextBox_Click);
            this.Scheduling_Minute_TextBox.Leave += new System.EventHandler(this.Scheduling_Minute_TextBox_Leave);
            // 
            // Scheduling_Hour_TextBox
            // 
            this.Scheduling_Hour_TextBox.Location = new System.Drawing.Point(3, 17);
            this.Scheduling_Hour_TextBox.Name = "Scheduling_Hour_TextBox";
            this.Scheduling_Hour_TextBox.Size = new System.Drawing.Size(32, 20);
            this.Scheduling_Hour_TextBox.TabIndex = 4;
            this.Scheduling_Hour_TextBox.Text = "Hour";
            this.Scheduling_Hour_TextBox.Click += new System.EventHandler(this.Scheduling_Hour_TextBox_Click);
            this.Scheduling_Hour_TextBox.Leave += new System.EventHandler(this.Scheduling_Hour_TextBox_Leave);
            // 
            // Is_Scheduling_CheckBox
            // 
            this.Is_Scheduling_CheckBox.Enabled = false;
            this.Is_Scheduling_CheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.Is_Scheduling_CheckBox.Location = new System.Drawing.Point(172, 12);
            this.Is_Scheduling_CheckBox.Name = "Is_Scheduling_CheckBox";
            this.Is_Scheduling_CheckBox.Size = new System.Drawing.Size(107, 25);
            this.Is_Scheduling_CheckBox.TabIndex = 2;
            this.Is_Scheduling_CheckBox.Text = "Is Scheduling";
            this.Is_Scheduling_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Stop_Scheduling_Button
            // 
            this.Stop_Scheduling_Button.Location = new System.Drawing.Point(146, 44);
            this.Stop_Scheduling_Button.Name = "Stop_Scheduling_Button";
            this.Stop_Scheduling_Button.Size = new System.Drawing.Size(60, 26);
            this.Stop_Scheduling_Button.TabIndex = 1;
            this.Stop_Scheduling_Button.Text = "Stop";
            this.Stop_Scheduling_Button.UseVisualStyleBackColor = true;
            this.Stop_Scheduling_Button.Click += new System.EventHandler(this.Stop_Scheduling_Button_Click);
            // 
            // Start_Scheduling_Button
            // 
            this.Start_Scheduling_Button.Location = new System.Drawing.Point(14, 44);
            this.Start_Scheduling_Button.Name = "Start_Scheduling_Button";
            this.Start_Scheduling_Button.Size = new System.Drawing.Size(113, 26);
            this.Start_Scheduling_Button.TabIndex = 0;
            this.Start_Scheduling_Button.Text = "Schedule Recording";
            this.Start_Scheduling_Button.UseVisualStyleBackColor = true;
            this.Start_Scheduling_Button.Click += new System.EventHandler(this.Start_Scheduling_Button_Click);
            // 
            // Schedule_Recording_timer
            // 
            this.Schedule_Recording_timer.Tick += new System.EventHandler(this.Schedule_Recording_timer_Tick);
            // 
            // Schedule_Recording_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Start_Schedule_Recording_GroupBox);
            this.Name = "Schedule_Recording_UserControl";
            this.Size = new System.Drawing.Size(282, 74);
            this.Start_Schedule_Recording_GroupBox.ResumeLayout(false);
            this.Start_Schedule_Recording_GroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Scheduling_Minute_TextBox;
        private System.Windows.Forms.TextBox Scheduling_Hour_TextBox;
        private System.Windows.Forms.CheckBox Is_Scheduling_CheckBox;
        private System.Windows.Forms.Button Stop_Scheduling_Button;
        private System.Windows.Forms.Button Start_Scheduling_Button;
        private System.Windows.Forms.ComboBox Time_of_Day_comboBox;
        public System.Windows.Forms.GroupBox Start_Schedule_Recording_GroupBox;
        private System.Windows.Forms.Timer Schedule_Recording_timer;
        private System.ComponentModel.BackgroundWorker Schedule_Recording_backgroundWorker;
        public System.Windows.Forms.CheckBox Schedule_Recording_checkBox;
    }
}
