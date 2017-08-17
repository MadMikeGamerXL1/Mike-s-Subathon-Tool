namespace subathontool
{
    partial class clearLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(clearLogs));
            this.promptCleanPnl = new System.Windows.Forms.Panel();
            this.clearFilesPnl = new System.Windows.Forms.Panel();
            this.exportLogBtn = new System.Windows.Forms.Button();
            this.clearLogBtn = new System.Windows.Forms.Button();
            this.logsList = new System.Windows.Forms.ListBox();
            this.stepsClearLbl = new System.Windows.Forms.Label();
            this.cancelClearBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.continueBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ExportBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.promptCleanPnl.SuspendLayout();
            this.clearFilesPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // promptCleanPnl
            // 
            this.promptCleanPnl.Controls.Add(this.clearFilesPnl);
            this.promptCleanPnl.Controls.Add(this.cancelBtn);
            this.promptCleanPnl.Controls.Add(this.continueBtn);
            this.promptCleanPnl.Controls.Add(this.label1);
            this.promptCleanPnl.Controls.Add(this.pictureBox1);
            this.promptCleanPnl.Controls.Add(this.titleLbl);
            this.promptCleanPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.promptCleanPnl.Location = new System.Drawing.Point(0, 0);
            this.promptCleanPnl.Name = "promptCleanPnl";
            this.promptCleanPnl.Size = new System.Drawing.Size(547, 348);
            this.promptCleanPnl.TabIndex = 0;
            // 
            // clearFilesPnl
            // 
            this.clearFilesPnl.Controls.Add(this.exportLogBtn);
            this.clearFilesPnl.Controls.Add(this.clearLogBtn);
            this.clearFilesPnl.Controls.Add(this.logsList);
            this.clearFilesPnl.Controls.Add(this.stepsClearLbl);
            this.clearFilesPnl.Controls.Add(this.cancelClearBtn);
            this.clearFilesPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearFilesPnl.Location = new System.Drawing.Point(0, 0);
            this.clearFilesPnl.Name = "clearFilesPnl";
            this.clearFilesPnl.Size = new System.Drawing.Size(547, 348);
            this.clearFilesPnl.TabIndex = 4;
            // 
            // exportLogBtn
            // 
            this.exportLogBtn.BackgroundImage = global::subathontool.Properties.Resources.icons8_Export_64;
            this.exportLogBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.exportLogBtn.Location = new System.Drawing.Point(441, 272);
            this.exportLogBtn.Name = "exportLogBtn";
            this.exportLogBtn.Size = new System.Drawing.Size(28, 28);
            this.exportLogBtn.TabIndex = 4;
            this.infoToolTip.SetToolTip(this.exportLogBtn, "Export the Selected Log");
            this.exportLogBtn.UseVisualStyleBackColor = true;
            this.exportLogBtn.Click += new System.EventHandler(this.exportLogBtn_Click);
            // 
            // clearLogBtn
            // 
            this.clearLogBtn.Location = new System.Drawing.Point(460, 313);
            this.clearLogBtn.Name = "clearLogBtn";
            this.clearLogBtn.Size = new System.Drawing.Size(75, 23);
            this.clearLogBtn.TabIndex = 3;
            this.clearLogBtn.Text = "Clear Logs";
            this.infoToolTip.SetToolTip(this.clearLogBtn, "Begin Clearing Logs");
            this.clearLogBtn.UseVisualStyleBackColor = true;
            this.clearLogBtn.Click += new System.EventHandler(this.clearLogBtn_Click);
            // 
            // logsList
            // 
            this.logsList.FormattingEnabled = true;
            this.logsList.Location = new System.Drawing.Point(65, 119);
            this.logsList.Name = "logsList";
            this.logsList.Size = new System.Drawing.Size(404, 147);
            this.logsList.TabIndex = 2;
            this.infoToolTip.SetToolTip(this.logsList, "List of Log Files");
            // 
            // stepsClearLbl
            // 
            this.stepsClearLbl.Location = new System.Drawing.Point(7, 9);
            this.stepsClearLbl.Name = "stepsClearLbl";
            this.stepsClearLbl.Size = new System.Drawing.Size(537, 107);
            this.stepsClearLbl.TabIndex = 1;
            this.stepsClearLbl.Text = resources.GetString("stepsClearLbl.Text");
            // 
            // cancelClearBtn
            // 
            this.cancelClearBtn.Location = new System.Drawing.Point(12, 313);
            this.cancelClearBtn.Name = "cancelClearBtn";
            this.cancelClearBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelClearBtn.TabIndex = 0;
            this.cancelClearBtn.Text = "Cancel";
            this.infoToolTip.SetToolTip(this.cancelClearBtn, "Exit Logs Folder Maintenance");
            this.cancelClearBtn.UseVisualStyleBackColor = true;
            this.cancelClearBtn.Click += new System.EventHandler(this.cancelClearBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(10, 313);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "No thanks";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelClearBtn_Click);
            // 
            // continueBtn
            // 
            this.continueBtn.Location = new System.Drawing.Point(460, 313);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 3;
            this.continueBtn.Text = "Continue ->";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(543, 121);
            this.label1.TabIndex = 2;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::subathontool.Properties.Resources.exclamation;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(105, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 46);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // titleLbl
            // 
            this.titleLbl.Font = new System.Drawing.Font("Gill Sans MT", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.Location = new System.Drawing.Point(2, 57);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(544, 46);
            this.titleLbl.TabIndex = 0;
            this.titleLbl.Text = "Logs Folder Alert";
            this.titleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ExportBrowser
            // 
            
            // 
            // clearLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 348);
            this.Controls.Add(this.promptCleanPnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "clearLogs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Logs Folder Maintenance";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.clearLogs_FormClosing);
            this.Load += new System.EventHandler(this.clearLogs_Load);
            this.promptCleanPnl.ResumeLayout(false);
            this.clearFilesPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button cancelClearBtn;
        private System.Windows.Forms.ListBox logsList;
        private System.Windows.Forms.Label stepsClearLbl;
        private System.Windows.Forms.Button clearLogBtn;
        public System.Windows.Forms.Panel promptCleanPnl;
        public System.Windows.Forms.Panel clearFilesPnl;
        private System.Windows.Forms.Button exportLogBtn;
        private System.Windows.Forms.ToolTip infoToolTip;
        private System.Windows.Forms.FolderBrowserDialog ExportBrowser;
    }
}