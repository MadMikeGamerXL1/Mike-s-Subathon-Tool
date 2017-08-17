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
    public partial class integrityDownload : Form
    {
        public integrityDownload()
        {
            InitializeComponent();
        }
        int toDownload = 0;
        string toShow = "";
        public void downloadFiles(string[] files, string[] urls)
        {
            foreach (string file in files)
            {
                toDownload++;
                toShow += Environment.NewLine + file;  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(toShow);
        }
    }
}
