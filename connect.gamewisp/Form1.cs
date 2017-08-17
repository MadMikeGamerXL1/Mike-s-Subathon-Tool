using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace connect.gamewisp
{
    
    public partial class authorization : Form
    {

        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public authorization()
        {
            InitializeComponent();
            InitBrowser();
           // browser.Load(https://api.gamewisp.com/pub/v1/oauth/authorize?client_id=18f5ac4aa00324a7867aaa6a7ec797df79946c5&redirect_uri=http://themadgamers.co.uk/subathontool.html&response_type=code&scope=read_only,subscriber_read_limited&state=ASKDLFJsisisks23k);
        }
        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            var settings = new CefSettings();
            settings.CachePath = appdataFolder + "/subathontool/cache";
            settings.LogFile = userDocsFolder + "/subathontool/cefsharp.txt";
            settings.LogSeverity = LogSeverity.Default;
            if (!Cef.IsInitialized)
            {
                Cef.Initialize(settings);
            }
            
            browser = new ChromiumWebBrowser("https://api.gamewisp.com/pub/v1/oauth/authorize?client_id=9e9fb5333819664694595ff18321995d75fff85&redirect_uri=http://dev.themadgamers.co.uk/access&response_type=code&scope=read_only,subscriber_read_limited&state=ASKDLFJsisisks23k");
            this.Controls.Add(browser);
            
            browser.Dock = DockStyle.Fill;
        }

        private void authorization_Load(object sender, EventArgs e)
        {
            
        }
    }
}
