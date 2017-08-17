namespace subathontool
{
    partial class giveaway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(giveaway));
            this.userGiveGroup = new System.Windows.Forms.GroupBox();
            this.totalUsers = new System.Windows.Forms.Label();
            this.removeUserBtn = new System.Windows.Forms.Button();
            this.beginUserBtn = new System.Windows.Forms.Button();
            this.howToUpdate = new System.Windows.Forms.LinkLabel();
            this.currentGiveaway = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.viewJsonBtn = new System.Windows.Forms.Button();
            this.refreshList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.viewerLists = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numberGiveGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.userGuesses = new System.Windows.Forms.ListBox();
            this.generateRNG = new System.Windows.Forms.Button();
            this.hideRNG = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RNGOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.wordGiveGroup = new System.Windows.Forms.GroupBox();
            this.userGiveGroup.SuspendLayout();
            this.numberGiveGroup.SuspendLayout();
            this.wordGiveGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // userGiveGroup
            // 
            this.userGiveGroup.Controls.Add(this.totalUsers);
            this.userGiveGroup.Controls.Add(this.removeUserBtn);
            this.userGiveGroup.Controls.Add(this.beginUserBtn);
            this.userGiveGroup.Controls.Add(this.howToUpdate);
            this.userGiveGroup.Controls.Add(this.currentGiveaway);
            this.userGiveGroup.Controls.Add(this.label4);
            this.userGiveGroup.Controls.Add(this.viewJsonBtn);
            this.userGiveGroup.Controls.Add(this.refreshList);
            this.userGiveGroup.Controls.Add(this.label3);
            this.userGiveGroup.Controls.Add(this.viewerLists);
            this.userGiveGroup.Controls.Add(this.label1);
            this.userGiveGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.userGiveGroup.Location = new System.Drawing.Point(2, 2);
            this.userGiveGroup.Name = "userGiveGroup";
            this.userGiveGroup.Size = new System.Drawing.Size(266, 519);
            this.userGiveGroup.TabIndex = 0;
            this.userGiveGroup.TabStop = false;
            this.userGiveGroup.Text = "Random User";
            this.userGiveGroup.Enter += new System.EventHandler(this.userGiveGroup_Enter);
            // 
            // totalUsers
            // 
            this.totalUsers.AutoSize = true;
            this.totalUsers.Location = new System.Drawing.Point(20, 220);
            this.totalUsers.Name = "totalUsers";
            this.totalUsers.Size = new System.Drawing.Size(0, 13);
            this.totalUsers.TabIndex = 9;
            // 
            // removeUserBtn
            // 
            this.removeUserBtn.Location = new System.Drawing.Point(23, 253);
            this.removeUserBtn.Name = "removeUserBtn";
            this.removeUserBtn.Size = new System.Drawing.Size(100, 23);
            this.removeUserBtn.TabIndex = 8;
            this.removeUserBtn.Text = "Remove Selected";
            this.removeUserBtn.UseVisualStyleBackColor = true;
            this.removeUserBtn.Click += new System.EventHandler(this.removeUserBtn_Click);
            // 
            // beginUserBtn
            // 
            this.beginUserBtn.Location = new System.Drawing.Point(23, 311);
            this.beginUserBtn.Name = "beginUserBtn";
            this.beginUserBtn.Size = new System.Drawing.Size(206, 23);
            this.beginUserBtn.TabIndex = 7;
            this.beginUserBtn.Text = "Choose User";
            this.beginUserBtn.UseVisualStyleBackColor = true;
            this.beginUserBtn.Click += new System.EventHandler(this.beginUserBtn_Click);
            // 
            // howToUpdate
            // 
            this.howToUpdate.AutoSize = true;
            this.howToUpdate.Location = new System.Drawing.Point(220, 343);
            this.howToUpdate.Name = "howToUpdate";
            this.howToUpdate.Size = new System.Drawing.Size(13, 13);
            this.howToUpdate.TabIndex = 6;
            this.howToUpdate.TabStop = true;
            this.howToUpdate.Text = "?";
            this.howToUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.howToUpdate_LinkClicked);
            // 
            // currentGiveaway
            // 
            this.currentGiveaway.Location = new System.Drawing.Point(116, 340);
            this.currentGiveaway.Name = "currentGiveaway";
            this.currentGiveaway.Size = new System.Drawing.Size(100, 20);
            this.currentGiveaway.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current Channel:";
            // 
            // viewJsonBtn
            // 
            this.viewJsonBtn.Location = new System.Drawing.Point(23, 282);
            this.viewJsonBtn.Name = "viewJsonBtn";
            this.viewJsonBtn.Size = new System.Drawing.Size(76, 23);
            this.viewJsonBtn.TabIndex = 3;
            this.viewJsonBtn.Text = "View JSON";
            this.viewJsonBtn.UseVisualStyleBackColor = true;
            this.viewJsonBtn.Click += new System.EventHandler(this.viewJsonBtn_Click);
            // 
            // refreshList
            // 
            this.refreshList.Location = new System.Drawing.Point(120, 282);
            this.refreshList.Name = "refreshList";
            this.refreshList.Size = new System.Drawing.Size(109, 23);
            this.refreshList.TabIndex = 3;
            this.refreshList.Text = "Refresh Viewer List";
            this.refreshList.UseVisualStyleBackColor = true;
            this.refreshList.Click += new System.EventHandler(this.refreshList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Viewers:";
            // 
            // viewerLists
            // 
            this.viewerLists.FormattingEnabled = true;
            this.viewerLists.Location = new System.Drawing.Point(10, 96);
            this.viewerLists.Name = "viewerLists";
            this.viewerLists.Size = new System.Drawing.Size(250, 121);
            this.viewerLists.TabIndex = 1;
            this.viewerLists.SelectedIndexChanged += new System.EventHandler(this.viewerLists_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is a tool for choosing a random person from chat. It will use the Twitch API" +
    " to fetch the viewer list, and they will be displayed in the list boxes below.";
            // 
            // numberGiveGroup
            // 
            this.numberGiveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberGiveGroup.Controls.Add(this.label8);
            this.numberGiveGroup.Controls.Add(this.userGuesses);
            this.numberGiveGroup.Controls.Add(this.generateRNG);
            this.numberGiveGroup.Controls.Add(this.hideRNG);
            this.numberGiveGroup.Controls.Add(this.label7);
            this.numberGiveGroup.Controls.Add(this.label6);
            this.numberGiveGroup.Controls.Add(this.RNGOutput);
            this.numberGiveGroup.Controls.Add(this.label2);
            this.numberGiveGroup.Location = new System.Drawing.Point(274, 2);
            this.numberGiveGroup.Name = "numberGiveGroup";
            this.numberGiveGroup.Size = new System.Drawing.Size(267, 519);
            this.numberGiveGroup.TabIndex = 1;
            this.numberGiveGroup.TabStop = false;
            this.numberGiveGroup.Text = "Random Number";
            this.numberGiveGroup.Enter += new System.EventHandler(this.numberGiveGroup_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Guesses So Far:";
            // 
            // userGuesses
            // 
            this.userGuesses.FormattingEnabled = true;
            this.userGuesses.Location = new System.Drawing.Point(6, 291);
            this.userGuesses.Name = "userGuesses";
            this.userGuesses.Size = new System.Drawing.Size(255, 212);
            this.userGuesses.TabIndex = 6;
            // 
            // generateRNG
            // 
            this.generateRNG.Location = new System.Drawing.Point(21, 180);
            this.generateRNG.Name = "generateRNG";
            this.generateRNG.Size = new System.Drawing.Size(184, 23);
            this.generateRNG.TabIndex = 5;
            this.generateRNG.Text = "Generate";
            this.generateRNG.UseVisualStyleBackColor = true;
            this.generateRNG.Click += new System.EventHandler(this.generateRNG_Click);
            // 
            // hideRNG
            // 
            this.hideRNG.AutoSize = true;
            this.hideRNG.Checked = true;
            this.hideRNG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hideRNG.Location = new System.Drawing.Point(145, 145);
            this.hideRNG.Name = "hideRNG";
            this.hideRNG.Size = new System.Drawing.Size(60, 17);
            this.hideRNG.TabIndex = 4;
            this.hideRNG.Text = "Hidden";
            this.hideRNG.UseVisualStyleBackColor = true;
            this.hideRNG.CheckedChanged += new System.EventHandler(this.hideRNG_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Current Number:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 99);
            this.label6.TabIndex = 2;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // RNGOutput
            // 
            this.RNGOutput.Location = new System.Drawing.Point(108, 143);
            this.RNGOutput.Name = "RNGOutput";
            this.RNGOutput.ReadOnly = true;
            this.RNGOutput.Size = new System.Drawing.Size(30, 20);
            this.RNGOutput.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(41, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "-- Coming Soon! -- ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(69, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "-- Coming Soon! -- ";
            // 
            // wordGiveGroup
            // 
            this.wordGiveGroup.Controls.Add(this.label5);
            this.wordGiveGroup.Dock = System.Windows.Forms.DockStyle.Right;
            this.wordGiveGroup.Enabled = false;
            this.wordGiveGroup.Location = new System.Drawing.Point(547, 2);
            this.wordGiveGroup.Name = "wordGiveGroup";
            this.wordGiveGroup.Size = new System.Drawing.Size(266, 519);
            this.wordGiveGroup.TabIndex = 2;
            this.wordGiveGroup.TabStop = false;
            this.wordGiveGroup.Text = "Word Giveaway";
            // 
            // giveaway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 523);
            this.Controls.Add(this.wordGiveGroup);
            this.Controls.Add(this.numberGiveGroup);
            this.Controls.Add(this.userGiveGroup);
            this.MinimumSize = new System.Drawing.Size(831, 562);
            this.Name = "giveaway";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.Text = "Giveaway Tool";
            this.Load += new System.EventHandler(this.giveaway_Load);
            this.userGiveGroup.ResumeLayout(false);
            this.userGiveGroup.PerformLayout();
            this.numberGiveGroup.ResumeLayout(false);
            this.numberGiveGroup.PerformLayout();
            this.wordGiveGroup.ResumeLayout(false);
            this.wordGiveGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox userGiveGroup;
        private System.Windows.Forms.GroupBox numberGiveGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox viewerLists;
        private System.Windows.Forms.Button viewJsonBtn;
        private System.Windows.Forms.LinkLabel howToUpdate;
        private System.Windows.Forms.TextBox currentGiveaway;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button beginUserBtn;
        private System.Windows.Forms.Button removeUserBtn;
        private System.Windows.Forms.Label totalUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button generateRNG;
        private System.Windows.Forms.CheckBox hideRNG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RNGOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox userGuesses;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox wordGiveGroup;
    }
}