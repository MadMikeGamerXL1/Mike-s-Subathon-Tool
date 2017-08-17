using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subathontool
{
    public partial class winner : Form
    {
        public winner()
        {
            InitializeComponent();
        }

        private void winner_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
           // username.Visible = false;
            timer1.Start();
            username.Dock = DockStyle.Fill;
            username.AutoSize = false;
            username.TextAlign = ContentAlignment.MiddleCenter;
            username.SendToBack();
            username.Visible = true;
            username.ForeColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            this.ShowIcon = false;
            ShowInTaskbar = false;
            Text = "";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            webBrowser1.Visible = false;
            username.Visible = true;
            visibilityTimer.Start();
            timer1.Stop();
        }

        private void visibilityTimer_Tick(object sender, EventArgs e)
        {
            int fadeSpeed = 3;
            username.ForeColor = Color.FromArgb(username.ForeColor.R - fadeSpeed, username.ForeColor.G - fadeSpeed, username.ForeColor.B - fadeSpeed);
            if (username.ForeColor.R >= this.BackColor.R || username.ForeColor.R < 1)
            {
                visibilityTimer.Stop();
                username.ForeColor = Color.Black;
            }
        }
    }
}
