using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Converting_Progress : Form
    {
        public Boolean is_scheduled;
        public Label conversionComplete_label;
        public Button conversionComplete_button;
        public string conversionDirectory;
        public string schedFileName;

        public void showConfirmation()
        {
            Conversion_Progressbar.Hide();
            convertingIndicator_label.Hide();

            //Textbox
            conversionComplete_label = new Label();
            conversionComplete_label.Location = new Point(8, 20);
            conversionComplete_label.AutoSize = true;
            conversionComplete_label.TextAlign = ContentAlignment.MiddleCenter;
            if (is_scheduled)
            {
                conversionComplete_label.Text = "The '" + schedFileName + "' Has Completed! \n     Click 'OK' to open file location!";
            }
            else
            {
                conversionComplete_label.Text = "        The Conversion Has Completed!";
            }
            conversionComplete_label.Show();
            this.Controls.Add(conversionComplete_label);

            //Button
            conversionComplete_button = new Button(); conversionComplete_button.Click += new EventHandler(conversionComplete_button_Click);
            conversionComplete_button.Location = new Point(74, 65);
            conversionComplete_button.Text = "OK";
            conversionComplete_button.Show();
            this.Controls.Add(conversionComplete_button);

        }
        public void openDirectory()
        {
            #region Open Created File

            Process proc = new Process();
            proc.StartInfo.FileName = conversionDirectory.ToString();
            proc.Start();

            #endregion
        }
        void conversionComplete_button_Click(object sender, EventArgs e)
        {
            if (is_scheduled == true)
            {
                openDirectory();
            }
            this.Dispose();
        }

        public Converting_Progress()
        {
            InitializeComponent();
            schedFileName = "Conversion Process";
        }


    }
}
