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
    public partial class changelog : Form
    {
        public changelog()
        {
            InitializeComponent();
        }
        updatecheck version = new updatecheck();
        private void changelog_Load(object sender, EventArgs e)
        {
            Text = "Change Log - " + version.currentVersion();
            webBrowser1.Refresh();
        }
    }
}
