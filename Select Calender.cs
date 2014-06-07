using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Select_Calender : Form
    {
        #region Variable Field
            Schedule_Recording_Dialog schedule_recording_dialog;
        #endregion

        public Select_Calender(Schedule_Recording_Dialog schedule_recording_dialog)
        {
            InitializeComponent();

            this.schedule_recording_dialog = schedule_recording_dialog;
        }

        private void Choose_Date_button_Click(object sender, EventArgs e)
        {
            schedule_recording_dialog.Date_Label.Text = monthCalendar.SelectionStart.ToShortDateString();
            schedule_recording_dialog.scheduled_date = monthCalendar.SelectionStart;
            this.Close();
        }
    }
}
