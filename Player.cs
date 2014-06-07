using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Player : Form
    {
        #region Variable Field
            My_Player my_player;
        #endregion

        public Player(My_Player my_player)
        {
            InitializeComponent();

            this.my_player = my_player;
        }

        private void Player_Load(object sender, EventArgs e)
        {
           
        }

        private void address_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (address.DocumentTitle == "Navigation Canceled" || address.DocumentTitle == "Internet Explorer cannot display the webpage")
            {
                Web_Location_Error_ToolTip.Show("This media's location:\n" + "  1. Is incorrect\n" + "  2. Does not exist anymore\n" + "Remove this media from the playlist and add it again", this.my_player.Web_Name_ComboBox);
                Web_Location_Error_ToolTip.Show("This media's location:\n" + "  1. Is incorrect\n" + "  2. Does not exist anymore\n" + "Remove this media from the playlist and add it again", this.my_player.Web_Name_ComboBox);
            } 
        }

        private void WindowsMediaPlayer_EndOfStream(object sender, AxWMPLib._WMPOCXEvents_EndOfStreamEvent e)
        {
            my_player.W_M_NowPlaying_Label.Text = "Nothing";
        }
    }
}
