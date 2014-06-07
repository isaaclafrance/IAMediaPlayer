namespace Player
{
    partial class Schedule_Recording_Dialog
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
            this.Start_Schedule_Recording_GroupBox = new System.Windows.Forms.GroupBox();
            this.Is_Recording_Checkbox = new System.Windows.Forms.CheckBox();
            this.Is_Scheduling_CheckBox = new System.Windows.Forms.CheckBox();
            this.Stop_Scheduling_Button = new System.Windows.Forms.Button();
            this.Start_Scheduling_Button = new System.Windows.Forms.Button();
            this.Set_When_groupBox = new System.Windows.Forms.GroupBox();
            this.Date_Label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Time_of_Day_comboBox1 = new System.Windows.Forms.ComboBox();
            this.Scheduling_Minute_textBox1 = new System.Windows.Forms.TextBox();
            this.Scheduling_Hour_textbox1 = new System.Windows.Forms.TextBox();
            this.Select_Day_Button = new System.Windows.Forms.Button();
            this.Set_How_Long_groupBox1 = new System.Windows.Forms.GroupBox();
            this.Recording_Length_Second_textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Recording_Length_Minute_textBox = new System.Windows.Forms.TextBox();
            this.Recording_Length_Hour_textBox = new System.Windows.Forms.TextBox();
            this.Schedule_Recording_timer = new System.Windows.Forms.Timer(this.components);
            this.Schedule_Recording_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Set_Name_groupBox1 = new System.Windows.Forms.GroupBox();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Select_Name_button = new System.Windows.Forms.Button();
            this.Schedule_Is_Recording_timer = new System.Windows.Forms.Timer(this.components);
            this.Start_Schedule_Recording_GroupBox.SuspendLayout();
            this.Set_When_groupBox.SuspendLayout();
            this.Set_How_Long_groupBox1.SuspendLayout();
            this.Set_Name_groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Start_Schedule_Recording_GroupBox
            // 
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Is_Recording_Checkbox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Is_Scheduling_CheckBox);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Stop_Scheduling_Button);
            this.Start_Schedule_Recording_GroupBox.Controls.Add(this.Start_Scheduling_Button);
            this.Start_Schedule_Recording_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Schedule_Recording_GroupBox.ForeColor = System.Drawing.Color.Blue;
            this.Start_Schedule_Recording_GroupBox.Location = new System.Drawing.Point(3, 233);
            this.Start_Schedule_Recording_GroupBox.Name = "Start_Schedule_Recording_GroupBox";
            this.Start_Schedule_Recording_GroupBox.Size = new System.Drawing.Size(233, 82);
            this.Start_Schedule_Recording_GroupBox.TabIndex = 2;
            this.Start_Schedule_Recording_GroupBox.TabStop = false;
            this.Start_Schedule_Recording_GroupBox.Text = "Step 4:  Start Scheduled  Recording";
            // 
            // Is_Recording_Checkbox
            // 
            this.Is_Recording_Checkbox.Enabled = false;
            this.Is_Recording_Checkbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Is_Recording_Checkbox.Location = new System.Drawing.Point(127, 21);
            this.Is_Recording_Checkbox.Name = "Is_Recording_Checkbox";
            this.Is_Recording_Checkbox.Size = new System.Drawing.Size(99, 25);
            this.Is_Recording_Checkbox.TabIndex = 3;
            this.Is_Recording_Checkbox.Text = "Is Recording";
            this.Is_Recording_Checkbox.UseVisualStyleBackColor = true;
            // 
            // Is_Scheduling_CheckBox
            // 
            this.Is_Scheduling_CheckBox.Enabled = false;
            this.Is_Scheduling_CheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Is_Scheduling_CheckBox.Location = new System.Drawing.Point(22, 21);
            this.Is_Scheduling_CheckBox.Name = "Is_Scheduling_CheckBox";
            this.Is_Scheduling_CheckBox.Size = new System.Drawing.Size(99, 25);
            this.Is_Scheduling_CheckBox.TabIndex = 2;
            this.Is_Scheduling_CheckBox.Text = "Is Scheduling";
            this.Is_Scheduling_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Stop_Scheduling_Button
            // 
            this.Stop_Scheduling_Button.Location = new System.Drawing.Point(164, 50);
            this.Stop_Scheduling_Button.Name = "Stop_Scheduling_Button";
            this.Stop_Scheduling_Button.Size = new System.Drawing.Size(60, 26);
            this.Stop_Scheduling_Button.TabIndex = 1;
            this.Stop_Scheduling_Button.Text = "Stop";
            this.Stop_Scheduling_Button.UseVisualStyleBackColor = true;
            this.Stop_Scheduling_Button.Click += new System.EventHandler(this.Stop_Scheduling_Button_Click);
            // 
            // Start_Scheduling_Button
            // 
            this.Start_Scheduling_Button.Location = new System.Drawing.Point(8, 50);
            this.Start_Scheduling_Button.Name = "Start_Scheduling_Button";
            this.Start_Scheduling_Button.Size = new System.Drawing.Size(144, 26);
            this.Start_Scheduling_Button.TabIndex = 0;
            this.Start_Scheduling_Button.Text = "Schedule Recording";
            this.Start_Scheduling_Button.UseVisualStyleBackColor = true;
            this.Start_Scheduling_Button.Click += new System.EventHandler(this.Start_Scheduling_Button_Click);
            // 
            // Set_When_groupBox
            // 
            this.Set_When_groupBox.Controls.Add(this.Date_Label);
            this.Set_When_groupBox.Controls.Add(this.label2);
            this.Set_When_groupBox.Controls.Add(this.label1);
            this.Set_When_groupBox.Controls.Add(this.Time_of_Day_comboBox1);
            this.Set_When_groupBox.Controls.Add(this.Scheduling_Minute_textBox1);
            this.Set_When_groupBox.Controls.Add(this.Scheduling_Hour_textbox1);
            this.Set_When_groupBox.Controls.Add(this.Select_Day_Button);
            this.Set_When_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set_When_groupBox.ForeColor = System.Drawing.Color.Red;
            this.Set_When_groupBox.Location = new System.Drawing.Point(3, 2);
            this.Set_When_groupBox.Name = "Set_When_groupBox";
            this.Set_When_groupBox.Size = new System.Drawing.Size(233, 94);
            this.Set_When_groupBox.TabIndex = 3;
            this.Set_When_groupBox.TabStop = false;
            this.Set_When_groupBox.Text = "Step 1:  Set When to Record";
            // 
            // Date_Label
            // 
            this.Date_Label.AutoSize = true;
            this.Date_Label.Location = new System.Drawing.Point(11, 33);
            this.Date_Label.Name = "Date_Label";
            this.Date_Label.Size = new System.Drawing.Size(37, 16);
            this.Date_Label.TabIndex = 12;
            this.Date_Label.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Selected Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Selected Day";
            // 
            // Time_of_Day_comboBox1
            // 
            this.Time_of_Day_comboBox1.FormattingEnabled = true;
            this.Time_of_Day_comboBox1.Items.AddRange(new object[] {
            "PM",
            "AM"});
            this.Time_of_Day_comboBox1.Location = new System.Drawing.Point(119, 69);
            this.Time_of_Day_comboBox1.Name = "Time_of_Day_comboBox1";
            this.Time_of_Day_comboBox1.Size = new System.Drawing.Size(48, 24);
            this.Time_of_Day_comboBox1.TabIndex = 9;
            this.Time_of_Day_comboBox1.Text = "PM";
            // 
            // Scheduling_Minute_textBox1
            // 
            this.Scheduling_Minute_textBox1.Location = new System.Drawing.Point(81, 69);
            this.Scheduling_Minute_textBox1.Name = "Scheduling_Minute_textBox1";
            this.Scheduling_Minute_textBox1.Size = new System.Drawing.Size(32, 22);
            this.Scheduling_Minute_textBox1.TabIndex = 7;
            this.Scheduling_Minute_textBox1.Text = "Min";
            this.Scheduling_Minute_textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Scheduling_Minute_textBox1.Click += new System.EventHandler(this.Scheduling_Minute_textBox1_Click);
            this.Scheduling_Minute_textBox1.Leave += new System.EventHandler(this.Scheduling_Minute_textBox1_Leave);
            // 
            // Scheduling_Hour_textbox1
            // 
            this.Scheduling_Hour_textbox1.Location = new System.Drawing.Point(35, 69);
            this.Scheduling_Hour_textbox1.Name = "Scheduling_Hour_textbox1";
            this.Scheduling_Hour_textbox1.Size = new System.Drawing.Size(40, 22);
            this.Scheduling_Hour_textbox1.TabIndex = 4;
            this.Scheduling_Hour_textbox1.Text = "Hour";
            this.Scheduling_Hour_textbox1.Click += new System.EventHandler(this.Scheduling_Hour_textbox1_Click);
            this.Scheduling_Hour_textbox1.Leave += new System.EventHandler(this.Scheduling_Hour_textbox1_Leave);
            // 
            // Select_Day_Button
            // 
            this.Select_Day_Button.Location = new System.Drawing.Point(119, 21);
            this.Select_Day_Button.Name = "Select_Day_Button";
            this.Select_Day_Button.Size = new System.Drawing.Size(90, 26);
            this.Select_Day_Button.TabIndex = 1;
            this.Select_Day_Button.Text = "Select Day";
            this.Select_Day_Button.UseVisualStyleBackColor = true;
            this.Select_Day_Button.Click += new System.EventHandler(this.Select_Day_Button_Click);
            // 
            // Set_How_Long_groupBox1
            // 
            this.Set_How_Long_groupBox1.Controls.Add(this.Recording_Length_Second_textbox);
            this.Set_How_Long_groupBox1.Controls.Add(this.label4);
            this.Set_How_Long_groupBox1.Controls.Add(this.Recording_Length_Minute_textBox);
            this.Set_How_Long_groupBox1.Controls.Add(this.Recording_Length_Hour_textBox);
            this.Set_How_Long_groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set_How_Long_groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.Set_How_Long_groupBox1.Location = new System.Drawing.Point(3, 102);
            this.Set_How_Long_groupBox1.Name = "Set_How_Long_groupBox1";
            this.Set_How_Long_groupBox1.Size = new System.Drawing.Size(233, 67);
            this.Set_How_Long_groupBox1.TabIndex = 4;
            this.Set_How_Long_groupBox1.TabStop = false;
            this.Set_How_Long_groupBox1.Text = "Step 2:  Set How Long to Record";
            // 
            // Recording_Length_Second_textbox
            // 
            this.Recording_Length_Second_textbox.Location = new System.Drawing.Point(119, 36);
            this.Recording_Length_Second_textbox.Name = "Recording_Length_Second_textbox";
            this.Recording_Length_Second_textbox.Size = new System.Drawing.Size(32, 22);
            this.Recording_Length_Second_textbox.TabIndex = 12;
            this.Recording_Length_Second_textbox.Text = "Sec";
            this.Recording_Length_Second_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Recording_Length_Second_textbox.Click += new System.EventHandler(this.Recording_Length_Second_textbox_Click);
            this.Recording_Length_Second_textbox.Leave += new System.EventHandler(this.Recording_Length_Second_textbox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Selected Recording Length";
            // 
            // Recording_Length_Minute_textBox
            // 
            this.Recording_Length_Minute_textBox.Location = new System.Drawing.Point(81, 37);
            this.Recording_Length_Minute_textBox.Name = "Recording_Length_Minute_textBox";
            this.Recording_Length_Minute_textBox.Size = new System.Drawing.Size(32, 22);
            this.Recording_Length_Minute_textBox.TabIndex = 7;
            this.Recording_Length_Minute_textBox.Text = "Min";
            this.Recording_Length_Minute_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Recording_Length_Minute_textBox.Click += new System.EventHandler(this.Recording_Length_Minute_textBox_Click);
            this.Recording_Length_Minute_textBox.Leave += new System.EventHandler(this.Recording_Length_Minute_textBox_Leave);
            // 
            // Recording_Length_Hour_textBox
            // 
            this.Recording_Length_Hour_textBox.Location = new System.Drawing.Point(35, 36);
            this.Recording_Length_Hour_textBox.Name = "Recording_Length_Hour_textBox";
            this.Recording_Length_Hour_textBox.Size = new System.Drawing.Size(40, 22);
            this.Recording_Length_Hour_textBox.TabIndex = 4;
            this.Recording_Length_Hour_textBox.Text = "Hour";
            this.Recording_Length_Hour_textBox.Click += new System.EventHandler(this.Recording_Length_Hour_textBox_Click);
            this.Recording_Length_Hour_textBox.Leave += new System.EventHandler(this.Recording_Length_Hour_textBox_Leave);
            // 
            // Schedule_Recording_timer
            // 
            this.Schedule_Recording_timer.Tick += new System.EventHandler(this.Schedule_Recording_timer_Tick);
            // 
            // Set_Name_groupBox1
            // 
            this.Set_Name_groupBox1.Controls.Add(this.Name_textBox);
            this.Set_Name_groupBox1.Controls.Add(this.Select_Name_button);
            this.Set_Name_groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set_Name_groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.Set_Name_groupBox1.Location = new System.Drawing.Point(3, 175);
            this.Set_Name_groupBox1.Name = "Set_Name_groupBox1";
            this.Set_Name_groupBox1.Size = new System.Drawing.Size(233, 52);
            this.Set_Name_groupBox1.TabIndex = 5;
            this.Set_Name_groupBox1.TabStop = false;
            this.Set_Name_groupBox1.Text = "Step 3:  Set Name of Recording";
            // 
            // Name_textBox
            // 
            this.Name_textBox.Location = new System.Drawing.Point(13, 23);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(126, 22);
            this.Name_textBox.TabIndex = 1;
            this.Name_textBox.Text = "Scheduled_Recording";
            // 
            // Select_Name_button
            // 
            this.Select_Name_button.Location = new System.Drawing.Point(149, 23);
            this.Select_Name_button.Name = "Select_Name_button";
            this.Select_Name_button.Size = new System.Drawing.Size(75, 23);
            this.Select_Name_button.TabIndex = 0;
            this.Select_Name_button.Text = "Select";
            this.Select_Name_button.UseVisualStyleBackColor = true;
            this.Select_Name_button.Click += new System.EventHandler(this.Select_Name_button_Click);
            // 
            // Schedule_Is_Recording_timer
            // 
            this.Schedule_Is_Recording_timer.Interval = 2000;
            this.Schedule_Is_Recording_timer.Tick += new System.EventHandler(this.Schedule_Is_Recording_timer_Tick);
            // 
            // Schedule_Recording_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 320);
            this.ControlBox = false;
            this.Controls.Add(this.Set_Name_groupBox1);
            this.Controls.Add(this.Set_How_Long_groupBox1);
            this.Controls.Add(this.Set_When_groupBox);
            this.Controls.Add(this.Start_Schedule_Recording_GroupBox);
            this.MaximumSize = new System.Drawing.Size(255, 358);
            this.MinimumSize = new System.Drawing.Size(255, 358);
            this.Name = "Schedule_Recording_Dialog";
            this.Opacity = 0.85D;
            this.ShowInTaskbar = false;
            this.Text = "Scheduling Dialog";
            this.TopMost = true;
            this.Start_Schedule_Recording_GroupBox.ResumeLayout(false);
            this.Set_When_groupBox.ResumeLayout(false);
            this.Set_When_groupBox.PerformLayout();
            this.Set_How_Long_groupBox1.ResumeLayout(false);
            this.Set_How_Long_groupBox1.PerformLayout();
            this.Set_Name_groupBox1.ResumeLayout(false);
            this.Set_Name_groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox Start_Schedule_Recording_GroupBox;
        private System.Windows.Forms.CheckBox Is_Scheduling_CheckBox;
        private System.Windows.Forms.Button Stop_Scheduling_Button;
        private System.Windows.Forms.Button Start_Scheduling_Button;
        public System.Windows.Forms.GroupBox Set_When_groupBox;
        private System.Windows.Forms.ComboBox Time_of_Day_comboBox1;
        private System.Windows.Forms.TextBox Scheduling_Minute_textBox1;
        private System.Windows.Forms.TextBox Scheduling_Hour_textbox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Select_Day_Button;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.GroupBox Set_How_Long_groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Recording_Length_Minute_textBox;
        private System.Windows.Forms.TextBox Recording_Length_Hour_textBox;
        private System.Windows.Forms.TextBox Recording_Length_Second_textbox;
        public System.Windows.Forms.Label Date_Label;
        private System.Windows.Forms.Timer Schedule_Recording_timer;
        private System.ComponentModel.BackgroundWorker Schedule_Recording_backgroundWorker;
        private System.Windows.Forms.CheckBox Is_Recording_Checkbox;
        public System.Windows.Forms.GroupBox Set_Name_groupBox1;
        private System.Windows.Forms.TextBox Name_textBox;
        private System.Windows.Forms.Button Select_Name_button;
        private System.Windows.Forms.Timer Schedule_Is_Recording_timer;
    }
}