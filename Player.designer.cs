namespace Player
{
    partial class Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            this.Windows_Media_Player_Tab = new System.Windows.Forms.TabPage();
            this.Web_Player_Tab = new System.Windows.Forms.TabPage();
            this.address = new System.Windows.Forms.WebBrowser();
            this.Player_tabControl = new System.Windows.Forms.TabControl();
            this.Web_Location_Error_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.WindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.Windows_Media_Player_Tab.SuspendLayout();
            this.Web_Player_Tab.SuspendLayout();
            this.Player_tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // Windows_Media_Player_Tab
            // 
            this.Windows_Media_Player_Tab.Controls.Add(this.WindowsMediaPlayer);
            this.Windows_Media_Player_Tab.Location = new System.Drawing.Point(4, 22);
            this.Windows_Media_Player_Tab.Name = "Windows_Media_Player_Tab";
            this.Windows_Media_Player_Tab.Padding = new System.Windows.Forms.Padding(3);
            this.Windows_Media_Player_Tab.Size = new System.Drawing.Size(676, 427);
            this.Windows_Media_Player_Tab.TabIndex = 1;
            this.Windows_Media_Player_Tab.Text = "Windows Media Player";
            this.Windows_Media_Player_Tab.UseVisualStyleBackColor = true;
            // 
            // Web_Player_Tab
            // 
            this.Web_Player_Tab.Controls.Add(this.address);
            this.Web_Player_Tab.Location = new System.Drawing.Point(4, 22);
            this.Web_Player_Tab.Name = "Web_Player_Tab";
            this.Web_Player_Tab.Padding = new System.Windows.Forms.Padding(3);
            this.Web_Player_Tab.Size = new System.Drawing.Size(676, 427);
            this.Web_Player_Tab.TabIndex = 0;
            this.Web_Player_Tab.Text = "Web Player";
            this.Web_Player_Tab.UseVisualStyleBackColor = true;
            // 
            // address
            // 
            this.address.Dock = System.Windows.Forms.DockStyle.Fill;
            this.address.Location = new System.Drawing.Point(3, 3);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(670, 421);
            this.address.TabIndex = 1;
            this.address.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.address_DocumentCompleted);
            // 
            // Player_tabControl
            // 
            this.Player_tabControl.Controls.Add(this.Web_Player_Tab);
            this.Player_tabControl.Controls.Add(this.Windows_Media_Player_Tab);
            this.Player_tabControl.Location = new System.Drawing.Point(0, 0);
            this.Player_tabControl.Name = "Player_tabControl";
            this.Player_tabControl.SelectedIndex = 0;
            this.Player_tabControl.Size = new System.Drawing.Size(684, 453);
            this.Player_tabControl.TabIndex = 1;
            // 
            // Web_Location_Error_ToolTip
            // 
            this.Web_Location_Error_ToolTip.AutomaticDelay = 5;
            this.Web_Location_Error_ToolTip.AutoPopDelay = 15000;
            this.Web_Location_Error_ToolTip.InitialDelay = 5;
            this.Web_Location_Error_ToolTip.IsBalloon = true;
            this.Web_Location_Error_ToolTip.ReshowDelay = 1;
            this.Web_Location_Error_ToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.Web_Location_Error_ToolTip.ToolTipTitle = "Invalid Media Location";
            // 
            // WindowsMediaPlayer
            // 
            this.WindowsMediaPlayer.Enabled = true;
            this.WindowsMediaPlayer.Location = new System.Drawing.Point(-4, 1);
            this.WindowsMediaPlayer.Name = "WindowsMediaPlayer";
            this.WindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WindowsMediaPlayer.OcxState")));
            this.WindowsMediaPlayer.Size = new System.Drawing.Size(681, 430);
            this.WindowsMediaPlayer.TabIndex = 2;
            this.WindowsMediaPlayer.EndOfStream += new AxWMPLib._WMPOCXEvents_EndOfStreamEventHandler(this.WindowsMediaPlayer_EndOfStream);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 453);
            this.ControlBox = false;
            this.Controls.Add(this.Player_tabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(698, 491);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(698, 491);
            this.Name = "Player";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Player";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Player_Load);
            this.Windows_Media_Player_Tab.ResumeLayout(false);
            this.Web_Player_Tab.ResumeLayout(false);
            this.Player_tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser address;
        public AxWMPLib.AxWindowsMediaPlayer WindowsMediaPlayer;
        private System.Windows.Forms.ToolTip Web_Location_Error_ToolTip;
        public System.Windows.Forms.TabPage Windows_Media_Player_Tab;
        public System.Windows.Forms.TabPage Web_Player_Tab;
        public System.Windows.Forms.TabControl Player_tabControl;
    }
}