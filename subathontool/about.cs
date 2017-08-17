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
    public partial class about : Form
    {
        updatecheck updater = new updatecheck();
        public about()
        {
            InitializeComponent();
        }

        private void about_Load(object sender, EventArgs e)
        {
            versionLbl.Text += updater.currentVersion();
        }

        private void about_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
