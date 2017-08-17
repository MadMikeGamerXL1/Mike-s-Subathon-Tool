using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subathontool
{
    class fileIntegrity
    {
        //string[] filesMissing = { };
        List<string> filesMissing = new List<string>();
        List<string> urls = new List<string>();
       // integrityDownload download = new integrityDownload();
        logger writeFile = new logger();
        updatecheck updateCheck = new updatecheck();
        bool updateNeeded = false;
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public void checkFiles()
        {
            var updatePath = "updater/version.tmgf";
            if (File.Exists(updatePath))
            {
                var updaterVersion = File.ReadAllText(updatePath);

                if (updateCheck.isUpdaterOutdated(updaterVersion))
                {
                    // Version is not newest, download new updater exe

                    if (!IsAdministrator())
                    {
                        MessageBox.Show("The Subathon Tool is either missing dependencies, or they're outdated. To ensure full functionality, please restart the tool with Administrator privilages, and the Tool will download these dependencies. ", "Outdated or Missing Dependencies Detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        exitApp();

                    }
                    else
                    {
                        writeFile.logFile("Detected updater.exe has new update - downloading now.");
                        downloadUpdater();
                        updateNeeded = true;
                    }

                }
                else
                {
                    writeFile.logFile("Detected updater.exe file is up to date.");
                }
            }
            else
            { //No Version file found, downloading latest update with new file.

                writeFile.logFile("Couldn't find the updater version - downloading new executable.");

                if (!IsAdministrator())
                {

                    MessageBox.Show("No updater file has been found. The product will not receive the latest updates without, and may experience critical problems. " + Environment.NewLine + "Please restart the application with Administrator privilages. If you're unsure about how, see the 'Help' section on the website.", "Critical File integrity error. Admin privilages required.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    exitApp();


                }
                else
                {

                    if (!Directory.Exists("updater"))
                    {
                        writeFile.logFile("Updater directory does not exist.");
                        Directory.CreateDirectory("updater");
                    }

                    downloadUpdater();
                    updateNeeded = true;
                }



            }


            if (File.Exists("Interop.WMPLib.dll") && File.Exists("AxInterop.WMPLib.dll"))
            {
                writeFile.logFile("Found WMPLib Dependencies in Application folder. ");
            }
            else
            {
                if (IsAdministrator() == true)
                {
                    writeFile.logFile("Detected missing dependencies - downloading now.");
                    downloadWMP();
                    updateNeeded = true;
                }
                else
                {
                    MessageBox.Show("The Application detected that there were missing dependencies - These are downloaded, but the application requires Administrator privilages. Please restart the app under Administrator mode. ", "Subathon Tool requires Administrator privilages", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    exitApp();
                }
            }
            if (File.Exists("Newtonsoft.Json.dll") && File.Exists("Newtonsoft.Json.xml"))
            {
                writeFile.logFile("Found WMPLib Dependencies in Application folder. ");
            }
            else
            {
                if (IsAdministrator() == true)
                {
                    writeFile.logFile("Detected missing dependencies - downloading now.");
                    downloadJSON();
                    updateNeeded = true;
                }
                else
                {
                    MessageBox.Show("The Application detected that there were missing dependencies - These are downloaded, but the application requires Administrator privilages. Please restart the app under Administrator mode. ", "Subathon Tool requires Administrator privilages", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    exitApp();
                }
            }
            if (File.Exists("IrcDotNet.dll") && File.Exists("IrcDotNet.xml"))
            {
                writeFile.logFile("Found WMPLib Dependencies in Application folder. ");
            }
            else
            {
                if (IsAdministrator() == true)
                {
                    writeFile.logFile("Detected missing dependencies - downloading now.");
                    downloadIRC();
                    updateNeeded = true;
                }
                else
                {
                    MessageBox.Show("The Application detected that there were missing dependencies - These are downloaded, but the application requires Administrator privilages. Please restart the app under Administrator mode. ", "Subathon Tool requires Administrator privilages", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    exitApp();
                }
            }
            if (File.Exists("data/new-battlelogo-128x128.ico") && File.Exists("data/subathon.ico"))
            {
                writeFile.logFile("Found WMPLib Dependencies in Application folder. ");
            }
            else
            {
                //  this.t = new System.Threading.Thread(new System.Threading.ThreadStart(fileIntegrity.downloadIcon));

                if (IsAdministrator() == true)
                {
                    writeFile.logFile("Detected missing dependencies - downloading now.");
                    downloadIcon();
                    updateNeeded = true;
                }
                else
                {
                    MessageBox.Show("The Application detected that there were missing files - These are downloaded, but the application requires Administrator privilages. Please restart the app under Administrator mode. ", "Subathon Tool requires Administrator privilages", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    exitApp();
                }
            }
            if(File.Exists("libcef.dll") && File.Exists("CefSharp.BrowserSubprocess.Core.dll") && File.Exists("CefSharp.Core.dll") && File.Exists("CefSharp.dll") && File.Exists("CefSharp.WinForms.dll") && File.Exists("connect.gamewisp.dll"))
            {
                writeFile.logFile("Found Gamewisp dependencies in folder.");
            }else
            {
                if(IsAdministrator() == true)
                {
                    writeFile.logFile("Detected missing Gamewisp dependencies - downloading. ");
                    downloadGamewispReq();
                    updateNeeded = true;
                }else
                {
                    MessageBox.Show("The Application detected that there were missing files - These are downloaded, but the application requires Administrator privilages. Please restart the app under Administrator mode. ", "Subathon Tool requires Administrator privilages", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    exitApp();
                }
            }
            if (updateNeeded)
            {
               // download.downloadFiles(filesMissing.ToArray(), urls.ToArray());

               /** if (download.Visible)
                {
                    download.BringToFront();
                }
                else
                {
                    download.Show();
                } **/
            }
            
           
        }
        public void exitApp()
        {
            Environment.Exit(2);

        }
        public void downloadWMP()
        {
            using (var client = new WebClient())
            {
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/AxInterop.WMPLib.dll", "AxInterop.WMPLib.dll");
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/Interop.WMPLib.dll", "Interop.WMPLib.dll");
                filesMissing.Add("Interop.WMPLib.dll");
                filesMissing.Add("AxInterop.WMPLib.dll");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/Interop.WMPLib.dll");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/AxInterop.WMPLib.dll");
            }
        }
        public void downloadIcon()
        {
            using (var client = new WebClient())
            {
                if (!Directory.Exists("data")) { Directory.CreateDirectory("data"); }
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/data/new-battlelogo-128x128.ico", "data/new-battlelogo-128x128.ico");
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/data/subathon.ico", "data/subathon.ico");
                filesMissing.Add("new-battlelogo-128x128.ico");
                filesMissing.Add("subathon.ico");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/data/new-battlelogo-128x128.ico");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/data/subathon.ico");
            }
        }
        public void downloadUpdater()
        {
            var webRequest = WebRequest.Create(@"http://themadgamers.co.uk/twitchtools/subathontool/updater/updateversion.txt");

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var latestVersion = reader.ReadToEnd();
                var updatePath = "updater/version.tmgf";

                File.WriteAllText(updatePath, latestVersion);

                using (var client = new WebClient())
                {
                    if (!Directory.Exists("updater")) { Directory.CreateDirectory("updater"); }
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/updater/updater.exe", "updater/updater.exe");
                    filesMissing.Add("updater.exe");
                    urls.Add("http://themadgamers.co.uk/twitchtools/subathontool/updater/updater.exe");
                }
            }
        }

        public void downloadJSON()
        {

            using (var client = new WebClient())
            {
                
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/Newtonsoft.Json.dll", "Newtonsoft.Json.dll");
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/Newtonsoft.Json.xml", "Newtonsoft.Json.xml");
                filesMissing.Add("Newtonsoft.Json.xml");
                filesMissing.Add("Newtonsoft.Json.dll");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/Newtonsoft.Json.xml");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/Newtonsoft.Json.dll");
            }

        }
        public void downloadIRC()
        {
            using (var client = new WebClient())
            {

                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/IrcDotNet.dll", "IrcDotNet.dll");
                client.DownloadFile("http://www.themadgamers.co.uk/twitchtools/subathontool/IrcDotNet.xml", "IrcDotNet.xml");
                filesMissing.Add("IrcDotNet.xml");
                filesMissing.Add("IrcDotNet.dll");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/IrcDotNet.xml");
                urls.Add("http://www.themadgamers.co.uk/twitchtools/subathontool/IrcDotNet.dll");
            }
        }
        public void downloadGamewispReq()
        {
            /**
             * CefSharp.BrowserSubprocess.Core.dll
             * CefSharp.Core.dll
             * CefSharp.dll
             * CefSharp.WinForms.dll
             * connect.gamewisp.dll
             * d3dcompiler_43.dll
             * d3dcompiler_47.dll
             * libcef.dll
             * libEGL.dll
             * libGLESv2.dll
             * widevinecdmadapter.dll
             * natives_blob.bin
             * snapshot_blob.bin
             * icudtl.dat
             * cef.pak
             * cef_100_percent.pak
             * cef_200_percent.pak
             * cef_extensions.pak
             * devtools_resources.pak
             * CefSharp.Core.xml
             * CefSharp.WinForms.xml
             * CefSharp.xml
             * **/
            using (var client = new WebClient())
            {

                client.DownloadFileCompleted += Client_DownloadFileCompleted;

                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.BrowserSubprocess.Core.dll", "CefSharp.BrowserSubprocess.Core.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.dll", "CefSharp.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.Core.dll", "CefSharp.Core.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.WinForms.dll", "CefSharp.WinForms.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/connect.gamewisp.dll", "connect.gamewisp.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/d3dcompiler_43.dll", "d3dcompiler_43.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/d3dcompiler_47.dll", "d3dcompiler_47.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/libcef.dll", "libcef.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/libEGL.dll", "libEGL.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/libGLESv2.dll", "libGLESv2.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/widevinecdmadapter.dll", "widevinecdmadapter.dll");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/natives_blob.bin", "natives_blob.bin");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/snapshot_blob.bin", "snapshot_blob.bin");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/icudtl.dat", "icudtl.dat");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/cef.pak", "cef.pak");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/cef_100_percent.pak", "cef_100_percent.pak");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/cef_200_percent.pak", "cef_200_percent.pak");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/cef_extensions.pak", "cef_extensions.pak");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/devtools_resources.pak", "devtools_resources.pak");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.Core.xml", "CefSharp.Core.xml");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.WinForms.xml", "CefSharp.WinForms.xml");
                client.DownloadFile("http://themadgamers.co.uk/twitchtools/subathontool/CefSharp.xml", "CefSharp.xml");

                filesMissing.Add("CefSharp.BrowserSubprocess.Core.dll");
                filesMissing.Add("CefSharp.Core.dll");
                filesMissing.Add("CefSharp.dll");
                filesMissing.Add("CefSharp.WinForms.dll");
                filesMissing.Add("connect.gamewisp.dll");
                filesMissing.Add("d3dcompiler_43.dll");
                filesMissing.Add("d3dcompiler_47.dll");
                filesMissing.Add("libcef.dll");
                filesMissing.Add("libEGL.dll");
                filesMissing.Add("libGLESv2.dll");
                filesMissing.Add("widevinecdmadapter.dll");
                filesMissing.Add("natives_blob.bin");
                filesMissing.Add("snapshot_blob.bin");
                filesMissing.Add("icudtl.dat");
                filesMissing.Add("cef.pak");
                filesMissing.Add("cef_100_percent.pak");
                filesMissing.Add("cef_200_percent.pak");
                filesMissing.Add("cef_extensions.pak");
                filesMissing.Add("devtools_resources.pak");
                filesMissing.Add("CefSharp.Core.xml");
                filesMissing.Add("CefSharp.WinForms.xml");
                filesMissing.Add("CefSharp.xml");


            }
        }
        
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Successfully downloaded missing dependencies. ", "Downloaded Missing Dependencies", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        
    }
}
