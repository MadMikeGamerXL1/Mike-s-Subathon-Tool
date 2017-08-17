namespace subathontool
{
    partial class activationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(activationWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.invalidCodeLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.activationCode = new System.Windows.Forms.TextBox();
            this.activateBtn = new System.Windows.Forms.Button();
            this.cancelActivationBtn = new System.Windows.Forms.Button();
            this.bgLbl = new System.Windows.Forms.Label();
            this.borderbottom = new System.Windows.Forms.Label();
            this.borderRight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.discordLink = new System.Windows.Forms.LinkLabel();
            this.mailLink = new System.Windows.Forms.LinkLabel();
            this.twitterLink = new System.Windows.Forms.LinkLabel();
            this.knowledgeLink = new System.Windows.Forms.LinkLabel();
            this.purchasePct = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.purchasePct)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkViolet;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(433, 85);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseUp);
            // 
            // invalidCodeLbl
            // 
            this.invalidCodeLbl.BackColor = System.Drawing.Color.Transparent;
            this.invalidCodeLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invalidCodeLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.invalidCodeLbl.Location = new System.Drawing.Point(12, 103);
            this.invalidCodeLbl.Name = "invalidCodeLbl";
            this.invalidCodeLbl.Size = new System.Drawing.Size(406, 62);
            this.invalidCodeLbl.TabIndex = 1;
            this.invalidCodeLbl.Text = "The Activation Code you entered was not valid! Please try again.";
            this.invalidCodeLbl.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(105, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(80, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Activation Code:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(179, 178);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(149, 20);
            this.username.TabIndex = 4;
            // 
            // activationCode
            // 
            this.activationCode.Location = new System.Drawing.Point(179, 225);
            this.activationCode.Name = "activationCode";
            this.activationCode.Size = new System.Drawing.Size(149, 20);
            this.activationCode.TabIndex = 5;
            this.activationCode.UseSystemPasswordChar = true;
            // 
            // activateBtn
            // 
            this.activateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.activateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.activateBtn.Location = new System.Drawing.Point(349, 477);
            this.activateBtn.Name = "activateBtn";
            this.activateBtn.Size = new System.Drawing.Size(75, 23);
            this.activateBtn.TabIndex = 6;
            this.activateBtn.Text = "Activate";
            this.activateBtn.UseVisualStyleBackColor = false;
            this.activateBtn.Click += new System.EventHandler(this.activateBtn_Click);
            // 
            // cancelActivationBtn
            // 
            this.cancelActivationBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cancelActivationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelActivationBtn.Location = new System.Drawing.Point(268, 477);
            this.cancelActivationBtn.Name = "cancelActivationBtn";
            this.cancelActivationBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelActivationBtn.TabIndex = 7;
            this.cancelActivationBtn.Text = "Cancel";
            this.cancelActivationBtn.UseVisualStyleBackColor = false;
            this.cancelActivationBtn.Click += new System.EventHandler(this.cancelActivationBtn_Click);
            // 
            // bgLbl
            // 
            this.bgLbl.BackColor = System.Drawing.Color.Thistle;
            this.bgLbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bgLbl.Location = new System.Drawing.Point(-1, 85);
            this.bgLbl.Name = "bgLbl";
            this.bgLbl.Size = new System.Drawing.Size(433, 427);
            this.bgLbl.TabIndex = 0;
            this.bgLbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseDown);
            this.bgLbl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseMove);
            this.bgLbl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseUp);
            // 
            // borderbottom
            // 
            this.borderbottom.BackColor = System.Drawing.Color.Indigo;
            this.borderbottom.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.borderbottom.Location = new System.Drawing.Point(-10, 506);
            this.borderbottom.Name = "borderbottom";
            this.borderbottom.Size = new System.Drawing.Size(455, 26);
            this.borderbottom.TabIndex = 0;
            // 
            // borderRight
            // 
            this.borderRight.BackColor = System.Drawing.Color.Indigo;
            this.borderRight.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.borderRight.Location = new System.Drawing.Point(428, 85);
            this.borderRight.Name = "borderRight";
            this.borderRight.Size = new System.Drawing.Size(10, 427);
            this.borderRight.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Indigo;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(-6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 421);
            this.label4.TabIndex = 0;
            // 
            // discordLink
            // 
            this.discordLink.AutoSize = true;
            this.discordLink.BackColor = System.Drawing.Color.Transparent;
            this.discordLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discordLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.discordLink.Location = new System.Drawing.Point(5, 490);
            this.discordLink.Name = "discordLink";
            this.discordLink.Size = new System.Drawing.Size(56, 13);
            this.discordLink.TabIndex = 8;
            this.discordLink.TabStop = true;
            this.discordLink.Text = "Discord  |  ";
            // 
            // mailLink
            // 
            this.mailLink.ActiveLinkColor = System.Drawing.Color.DarkMagenta;
            this.mailLink.AutoSize = true;
            this.mailLink.BackColor = System.Drawing.Color.Transparent;
            this.mailLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mailLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mailLink.Location = new System.Drawing.Point(52, 490);
            this.mailLink.Name = "mailLink";
            this.mailLink.Size = new System.Drawing.Size(46, 13);
            this.mailLink.TabIndex = 9;
            this.mailLink.TabStop = true;
            this.mailLink.Text = "Email  |  ";
            this.mailLink.VisitedLinkColor = System.Drawing.Color.White;
            // 
            // twitterLink
            // 
            this.twitterLink.AutoSize = true;
            this.twitterLink.BackColor = System.Drawing.Color.Transparent;
            this.twitterLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twitterLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.twitterLink.Location = new System.Drawing.Point(177, 490);
            this.twitterLink.Name = "twitterLink";
            this.twitterLink.Size = new System.Drawing.Size(46, 13);
            this.twitterLink.TabIndex = 11;
            this.twitterLink.TabStop = true;
            this.twitterLink.Text = "  Twitter ";
            // 
            // knowledgeLink
            // 
            this.knowledgeLink.AutoSize = true;
            this.knowledgeLink.BackColor = System.Drawing.Color.Transparent;
            this.knowledgeLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.knowledgeLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.knowledgeLink.Location = new System.Drawing.Point(90, 490);
            this.knowledgeLink.Name = "knowledgeLink";
            this.knowledgeLink.Size = new System.Drawing.Size(100, 13);
            this.knowledgeLink.TabIndex = 10;
            this.knowledgeLink.TabStop = true;
            this.knowledgeLink.Text = "Knowledgebase  |   ";
            // 
            // purchasePct
            // 
            this.purchasePct.Image = global::subathontool.Properties.Resources.purchase1;
            this.purchasePct.Location = new System.Drawing.Point(179, 251);
            this.purchasePct.Name = "purchasePct";
            this.purchasePct.Size = new System.Drawing.Size(148, 46);
            this.purchasePct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.purchasePct.TabIndex = 12;
            this.purchasePct.TabStop = false;
            this.purchasePct.MouseClick += new System.Windows.Forms.MouseEventHandler(this.purchasePct_MouseClick);
            // 
            // activationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(432, 512);
            this.Controls.Add(this.purchasePct);
            this.Controls.Add(this.twitterLink);
            this.Controls.Add(this.knowledgeLink);
            this.Controls.Add(this.mailLink);
            this.Controls.Add(this.discordLink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.borderbottom);
            this.Controls.Add(this.borderRight);
            this.Controls.Add(this.cancelActivationBtn);
            this.Controls.Add(this.activateBtn);
            this.Controls.Add(this.activationCode);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.invalidCodeLbl);
            this.Controls.Add(this.bgLbl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "activationWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "activation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.activationWindow_Load);
            this.Shown += new System.EventHandler(this.activationWindow_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.activationWindow_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.purchasePct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelActivationBtn;
        public System.Windows.Forms.Label invalidCodeLbl;
        public System.Windows.Forms.Button activateBtn;
        public System.Windows.Forms.TextBox username;
        public System.Windows.Forms.TextBox activationCode;
        private System.Windows.Forms.Label bgLbl;
        private System.Windows.Forms.Label borderbottom;
        private System.Windows.Forms.Label borderRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel discordLink;
        private System.Windows.Forms.LinkLabel mailLink;
        private System.Windows.Forms.LinkLabel twitterLink;
        private System.Windows.Forms.LinkLabel knowledgeLink;
        private System.Windows.Forms.PictureBox purchasePct;
    }
}