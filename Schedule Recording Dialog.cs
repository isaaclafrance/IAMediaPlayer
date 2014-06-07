using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Alvas.Audio;

namespace Player
{
    public partial class Schedule_Recording_Dialog : Form
    {
        #region Variable Field
            My_Player my_player;
            public DateTime scheduled_date;
            public DateTime scheduled_time;
            bool has_recorded;
            int hour_recording = 0;
            int minute_recording = 0;
            int second_recording = 0;
            int saving_num = 1;
            public string sched_name;
            private bool sched_name_choosen;

        #endregion

        public Schedule_Recording_Dialog(My_Player my_player, int saving_num)
        {
            InitializeComponent();

            this.my_player = my_player;
            scheduled_date = new DateTime();
            scheduled_time = new DateTime();
            sched_name_choosen = false;
            this.saving_num = saving_num;
        }
        public Schedule_Recording_Dialog(int saving_num)
        {
            InitializeComponent();
            scheduled_date = new DateTime();
            scheduled_time = new DateTime();
            sched_name_choosen = false;
            this.saving_num = saving_num;
        }

        private void Schedule_Recording_timer_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now > scheduled_time && has_recorded == false)
            {
                #region
                #region
                if (Recording_Length_Hour_textBox.Text == "Hour")
                {
                    hour_recording = 0;
                }
                else { hour_recording = Int32.Parse(Recording_Length_Hour_textBox.Text); }

                if (Recording_Length_Minute_textBox.Text == "Min")
                {
                    minute_recording = 0;
                }
                else { minute_recording = Int32.Parse(Recording_Length_Minute_textBox.Text); }

                if (Recording_Length_Second_textbox.Text == "Sec")
                {
                    second_recording = 10;
                }
                else { second_recording = Int32.Parse(Recording_Length_Second_textbox.Text); }
                #endregion

                my_player.Start_Recording_GroupBox.Controls["Recording_Hour_TextBox"].Text = hour_recording.ToString();
                my_player.Start_Recording_GroupBox.Controls["Recording_Minute_TextBox"].Text = minute_recording.ToString();
                my_player.Start_Recording_GroupBox.Controls["Recording_Second_TextBox"].Text = second_recording.ToString();
                #endregion

                if (!(my_player.recorder.State == DeviceState.InProgress || my_player.recorder.State == DeviceState.Opened)) //Check if recorder is already in use
                {
                    my_player.Record_with_Timer_Button.PerformClick();
                }

                Is_Recording_Checkbox.Checked = true;

                if (sched_name_choosen == false)
                {
                    Select_Name_button_Click(null, null);
                }

                has_recorded = true;

                Schedule_Recording_timer.Stop();
                Schedule_Is_Recording_timer.Start();
            }
        }
        private void Schedule_Is_Recording_timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= scheduled_time && has_recorded == true)
            {
                if (my_player.recorder.State == DeviceState.Closed && !my_player.Recording_CheckBox.Checked)
                {
                    //Schedule_Is_Recording_timer.Stop();
                    #region
                    my_player.Recording_Location_Textbox.Text = Path.ChangeExtension(Application.ExecutablePath, ".wav");
                    my_player.Compress_checkBox.CheckState = CheckState.Checked;

                    my_player.scheduledFileName = sched_name;
                    #endregion

                    my_player.Stop_Recording_Button_Click(null, null);
                    my_player.Save_Recording_Button_Click(saving_num, sched_name);
                }
                if (my_player.recorder.State == DeviceState.Closed && !my_player.Recording_CheckBox.Checked)
                {
                    Schedule_Is_Recording_timer.Stop();
                    Is_Recording_Checkbox.Checked = false;
                    Stop_Scheduling_Button.PerformClick();
                }
            }
        }

        private void Scheduling_Hour_textbox1_Click(object sender, EventArgs e)
        {
            Scheduling_Hour_textbox1.Text = "";

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Scheduling_Minute_textBox1_Click(object sender, EventArgs e)
        {
            Scheduling_Minute_textBox1.Text = "";

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Scheduling_Hour_textbox1_Leave(object sender, EventArgs e)
        {
            if (Scheduling_Hour_textbox1.Text == "")
            {
                Scheduling_Hour_textbox1.Text = "Hour";
            }
        }
        private void Scheduling_Minute_textBox1_Leave(object sender, EventArgs e)
        {
            if (Scheduling_Minute_textBox1.Text == "")
            {
                Scheduling_Minute_textBox1.Text = "Min";
            }
        }

        private void Recording_Length_Hour_textBox_Click(object sender, EventArgs e)
        {
            Recording_Length_Hour_textBox.Text = "";

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Blue;
            Set_Name_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Recording_Length_Minute_textBox_Click(object sender, EventArgs e)
        {
            Recording_Length_Minute_textBox.Text = "";

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Blue;
            Set_Name_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Recording_Length_Second_textbox_Click(object sender, EventArgs e)
        {
            Recording_Length_Second_textbox.Text = "";

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Blue;
            Set_Name_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Recording_Length_Hour_textBox_Leave(object sender, EventArgs e)
        {
            if (Recording_Length_Hour_textBox.Text == "")
            {
                Recording_Length_Hour_textBox.Text = "Hour";
            }
        }
        private void Recording_Length_Minute_textBox_Leave(object sender, EventArgs e)
        {
            if (Recording_Length_Minute_textBox.Text == "")
            {
                Recording_Length_Minute_textBox.Text = "Min";
            }
        }
        private void Recording_Length_Second_textbox_Leave(object sender, EventArgs e)
        {
            if (Recording_Length_Second_textbox.Text == "")
            {
                Recording_Length_Second_textbox.Text = "Sec";
            }
        }

        private void Select_Day_Button_Click(object sender, EventArgs e)
        {
            Select_Calender calender = new Select_Calender(this);
            calender.Show();

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Red;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
        }
        private void Select_Name_button_Click(object sender, EventArgs e)
        {

            if (sched_name_choosen == false)
            {
                sched_name = Name_textBox.Text;
                Name_textBox.Enabled = false;
                Select_Name_button.Text = "Deselect";

                sched_name_choosen = true;
            }
            else
            {
                if (Name_textBox.Text == "" || Name_textBox.Text == "")
                {
                    Name_textBox.Text = "Scheduled_Recording";
                }
                Name_textBox.Enabled = true;
                Select_Name_button.Text = "Select";

                sched_name_choosen = false;
            }

            Set_When_groupBox.ForeColor = Color.Blue;
            Set_How_Long_groupBox1.ForeColor = Color.Blue;
            Set_Name_groupBox1.ForeColor = Color.Blue;
            Start_Schedule_Recording_GroupBox.ForeColor = Color.Red;
        }

        private void Start_Scheduling_Button_Click(object sender, EventArgs e)
        {
            int hour = 0;
            int minute = 0;

            #region
                #region
                if (scheduled_date == null)
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Select_Day_Button);
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Select_Day_Button);
                }
                if (scheduled_date.Date.Day < DateTime.Now.Day)
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Choose Another Date", Select_Day_Button);
                    my_player.Schedule_Recording_toolTip.Show("Please Choose Another Date", Select_Day_Button);
                }
                if (Scheduling_Hour_textbox1.Text == "Hour")
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Hour_textbox1);
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Hour_textbox1);
                }
                else { hour = Int32.Parse(Scheduling_Hour_textbox1.Text); }

                if (Scheduling_Minute_textBox1.Text == "Min")
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Minute_textBox1);
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Minute_textBox1);
                }
                else { minute = Int32.Parse(Scheduling_Minute_textBox1.Text); }
                #endregion
                #region
                if (Time_of_Day_comboBox1.Text == "AM")
                {
                    if (hour == 12)
                    {
                        scheduled_time = new DateTime(scheduled_date.Year, scheduled_date.Month, scheduled_date.Day, 0, minute, 0);
                    }
                    else
                    {
                        scheduled_time = new DateTime(scheduled_date.Year, scheduled_date.Month, scheduled_date.Day, hour, minute, 0);
                    }
                }
                if (Time_of_Day_comboBox1.Text == "PM")
                {
                    if ((hour + 12) < 24)
                    {
                        scheduled_time = new DateTime(scheduled_date.Year, scheduled_date.Month, scheduled_date.Day, hour + 12, minute, 0);
                    }

                    if ((hour + 12) == 24)
                    {
                        scheduled_time = new DateTime(scheduled_date.Year, scheduled_date.Month, scheduled_date.Day, hour , minute, 0);
                    }
                } 
                #endregion
            #endregion

            if (Scheduling_Hour_textbox1.Text != "Hour" && Scheduling_Minute_textBox1.Text != "Min" && scheduled_date != null && !(scheduled_date.Date.Day < DateTime.Now.Day))
            {
                Schedule_Recording_timer.Start();

                has_recorded = false;
                Is_Scheduling_CheckBox.Checked = true;
                my_player.isScheduling = true;

                #region Disable Unneccesary Controls
                Set_When_groupBox.Enabled = false;
                Set_How_Long_groupBox1.Enabled = false;
                Set_Name_groupBox1.Enabled = false;
                Start_Scheduling_Button.Enabled = false;
                Select_Name_button.Enabled = false;
                Stop_Scheduling_Button.Enabled = true;
                #endregion

                Set_When_groupBox.ForeColor = Color.Red;
                Set_How_Long_groupBox1.ForeColor = Color.Blue;
                Start_Schedule_Recording_GroupBox.ForeColor = Color.Blue;
            }
        }
        private void Stop_Scheduling_Button_Click(object sender, EventArgs e)
        {
            Schedule_Recording_timer.Stop();
            Schedule_Is_Recording_timer.Stop();

            Is_Scheduling_CheckBox.Checked = false;

            #region Enable Unneccesary Controls
            Set_When_groupBox.Enabled = true;
            Set_How_Long_groupBox1.Enabled = true;
            Set_Name_groupBox1.Enabled = true;
            Start_Scheduling_Button.Enabled = true;
            Stop_Scheduling_Button.Enabled = true;
            Select_Name_button.Enabled = true; Select_Name_button.Text = "Select";
            Name_textBox.Enabled = true;
            #endregion

            Scheduling_Hour_textbox1.Text = "Hour";
            Scheduling_Minute_textBox1.Text = "Min";
            Recording_Length_Hour_textBox.Text = "Hour";
            Recording_Length_Minute_textBox.Text = "Min";
            Recording_Length_Second_textbox.Text = "Sec";
        }

    }
}
