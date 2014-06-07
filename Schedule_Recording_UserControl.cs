using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Alvas.Audio;

namespace Player
{
    public partial class Schedule_Recording_UserControl : UserControl
    {
        #region Variable Field
            My_Player my_player;
            DateTime scheduled_time;
            bool has_recorded;
        #endregion

        public Schedule_Recording_UserControl(My_Player my_player)
        {
            InitializeComponent();

            this.my_player = my_player;
        }

        private void Schedule_Recording_timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= scheduled_time && has_recorded == false)
            {
                Stop_Scheduling_Button.PerformClick();
                my_player.Record_with_Timer_Button_Click(null, null);

                has_recorded = true;
            }

            if (DateTime.Now >= scheduled_time && has_recorded == true)
            {
                if (my_player.recorder.State == DeviceState.Closed)
                {
                    my_player.Save_Recording_Button_Click(null, null);
                    Schedule_Recording_timer.Stop();
                }
            }
        }

        private void Start_Scheduling_Button_Click(object sender, EventArgs e)
        {
            int hour = 0;
            int minute = 0;

            #region
                if (Scheduling_Hour_TextBox.Text == "Hour")
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Hour_TextBox);
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Hour_TextBox);
                }
                else { hour = Int32.Parse(Scheduling_Hour_TextBox.Text); }

                if (Scheduling_Minute_TextBox.Text == "Min")
                {
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Minute_TextBox);
                    my_player.Schedule_Recording_toolTip.Show("Please Set at What Time You Want Recording to Happen", Scheduling_Minute_TextBox);
                }
                else { minute = Int32.Parse(Scheduling_Minute_TextBox.Text); }

                if (Time_of_Day_comboBox.Text == "AM")
                {
                    scheduled_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
                }
                if (Time_of_Day_comboBox.Text == "PM")
                {
                    scheduled_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour + 12, minute, 0);
                } 
            #endregion
            if (Scheduling_Hour_TextBox.Text != "Hour" && Scheduling_Minute_TextBox.Text != "Min")
            {
                Schedule_Recording_timer.Start();

                Is_Scheduling_CheckBox.Checked = true;

                #region Disable Unneccesary Controls
                Scheduling_Hour_TextBox.Enabled = false;
                Scheduling_Minute_TextBox.Enabled = false;
                Time_of_Day_comboBox.Enabled = false;
                Start_Scheduling_Button.Enabled = false;
                Stop_Scheduling_Button.Enabled = true;
                Schedule_Recording_checkBox.Enabled = true;
                #endregion  
            }
            
        }
        private void Stop_Scheduling_Button_Click(object sender, EventArgs e)
        {
            Schedule_Recording_timer.Stop();

            Is_Scheduling_CheckBox.Checked = false;

            #region Enable Unneccesary Controls
            Scheduling_Hour_TextBox.Enabled = true;
            Scheduling_Minute_TextBox.Enabled = true;
            Time_of_Day_comboBox.Enabled = true;
            Start_Scheduling_Button.Enabled = true;
            Stop_Scheduling_Button.Enabled = true;
            #endregion

            Scheduling_Hour_TextBox.Text = "Hour";
            Scheduling_Minute_TextBox.Text = "Min";
        }

        private void Scheduling_Hour_TextBox_Click(object sender, EventArgs e)
        {
            Scheduling_Hour_TextBox.Text = "";
        }
        private void Scheduling_Minute_TextBox_Click(object sender, EventArgs e)
        {
            Scheduling_Minute_TextBox.Text = "";
        }
        private void Scheduling_Hour_TextBox_Leave(object sender, EventArgs e)
        {
            if (Scheduling_Hour_TextBox.Text == "")
            {
                Scheduling_Hour_TextBox.Text = "Hour"; 
            }
        }
        private void Scheduling_Minute_TextBox_Leave(object sender, EventArgs e)
        {
            if (Scheduling_Minute_TextBox.Text == "")
            {
                Scheduling_Minute_TextBox.Text = "Min"; 
            }
        }

        private void Schedule_Recording_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            switch (Schedule_Recording_checkBox.CheckState)
            {
                case CheckState.Checked:
                    this.Show();
                    my_player.Start_Recording_GroupBox.Hide();
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    this.Hide();
                    my_player.Schedule_Recording_checkBox.Checked = false;
                    my_player.Start_Recording_GroupBox.Show();
                    break;
                default:
                    break;
            }
        }
        private void Schedule_Recording_checkBox_MouseHover(object sender, EventArgs e)
        {
            my_player.Schedule_Recording_toolTip.SetToolTip(Schedule_Recording_checkBox, "Click Here to Return to Timer Interface");
            my_player.Schedule_Recording_toolTip.SetToolTip(Schedule_Recording_checkBox, "Click Here to Return to Timer Interface");
        }
    }
}
