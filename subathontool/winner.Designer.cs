namespace subathontool
{
    partial class winner
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.username = new System.Windows.Forms.Label();
            this.visibilityTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(1902, 999);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://www.themadgamers.co.uk/twitchtools/subathontool/giveaway/giveaway.html", System.UriKind.Absolute);
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 9000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Yoster Island", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.Location = new System.Drawing.Point(614, 213);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(352, 96);
            this.username.TabIndex = 1;
            this.username.Text = "label1";
            // 
            // visibilityTimer
            // 
            this.visibilityTimer.Interval = 50;
            this.visibilityTimer.Tick += new System.EventHandler(this.visibilityTimer_Tick);
            // 
            // winner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 999);
            this.Controls.Add(this.username);
            this.Controls.Add(this.webBrowser1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1918, 1038);
            this.Name = "winner";
            this.Text = "winner";
            this.Load += new System.EventHandler(this.winner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label username;
        private System.Windows.Forms.Timer visibilityTimer;
    }
}