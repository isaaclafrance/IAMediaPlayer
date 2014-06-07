using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Edit_Main_Button_Dialog : Form
    {
        #region Variable Field
            My_Player my_player;
            Find_Web_Player_Dialog find_web_player_dialog;
            int main_button_num;
        #endregion

        public Edit_Main_Button_Dialog(My_Player my_player, int main_button_num)
        {
            InitializeComponent();

            this.my_player = my_player;
            this.main_button_num = main_button_num;
        }

        private void Edit_Main_Browse_Button_1_Click(object sender, EventArgs e)
        {
            Uri home;
            UriBuilder uri_builder = new UriBuilder("www.google.com");
            home = uri_builder.Uri;

            find_web_player_dialog = new Find_Web_Player_Dialog(this);
            find_web_player_dialog.Show();
            find_web_player_dialog.webBrowser1.Url = home;
        }
        private void Edit_Main_Save_Button_1_Click(object sender, EventArgs e)
        {
            switch (main_button_num)
            {
                case 1:

                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton1_Name.ToLower());

                    my_player.full_MainButton1_Name = Edit_Main_Name_TextBox_1.Text.ToUpper();
                    if (my_player.full_MainButton1_Name.Length >= 8)
                    {
                        my_player.Main_Button_1.Text = new string(my_player.full_MainButton1_Name.ToCharArray(0, 7));
                        my_player.Main_Button_1.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_1.Text = my_player.full_MainButton1_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_1.Text.ToUpper(), Edit_Main_Location_TextBox_1.Text+"&web");
                    break;

                case 2:
                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton2_Name.ToLower());

                    my_player.full_MainButton2_Name = Edit_Main_Name_TextBox_1.Text.ToUpper();
                    if (my_player.full_MainButton2_Name.Length >= 8)
                    {
                        my_player.Main_Button_2.Text = new string(my_player.full_MainButton2_Name.ToCharArray(0, 7));
                        my_player.Main_Button_2.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_2.Text = my_player.full_MainButton2_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_1.Text.ToUpper(), Edit_Main_Location_TextBox_1.Text+"&web");
                    break;

                case 3:
                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton3_Name.ToLower());

                    my_player.full_MainButton3_Name = Edit_Main_Name_TextBox_1.Text.ToUpper();
                    if (my_player.full_MainButton3_Name.Length >= 8)
                    {
                        my_player.Main_Button_3.Text = new string(my_player.full_MainButton3_Name.ToCharArray(0, 7));
                        my_player.Main_Button_3.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_3.Text = my_player.full_MainButton3_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_1.Text.ToUpper(), Edit_Main_Location_TextBox_1.Text+"&web");
                    break;

                default:
                    break;
            }

            this.Dispose();
        }

        private void Edit_Main_Browse_Button_2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void Edit_Main_Save_Button_2_Click(object sender, EventArgs e)
        {
            switch (main_button_num)
            {
                case 1:
                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton1_Name.ToLower());

                    my_player.full_MainButton3_Name = Edit_Main_Name_TextBox_2.Text.ToUpper();
                    if (my_player.full_MainButton1_Name.Length >= 8)
                    {
                        my_player.Main_Button_1.Text = new string(my_player.full_MainButton1_Name.ToCharArray(0, 7));
                        my_player.Main_Button_1.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_1.Text = my_player.full_MainButton1_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_2.Text.ToUpper(), Edit_Main_Location_TextBox_2.Text+"&windows media");
                    break;

                case 2:
                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton2_Name.ToLower());

                    my_player.full_MainButton3_Name = Edit_Main_Name_TextBox_2.Text.ToUpper();
                    if (my_player.full_MainButton2_Name.Length >= 8)
                    {
                        my_player.Main_Button_2.Text = new string(my_player.full_MainButton2_Name.ToCharArray(0, 7));
                        my_player.Main_Button_2.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_2.Text = my_player.full_MainButton2_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_2.Text.ToUpper(), Edit_Main_Location_TextBox_2.Text+"&windows media");
                    break;

                case 3:
                    my_player.Main_Button_Playlist_Dictionary.Remove(my_player.full_MainButton3_Name.ToLower());

                    my_player.full_MainButton3_Name = Edit_Main_Name_TextBox_2.Text.ToUpper();
                    if (my_player.full_MainButton3_Name.Length >= 8)
                    {
                        my_player.Main_Button_3.Text = new string(my_player.full_MainButton3_Name.ToCharArray(0, 7));
                        my_player.Main_Button_3.Text += " ...";
                    }
                    else
                    {
                        my_player.Main_Button_3.Text = my_player.full_MainButton3_Name;
                    }

                    my_player.Main_Button_Playlist_Dictionary.Add(Edit_Main_Name_TextBox_2.Text.ToUpper(), Edit_Main_Location_TextBox_2.Text+"&windows media");
                    break;

                default:
                    break;
            }

            this.Dispose();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
              Edit_Main_Location_TextBox_2.Text = openFileDialog1.FileName;          
        }

    }
}
