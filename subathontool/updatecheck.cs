using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.ComponentModel;
using Newtonsoft.Json;

namespace subathontool
{
    class updatecheck
    {
        logger log = new logger();
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public bool updateNeeded()
        {
            return needsUpdate;
        }
        public void checkUpdates()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            log.logUpdate("--updatechecker-- Current application version: " + version);
            Console.WriteLine("--updatechecker-- Current Application version: " + version);
            // MessageBox.Show("Found application version : " + version);
            
            var webRequest = WebRequest.Create(@"http://themadgamers.co.uk/twitchtools/subathontool/version.txt");

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var latestVersion = reader.ReadToEnd();
                log.logUpdate("Checking ' subathontool/version ' for latest known version...");
                if (version != latestVersion)
                {
                    MessageBox.Show("Subathon Tool Outdated! " + Environment.NewLine +  Environment.NewLine + "Current Version: " + version + Environment.NewLine + "Latest Update: " + latestVersion, latestVersion + " is available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    needsUpdate = true;
                    ProcessStartInfo p = new ProcessStartInfo(@"updater\updater.exe");
                    File.WriteAllText(appdataFolder + "/subathontool/change.subtool", "true");

                    p.UseShellExecute = true;
                    p.Verb = "runas";
                    
                    
                    try {
                        Process.Start(p);
                        log.logUpdate("Loaded updater tool - shutting down application.");
                        log.logUpdate("");
                    }catch(Win32Exception ex)
                    {
                        log.logUpdate("Failed to start Updater program - " + ex);
                    }
                    //Application.Exit();
                    Environment.Exit(0);
                }
                else
                {
                    //  MessageBox.Show("Latest Version Detected: " + latestVersion + Environment.NewLine + version + " appears to be the latest version!");
                    log.logUpdate("Found latest version - " + latestVersion + " Current version: " + version + " - Application is up to date.");
                    needsUpdate = false;
                }
            }

        }
        bool needsUpdate = false;
        public bool isUpdaterOutdated(string version)
        {
            try
            {

            
            var webRequest = WebRequest.Create(@"http://themadgamers.co.uk/twitchtools/subathontool/updater/updateversion.txt");

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var latestVersion = reader.ReadToEnd();


                log.logUpdate("Checking ' subathontool/updater/version ' for latest known version...");

                if (version != latestVersion)
                {
                    return true;
                }
                else
                {
                    //  MessageBox.Show("Latest Version Detected: " + latestVersion + Environment.NewLine + version + " appears to be the latest version!");
                    log.logUpdate("Found latest version - " + latestVersion + " Current version: " + version + " - Application is up to date.");
                    return false;
                }
            }
        } catch(Exception err)
            {
                log.logUpdate("Encountered error while checking site - " + err);
                MessageBox.Show("We couldn't access the internet to verify your files. Please check your internet connection.");
                return false;
            }
        }
        public string currentVersion ()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            return version;
        }
        public class data
        {
            public string channeltojoin { get; set; }
            public string channelforgiveaway { get; set; }
            public string timerCompleteSoundFile { get; set; }
            public bool checkViewerList { get; set; }
            public bool refreshViewerSound { get; set; }
            public int defaultCountdownValue { get; set; }
            public int subscriberAddsValue { get; set; }
            public bool gamingMode { get; set; }
            public int resubsAddValue { get; set; }
            public bool enableSongRequest { get; set; }
            public string[] regularsList { get; set; }
        }
    }
}
