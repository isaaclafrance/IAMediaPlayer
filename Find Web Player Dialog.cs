using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Player
{
    public partial class Find_Web_Player_Dialog : Form
    {
        #region Variable Field
            My_Player my_player;
            Edit_Main_Button_Dialog edit_main_button_dialog;
            string url;
        #endregion

        public Find_Web_Player_Dialog(My_Player my_player)
        {
            InitializeComponent();
            this.my_player = my_player;
        }
        public Find_Web_Player_Dialog(Edit_Main_Button_Dialog edit_main_button_dialog)
        {
            InitializeComponent();
            this.edit_main_button_dialog = edit_main_button_dialog;
        }

        private void Find_Web_Player_Dialog_Load(object sender, EventArgs e)
        {
            url = "";

            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WinFormBrowser_DocumentCompleted);
            webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(WinFormBrowser_NewWindow);
        }

        private void Web_Player_Select_Button_Click(object sender, EventArgs e)
        {
            if (my_player != null)
            {
                my_player.Web_Location_TextBox.Text = webBrowser1.Url.ToString(); 
            }
            if (edit_main_button_dialog != null)
            {
                edit_main_button_dialog.Edit_Main_Location_TextBox_1.Text = webBrowser1.Url.ToString();
            }
            
            Uri blank;
            UriBuilder uri_builder = new UriBuilder("about:blank");
            blank = uri_builder.Uri;

            webBrowser1.Url = blank;
            this.Hide();
        }
        private void Web_Browser_Back_Button_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack(); 
            }
        }

        void WinFormBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElementCollection links = webBrowser1.Document.Links;
            foreach (HtmlElement var in links)
            {
                var.AttachEventHandler("onclick", LinkClicked);
            }
        }
        void WinFormBrowser_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        void LinkClicked(object sender, EventArgs e)
        {
            HtmlElement link = webBrowser1.Document.ActiveElement;
            url = link.GetAttribute("href");
        }
    }
}
