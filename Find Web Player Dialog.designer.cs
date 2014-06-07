namespace Player
{
    partial class Find_Web_Player_Dialog
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
            this.Web_Player_Select_Button = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.Web_Browser_Back_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Web_Player_Select_Button
            // 
            this.Web_Player_Select_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Web_Player_Select_Button.Location = new System.Drawing.Point(403, 0);
            this.Web_Player_Select_Button.Name = "Web_Player_Select_Button";
            this.Web_Player_Select_Button.Size = new System.Drawing.Size(220, 44);
            this.Web_Player_Select_Button.TabIndex = 1;
            this.Web_Player_Select_Button.Text = "Select Web Player Website";
            this.Web_Player_Select_Button.UseVisualStyleBackColor = true;
            this.Web_Player_Select_Button.Click += new System.EventHandler(this.Web_Player_Select_Button_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 50);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(958, 564);
            this.webBrowser1.TabIndex = 2;
            // 
            // Web_Browser_Back_Button
            // 
            this.Web_Browser_Back_Button.Location = new System.Drawing.Point(0, 21);
            this.Web_Browser_Back_Button.Name = "Web_Browser_Back_Button";
            this.Web_Browser_Back_Button.Size = new System.Drawing.Size(75, 23);
            this.Web_Browser_Back_Button.TabIndex = 3;
            this.Web_Browser_Back_Button.Text = "Back";
            this.Web_Browser_Back_Button.UseVisualStyleBackColor = true;
            this.Web_Browser_Back_Button.Click += new System.EventHandler(this.Web_Browser_Back_Button_Click);
            // 
            // Find_Web_Player_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 615);
            this.ControlBox = false;
            this.Controls.Add(this.Web_Browser_Back_Button);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.Web_Player_Select_Button);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(970, 649);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(970, 649);
            this.Name = "Find_Web_Player_Dialog";
            this.Opacity = 0.95;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Find Web Player Dialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Find_Web_Player_Dialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser webBrowser1;
        public System.Windows.Forms.Button Web_Player_Select_Button;
        private System.Windows.Forms.Button Web_Browser_Back_Button;

    }
}