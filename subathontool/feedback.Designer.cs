namespace subathontool
{
    partial class feedback
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(feedback));
            this.sendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.senderNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.senderEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.senderUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.senderReport = new System.Windows.Forms.TextBox();
            this.emailLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.Location = new System.Drawing.Point(267, 480);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 4;
            this.sendBtn.Text = "Done";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Your Name:";
            // 
            // senderNameTxt
            // 
            this.senderNameTxt.Location = new System.Drawing.Point(90, 71);
            this.senderNameTxt.Name = "senderNameTxt";
            this.senderNameTxt.Size = new System.Drawing.Size(252, 20);
            this.senderNameTxt.TabIndex = 0;
            this.senderNameTxt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.senderNameTxt_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Your Email:";
            // 
            // senderEmail
            // 
            this.senderEmail.Location = new System.Drawing.Point(90, 97);
            this.senderEmail.Name = "senderEmail";
            this.senderEmail.Size = new System.Drawing.Size(252, 20);
            this.senderEmail.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Twitch Username:";
            // 
            // senderUsername
            // 
            this.senderUsername.Location = new System.Drawing.Point(132, 123);
            this.senderUsername.Name = "senderUsername";
            this.senderUsername.Size = new System.Drawing.Size(210, 20);
            this.senderUsername.TabIndex = 2;
            this.senderUsername.MouseClick += new System.Windows.Forms.MouseEventHandler(this.senderUsername_MouseClick);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(330, 26);
            this.label5.TabIndex = 2;
            this.label5.Text = "Suggestion/Bug Report (please include as much detail as possible) :";
            // 
            // senderReport
            // 
            this.senderReport.Location = new System.Drawing.Point(24, 184);
            this.senderReport.Multiline = true;
            this.senderReport.Name = "senderReport";
            this.senderReport.Size = new System.Drawing.Size(318, 290);
            this.senderReport.TabIndex = 3;
            this.senderReport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.senderReport_MouseClick);
            // 
            // emailLink
            // 
            this.emailLink.AutoSize = true;
            this.emailLink.Location = new System.Drawing.Point(76, 100);
            this.emailLink.Name = "emailLink";
            this.emailLink.Size = new System.Drawing.Size(13, 13);
            this.emailLink.TabIndex = 5;
            this.emailLink.TabStop = true;
            this.emailLink.Text = "?";
            this.emailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLink_LinkClicked);
            // 
            // feedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 515);
            this.Controls.Add(this.emailLink);
            this.Controls.Add(this.senderReport);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.senderUsername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.senderEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.senderNameTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "feedback";
            this.Padding = new System.Windows.Forms.Padding(4, 4, 0, 0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Feedback Form";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.feedback_FormClosing);
            this.Load += new System.EventHandler(this.feedback_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox senderNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox senderEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox senderUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox senderReport;
        private System.Windows.Forms.LinkLabel emailLink;
    }
}