using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using System.Diagnostics;
using CefSharp.WinForms;
namespace subathontool
{
    public partial class activationWindow : Form
    {
        //  main main = new main();
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public activationWindow()
        {
            InitializeComponent();
            InitializeBrowser();
        }
        public ChromiumWebBrowser activationBrow;
        public void InitializeBrowser()
        {
            var settings = new CefSettings();
            settings.CachePath = appdataFolder + "/subathontool/cache";
            settings.LogFile = appdataFolder + "/subathontool/cefsharp.txt";
            settings.LogSeverity = LogSeverity.Default;

            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }

            activationBrow = new ChromiumWebBrowser("");
            this.Controls.Add(activationBrow);
            activationBrow.Visible = true;
            activationBrow.Location = new Point(-1, 0);
            activationBrow.Size = new Size(433, 85);
            label1.BringToFront();

        }
        private void cancelActivationBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public class activation
        {
            public bool isActivated { get; set; }
            public string activatedTo { get; set; }
            public string activationKey { get; set; }
        }
        private Point _Offset = Point.Empty;

        private void activationWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _Offset = new Point(e.X, e.Y);
            }
        }

        private void activationWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _Offset = Point.Empty;
        }

        private void activationWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Offset != Point.Empty)
            {
                Point newlocation = this.Location;
                newlocation.X += e.X - _Offset.X;
                newlocation.Y += e.Y - _Offset.Y;
                this.Location = newlocation;
            }
        }

        private void activateBtn_Click(object sender, EventArgs e)
        {
            invalidCodeLbl.Visible = false;
        }

        private void activationWindow_Load(object sender, EventArgs e)
        {
            discordLink.LinkBehavior = LinkBehavior.NeverUnderline;
            knowledgeLink.LinkBehavior = LinkBehavior.NeverUnderline;
            mailLink.LinkBehavior = LinkBehavior.NeverUnderline;
            twitterLink.LinkBehavior = LinkBehavior.NeverUnderline;

            discordLink.Click += DiscordLink_Click;
            knowledgeLink.Click += KnowledgeLink_Click;
            mailLink.Click += MailLink_Click;
            twitterLink.Click += twitterLinkClick;
        }

        private void twitterLinkClick(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/madgamerbot");
        }

        private void MailLink_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:support@themadgamers.co.uk");
        }

        private void KnowledgeLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://themadgamers.freshdesk.com/support/solutions");
        }

        private void DiscordLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/n9WxdDF");
        }

        private void activationWindow_Shown(object sender, EventArgs e)
        {
            
        }
        public void loadBrowser(string url)
        {
            if (!activationBrow.Address.Contains("login.php?"))
            {
                activationBrow.Load(url);
            }
                
           
        }

        private void purchasePct_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("http://streamtip.com/t/madmikegamerxl1");
        }
    }
}
