namespace subathontool
{
    partial class about
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
            this.aboutLogo = new System.Windows.Forms.PictureBox();
            this.softwareTitle = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.releasedateLbl = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.aboutLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutLogo
            // 
            this.aboutLogo.BackColor = System.Drawing.SystemColors.Control;
            this.aboutLogo.Image = global::subathontool.Properties.Resources.the_mad_gamers_logo1;
            this.aboutLogo.Location = new System.Drawing.Point(12, 12);
            this.aboutLogo.Name = "aboutLogo";
            this.aboutLogo.Size = new System.Drawing.Size(75, 75);
            this.aboutLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.aboutLogo.TabIndex = 0;
            this.aboutLogo.TabStop = false;
            // 
            // softwareTitle
            // 
            this.softwareTitle.AutoSize = true;
            this.softwareTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.softwareTitle.Location = new System.Drawing.Point(93, 12);
            this.softwareTitle.Name = "softwareTitle";
            this.softwareTitle.Size = new System.Drawing.Size(217, 21);
            this.softwareTitle.TabIndex = 1;
            this.softwareTitle.Text = "Mike\'s Subathon Tool 2016\r\n";
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.Location = new System.Drawing.Point(94, 47);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(45, 13);
            this.versionLbl.TabIndex = 2;
            this.versionLbl.Text = "Version:";
            // 
            // releasedateLbl
            // 
            this.releasedateLbl.AutoSize = true;
            this.releasedateLbl.Location = new System.Drawing.Point(94, 74);
            this.releasedateLbl.Name = "releasedateLbl";
            this.releasedateLbl.Size = new System.Drawing.Size(213, 26);
            this.releasedateLbl.TabIndex = 3;
            this.releasedateLbl.Text = "Release Date: Beta Release [14 ‎April ‎2016]\r\nConfirmed Release: 27th May 2017\r\n";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.AutoSize = true;
            this.copyrightLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrightLabel.Location = new System.Drawing.Point(71, 113);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(281, 16);
            this.copyrightLabel.TabIndex = 4;
            this.copyrightLabel.Text = "Copyright © 2017 The Mad Gamers. All Rights Reserved\r\n";
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 138);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.releasedateLbl);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.softwareTitle);
            this.Controls.Add(this.aboutLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "about";
            this.Text = "About Mike\'s Subathon Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.about_FormClosing);
            this.Load += new System.EventHandler(this.about_Load);
            ((System.ComponentModel.ISupportInitialize)(this.aboutLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox aboutLogo;
        private System.Windows.Forms.Label softwareTitle;
        private System.Windows.Forms.Label versionLbl;
        private System.Windows.Forms.Label releasedateLbl;
        private System.Windows.Forms.Label copyrightLabel;
    }
}