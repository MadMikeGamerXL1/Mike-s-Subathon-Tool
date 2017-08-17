namespace subathontool
{
    partial class twitchChat
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.userChatMsg = new System.Windows.Forms.TextBox();
            this.sendMsg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(624, 420);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // userChatMsg
            // 
            this.userChatMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userChatMsg.Location = new System.Drawing.Point(0, 422);
            this.userChatMsg.Name = "userChatMsg";
            this.userChatMsg.Size = new System.Drawing.Size(543, 20);
            this.userChatMsg.TabIndex = 1;
            // 
            // sendMsg
            // 
            this.sendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMsg.Location = new System.Drawing.Point(548, 421);
            this.sendMsg.Name = "sendMsg";
            this.sendMsg.Size = new System.Drawing.Size(75, 23);
            this.sendMsg.TabIndex = 2;
            this.sendMsg.Text = "Send";
            this.sendMsg.UseVisualStyleBackColor = true;
            this.sendMsg.Click += new System.EventHandler(this.sendMsg_Click);
            // 
            // twitchChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.sendMsg);
            this.Controls.Add(this.userChatMsg);
            this.Controls.Add(this.listBox1);
            this.Name = "twitchChat";
            this.ShowIcon = false;
            this.Text = "twitchChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.twitchChat_FormClosing);
            this.Load += new System.EventHandler(this.twitchChat_Load);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.twitchChat_GiveFeedback);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox userChatMsg;
        private System.Windows.Forms.Button sendMsg;
    }
}