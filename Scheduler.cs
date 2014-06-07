using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Scheduler : Form
    {
        #region Variable Field
            My_Player my_player;

            public Schedule_Recording_Dialog s_c_d1;
            public Schedule_Recording_Dialog s_c_d2;
            public Schedule_Recording_Dialog s_c_d3;
            public Schedule_Recording_Dialog s_c_d4;
            public Schedule_Recording_Dialog s_c_d5;
            public Schedule_Recording_Dialog s_c_d6;
        #endregion

        public Scheduler(My_Player my_player)
        {
            InitializeComponent();

            this.my_player = my_player;
        }

        private void Enable_Sched_1_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_1.CheckState)
            {
                case CheckState.Checked:
                    s_c_d1 = new Schedule_Recording_Dialog(this.my_player, 1);
                    s_c_d1.Show();
                    Show_Sched_1.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d1.Close();
                    Show_Sched_1.Checked = false;
                    break;
                default:
                    break;
            }
        }
        private void Enable_Sched_2_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_2.CheckState)
            {
                case CheckState.Checked:
                    s_c_d2 = new Schedule_Recording_Dialog(this.my_player, 2);
                    s_c_d2.Show();
                    Show_Sched_2.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d2.Close();
                    Show_Sched_2.Checked = false;
                    break;
                default:
                    break;
            }
        }
        private void Enable_Sched_3_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_3.CheckState)
            {
                case CheckState.Checked:
                    s_c_d3 = new Schedule_Recording_Dialog(this.my_player, 3);
                    s_c_d3.Show();
                    Show_Sched_3.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d3.Close();
                    Show_Sched_3.Checked = false;
                    break;
                default:
                    break;
            }
        }
        private void Enable_Sched_4_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_4.CheckState)
            {
                case CheckState.Checked:
                    s_c_d4 = new Schedule_Recording_Dialog(this.my_player, 4);
                    s_c_d4.Show();
                    Show_Sched_4.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d4.Close();
                    Show_Sched_4.Checked = false;
                    break;
                default:
                    break;
            }
        }
        private void Enable_Sched_5_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_5.CheckState)
            {
                case CheckState.Checked:
                    s_c_d5 = new Schedule_Recording_Dialog(this.my_player, 5);
                    s_c_d5.Show();
                    Show_Sched_5.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d5.Close();
                    Show_Sched_5.Checked = false;
                    break;
                default:
                    break;
            }
        }
        private void Enable_Sched_6_CheckedChanged(object sender, EventArgs e)
        {
            switch (Enable_Sched_6.CheckState)
            {
                case CheckState.Checked:
                    s_c_d6 = new Schedule_Recording_Dialog(this.my_player, 6);
                    s_c_d6.Show();
                    Show_Sched_6.Checked = true;
                    break;
                case CheckState.Indeterminate:
                    break;
                case CheckState.Unchecked:
                    s_c_d6.Close();
                    Show_Sched_6.Checked = false;
                    break;
                default:
                    break;
            }
        }

        private void Show_Sched_1_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_1.Checked)
            {
                switch (Show_Sched_1.Checked)
                {
                    case true:
                        s_c_d1.Show();
                        break;

                    case false:
                        s_c_d1.Hide();
                        break;

                    default:
                        break;
                } 
            }
            if (s_c_d1 == null || !Enable_Sched_1.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_1);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_1);
            }
        }
        private void Show_Sched_2_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_2.Checked)
            {
                switch (Show_Sched_2.Checked)
                {
                    case true:
                        s_c_d2.Show();
                        break;

                    case false:
                        s_c_d2.Hide();
                        break;

                    default:
                        break;
                } 
            }
            if (s_c_d2 == null || !Enable_Sched_2.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_2);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_2);
            }
        }
        private void Show_Sched_3_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_3.Checked)
            {
                switch (Show_Sched_3.Checked)
                {
                    case true:
                        s_c_d3.Show();
                        break;

                    case false:
                        s_c_d3.Hide();
                        break;

                    default:
                        break;
                } 
            }
            if (s_c_d3 == null || !Enable_Sched_3.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_3);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_3);
            }
        }
        private void Show_Sched_4_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_4.Checked)
            {
                switch (Show_Sched_4.Checked)
                {
                    case true:
                        s_c_d4.Show();
                        break;

                    case false:
                        s_c_d4.Hide();
                        break;

                    default:
                        break;
                }
            }
            if (s_c_d4 == null || !Enable_Sched_4.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_4);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_4);
            }
        }
        private void Show_Sched_5_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_5.Checked)
            {
                switch (Show_Sched_5.Checked)
                {
                    case true:
                        s_c_d5.Show();
                        break;

                    case false:
                        s_c_d5.Hide();
                        break;

                    default:
                        break;
                }
            }
            if (s_c_d5 == null || !Enable_Sched_5.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_5);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_5);
            }
        }
        private void Show_Sched_6_CheckedChanged(object sender, EventArgs e)
        {
            if (Enable_Sched_6.Checked)
            {
                switch (Show_Sched_6.Checked)
                {
                    case true:
                        s_c_d6.Show();
                        break;

                    case false:
                        s_c_d6.Hide();
                        break;

                    default:
                        break;
                }
            }
            if (s_c_d6 == null || !Enable_Sched_6.Checked)
            {
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_6);
                toolTip1.Show("You Must First Enable the Schedule\n" + "\n" + "Click This Check Box to Enable", Enable_Sched_6);
            }
        }

        private void Hide_Schedule_Button_Click(object sender, EventArgs e)
        {
            if (s_c_d1 != null)
            {
                s_c_d1.Hide(); 
            }
            if (s_c_d2 != null)
            {
                s_c_d2.Hide(); 
            }
            if (s_c_d3 != null)
            {
                s_c_d3.Hide(); 
            }
            if (s_c_d4 != null)
            {
                s_c_d4.Hide();
            }
            if (s_c_d5 != null)
            {
                s_c_d5.Hide();
            }
            if (s_c_d6 != null)
            {
                s_c_d6.Hide();
            }
        }
        private void Show_Schedule_Button_Click(object sender, EventArgs e)
        {
            if (s_c_d1 != null && Show_Sched_1.Checked)
            {
                Show_Sched_1.PerformClick();
            }
            if (s_c_d2 != null && Show_Sched_2.Checked)
            {
                Show_Sched_2.PerformClick();
            }
            if (s_c_d3 != null && Show_Sched_3.Checked)
            {
                Show_Sched_3.PerformClick();
            }
            if (s_c_d4 != null && Show_Sched_4.Checked)
            {
                Show_Sched_4.PerformClick();
            }
            if (s_c_d5 != null && Show_Sched_5.Checked)
            {
                Show_Sched_5.PerformClick();
            }
            if (s_c_d6 != null && Show_Sched_6.Checked)
            {
                Show_Sched_6.PerformClick();
            }
        }

        private void Show_Sched_1_Click(object sender, EventArgs e)
        {
            if (this.s_c_d1 != null)
            {
                if (Show_Sched_1.Checked)
                {
                    this.s_c_d1.Show();
                }
            }
        }
        private void Show_Sched_2_Click(object sender, EventArgs e)
        {
            if (this.s_c_d2 != null)
            {
                if (Show_Sched_2.Checked)
                {
                    this.s_c_d2.Show();
                }
            }
        }
        private void Show_Sched_3_Click(object sender, EventArgs e)
        {
            if (this.s_c_d3 != null)
            {
                if (Show_Sched_3.Checked)
                {
                    this.s_c_d3.Show();
                } 
            }
        }
        private void Show_Sched_4_Click(object sender, EventArgs e)
        {
            if (this.s_c_d4 != null)
            {
                if (Show_Sched_4.Checked)
                {
                    this.s_c_d4.Show();
                }
            }
        }
        private void Show_Sched_5_Click(object sender, EventArgs e)
        {
            if (this.s_c_d5 != null)
            {
                if (Show_Sched_5.Checked)
                {
                    this.s_c_d5.Show();
                }
            }
        }
        private void Show_Sched_6_Click(object sender, EventArgs e)
        {
            if (this.s_c_d6 != null)
            {
                if (Show_Sched_6.Checked)
                {
                    this.s_c_d6.Show();
                }
            }
        }
    }
}
