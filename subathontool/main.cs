using connect.gamewisp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using twitchBits;
using CefSharp;
using CefSharp.WinForms;
using Microsoft.Win32;
namespace subathontool
{

    public partial class main : Form
    {

        public int timeLeft { get; set; }
        public int uptime { get; set; }
        twitchChat f;
        clearLogs clear = new clearLogs();
        bitsStoreOptions storeOptions = new bitsStoreOptions();
        settingsForm settingsForm;
        activationWindow activatewindow;
        System.ComponentModel.Container component;
        public ChromiumWebBrowser clipsBrowser;
        System.Drawing.Icon icon;
        System.Threading.Thread t;
        ContextMenuStrip rightClickMenu = new ContextMenuStrip();
        NotifyIcon trayIcon;
        ToolStripMenuItem options = new ToolStripMenuItem();
        feedback feedback = new feedback();
        ToolStripMenuItem exit = new ToolStripMenuItem();
        ToolStripSeparator seperator = new ToolStripSeparator();
        bitsStore BStore = new bitsStore();
        logger writeFile = new logger();
        Timer connectionVerification = new Timer();
        fileIntegrity fileIntegrity = new fileIntegrity();
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private Point _offset = Point.Empty;
        int channelID;
        // Main Variables

        TcpClient tcpClient;
        StreamReader reader;
        StreamWriter writer;

        //Dashboard Values
        int totalSubs;
        int totalreSubs;
        int totalPrimeSubs;
        decimal bitsSum;
        decimal subsSum;
        decimal tipsSum;
        int totalT1Subs;
        int totalT2Subs;
        int totalT3Subs;
        int totalTips;
        int amountOfCheers;
        int totalMsg;
        int GiveawaysRan;
        int StorePurchases;
        int newSubValue;
        int reSubValue;
        int newSubAddTime;
        
        decimal bitsAdd = 0;
        int multiplyAmount;
        decimal tipsAdd;
        int maxNumber = 10;

        /**
            Which minutes panels are showing in the settings? If they're not, the expand|collapse button beside it should be set to: Expand - if they are visible, it should be set to Collapse.
        **/
        bool expandedCountdown = false;
        bool expandedSubs = false;
        bool expandedResubs = false;
        bool expandedBits = false;
        bool expandedTips = false;
        bool expandedGWSubs = false;
        bool expandedGWResubs = false;

        /// <summary>
        /// Determines whether the slot combo box has a selected item, or if the user typed something.
        /// </summary>
        bool slot0Selected = false;
        bool slot1Selected = false;
        bool slot2Selected = false;
        bool slot3Selected = false;
        bool slot4Selected = false;
        bool slot5Selected = false;
        bool slot6Selected = false;
        bool slot7Selected = false;
        bool slot8Selected = false;
        bool slot9Selected = false;
        bool slot10Selected = false;
        bool slot11Selected = false;

        bool allowStaffControl = false;
        bool connectionMessageReceived = false;
        bool useMultiplier = false;
        bool useGamewisp = false;
        bool playSoundOnFinish = false;
        bool isGamingModeActive = false;
        bool useChatCommands = false;
        bool announceResub = false;
        bool announceNewSub = false;
        bool announceCheers = false;
        bool OpensNewWindow;
        bool isConnected = false;
        bool lastCheckOnline = false;
        bool hasTimerFinished = false;
        bool checkingReActivation = false;
        bool hasActivatedOnce = false;
        bool useBitsAdd = false;
        bool useDonationCmd = false;
        bool roundUp = false;
        bool resetUptime = true;
        bool allowLogMaintenance = false;
        public bool isSubathonPaused = false;
        public bool giveawayIsActive = false;
        bool isCounting = false;
        string username;
        string password;
        string chan;
        string toJoin;
        string playSoundFile;
        string channelForGiveaway;
        string generatedRandomWord;
        string giveawayWord;
        string giveawayType;
        string currentLog;
        string registryActivated;
        string registryUser;
        string registryKey;
        dynamic json;
        dynamic jsonParse;
        dynamic giveawayViewers;
        dynamic giveawayViewersParse;
        public Dictionary<string, int> bitsDictionary = new Dictionary<string, int>();
        Dictionary<string, int> testDictionary = new Dictionary<string, int>();
        List<string> cheerUsers = new List<string>();
        List<string> giveawayUsers = new List<string>();
        List<int> cheerTotal = new List<int>();
        bits Bits;
        public static string MD5H(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuild.Append(result[i].ToString("x2"));
            }
            return strBuild.ToString();
        }

        public main()
        {
            checkFiles();
            updateCheck.checkUpdates();
            if (updateCheck.updateNeeded() == false)
            {
                Console.WriteLine("Update not needed.");
                Bits = new bits();
                gamewisp = new connection();
            }
            else { Console.WriteLine("[test] -- Update required -- "); }
            AppDomain.CurrentDomain.ProcessExit += exitSubathon;
            InitializeComponent();
            activatewindow = new activationWindow();
            f = new twitchChat(this);
            settingsForm = new settingsForm();
            settingsForm.FormClosing += SettingsForm_FormClosing;
            connectionVerification.Tick += connectionVerificationTick;
            connectionVerification.Interval = 3000;
            // Giving the classes a reference.
            aboutButtons = new Buttons.About();
            feedbackButtons = new Buttons.feedback();
            madgamerbotButtons = new Buttons.MadGamerBot();
            chatButtons = new Buttons.twitchChat();
            bitsButtons = new Buttons.bitsStore();
            settingsButtons = new Buttons.settings();
            giveawayButtons = new Buttons.giveaway();
            icon = new Icon("data/subathon.ico");
            // taskbarNotify.Icon = icon;
            component = new System.ComponentModel.Container();
            mainTabs.Controls.Remove(clipsTabPage);
            mainTabs.Controls.Remove(SongRequestPage);
           // mainTabs.Controls.Remove(dashboardTabPage);
        }
        private void connectionVerificationTick(object sender, EventArgs e)
        {
            if (!connectionMessageReceived)
            {
                Console.WriteLine("Message wasn't received - connection may have failed. Retrying.");
                writeFile.logFile("[WARN] : No message was received from IRC. The connection may have failed. Retrying...");
                reconnect(toJoin);
                chatBox.Items.Add("-- Connection Failed. Reconnecting --");
            }else { writeFile.logFile("Message received; connection verified."); }
            connectionVerification.Stop();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            settingsForm.Parent = null;
            settingsForm.Hide();
            e.Cancel = true;
            writeFile.logFile("Cancelled exiting settings... i.e. I saved the universe!! :)");

            checkSubValue();
            checkJsonSettings();
            checkDocumentsFolder();
        }

        void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


        void options_Click(object sender, EventArgs e)
        {
            mainTabs.SelectedTab = settingsPage;
        }
        public void exitApp()
        {
            Environment.Exit(2);

        }
        public void checkFiles()
        {
            if (!Directory.Exists(appdataFolder + "/subathontool/"))
            {
                Directory.CreateDirectory(appdataFolder + "/subathontool/");
                
            }
            string date = DateTime.UtcNow + ":" + DateTime.UtcNow.Millisecond;
            string fileName;
            fileName = date.Replace("/", "-").Replace(":", "-").Replace(" ", "_");
            File.WriteAllText(appdataFolder + "/subathontool/currentlog.txt", fileName);
            fileIntegrity.checkFiles();
        }
        updatecheck updateCheck = new updatecheck();
        bool hasUpdated = false;
        private void onIconClick(object sender, EventArgs e)
        {

        }
        private void checkChangelogShow()
        {
            var changelogFile = appdataFolder + "/subathontool/change.subtool";
            try
            {


                if (File.Exists(changelogFile))
                {
                    var fileText = File.ReadAllText(changelogFile);
                    bool showChangelog = Convert.ToBoolean(fileText);
                    if (showChangelog)
                    {
                        changelogView.Visible = true;
                        hasUpdated = true;
                    }
                    else
                    {
                        hideChangelog();
                        hasUpdated = false;
                    }
                }
                else
                {

                    File.WriteAllText(changelogFile, "true");

                    changelogView.Visible = true;
                }
            }
            catch (Exception err)
            {
                writeFile.logFile("Encountered error checking changelog file - " + err);
            }
        }
        public void getAPIDetails(string channelToFetch)
        {
            if (channelToFetch != null || channelToFetch != "")
            {
                using (WebClient wc = new WebClient())
                {
                    try
                    {

                        wc.DownloadFile($"https://api.twitch.tv/kraken/channels/{channelToFetch}?client_id=97rngr9u9zk6z0glo9o32bro3kygpkf", appdataFolder + "/subathontool/api.json");
                        dynamic jsonCode;
                        var file = File.ReadAllText(appdataFolder + "/subathontool/api.json");
                        jsonCode = file;
                        JObject o = JObject.Parse(jsonCode);
                        channelID = (int)o["_id"];
                        Console.WriteLine("Found channel ID from API - " + channelID);

                        if (File.Exists($"{appdataFolder}/subathontool/api.json")) { File.Delete($"{appdataFolder}/subathontool/api.json"); }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("We encountered an error trying to download your channel statistics from the API - please try again. If you continue to experience this, please report it using the built-in Feedback tool, or contact @MadGamerBot on Twitter. Try quoting this error to the team: \n" + Environment.NewLine + err + Environment.NewLine, "Encountered Error Checking API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                Console.WriteLine("Channel is empty. Skipping API check.");
                writeFile.logFile("ERROR fetching channel ID - the channel string is empty.");

            }

        }
        public void checkIsOnline()
        {
            dash_MsgCountLbl.Text = totalLines.ToString();
            dash_NewSubLbl.Text = totalSubs.ToString();
            dash_ReSubLbl.Text = totalreSubs.ToString();
            dash_PrimeSubLbl.Text = totalPrimeSubs.ToString();
            dash_T1SubLbl.Text = totalT1Subs.ToString();
            dash_T2SubLbl.Text = totalT2Subs.ToString();
            dash_T3SubLbl.Text = totalT3Subs.ToString();
            dash_SubsSumLbl.Text = "$" + subsSum.ToString();
            dash_TipCountLbl.Text = totalTips.ToString();
            dash_TipSumLbl.Text = "$" + tipsSum.ToString();
            dash_TotalBitsLbl.Text = amountOfCheers.ToString();
            dash_TotalBitSumLbl.Text = "$" + bitsSum.ToString();
            dash_GiveCountLbl.Text = GiveawaysRan.ToString();
            dash_TimeFromSubsLbl.Text = totalAddedSubs;
            dash_TimeFromReSubsLbl.Text = totalAddedResubs;
            dash_TimeFromBitsLbl.Text = totalAddedBits;
            dash_UptimeLbl.Text = statusLabel.Text;
            dash_StoreCountLbl.Text = StorePurchases.ToString();
            dash_TimeFromTipsLbl.Text = totalAddedTips;
            using (WebClient wc = new WebClient())
            {
                // 
                if (toJoin == null)
                {
                    writeFile.logFile("Couldn't check stream API - no channel was specified. - " + toJoin);
                }
                else
                {
                    try
                    {
                        var checkURL = "https://api.twitch.tv/kraken/streams/" + toJoin.Substring(toJoin.IndexOf(("#")) + 1) + "?client_id=sdum66uach1g3aprrgzws118kvt1agt";
                        // writeFile.logFile("Checking URL - " + checkURL);
                        json = wc.DownloadString(checkURL);

                        JObject o = JObject.Parse(json);
                        // viewerLists.Items.Add(jsonParse.stream);

                        /// converter.ReadJson();
                        if (!o["stream"].HasValues)
                        {
                            if(lastCheckOnline)
                            {
                                uptimeCount.Stop();
                                if (resetUptime)
                                {
                                    uptime = 0;
                                }
                                
                            }
                            uptimeLabel.Text = "Offline";
                            streamStatusPct.Image = subathontool.Properties.Resources._16x16_offline;
                            statusLabel.Image = Properties.Resources._16x16_offline;
                           // writeFile.logAPI("Detected '" + toJoin + "' is offline. - ");

                            if (lastCheckOnline == true && hasTimerFinished != true)
                            {
                                /** DialogResult result = MessageBox.Show("We've detected your stream has dropped, but the timer isn't over. " + Environment.NewLine + "Would you like to pause the countdown?", "Unexpected Stream Drop", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                 if (result == DialogResult.Yes)
                                 {
                                     // Pause countdown
                                     writeFile.logFile("Pausing the subathon.");
                                     pauseSubathon();
                                     
                                 }
                                 else { Console.WriteLine("User doesn't want to save."); } **/
                            }
                            lastCheckOnline = false;
                        }
                        else
                        {
                            if (!lastCheckOnline)
                            {
                                uptimeLabel.Text = "Online";
                                streamStatusPct.Image = subathontool.Properties.Resources._16x16_online;
                                statusLabel.Image = Properties.Resources._16x16_online;
                               // writeFile.logAPI("Detected '" + toJoin + "' is online! - ");
                                uptimeCount.Start();
                                lastCheckOnline = true;
                            }
                            
                        }
                        streamStatusPct.Location = new Point(831, 28);
                        streamStatusLbl.Location = new Point(860, 32);
                    }
                    catch (Exception error)
                    {
                        writeFile.logAPI("Parsing URL api.twitch.tv failed - " + Environment.NewLine + error);
                        streamStatusPct.Image = subathontool.Properties.Resources._16x16_offline;
                        streamStatusLbl.Text = "API_ERROR - " + error.HResult;

                        streamStatusPct.Location = new Point(807, 28);
                        streamStatusLbl.Location = new Point(836, 32);
                    }
                    // error 807, 28 | 836, 32
                    // offline 841, 28 | 870, 32
                }


            }
        }

        bool hasCrashDetected = false;
        public void pauseSubathon() {
            if (isCountingDown()) { subathonCount.Stop(); }
        }
        public void resumeSubathon()
        {
            if (isCountingDown()) { subathonCount.Start(); }
        }
        public void verifyUptime()
        {
            if (writeFile.fetchRawUptime() != null && writeFile.fetchRawUptime() != "0")
            {
                DialogResult result = MessageBox.Show("It appears the software may have crashed since the last uptime. Would you like to restore the uptime?", "Unexpected close detected.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    int tempUptime;
                    if (int.TryParse(writeFile.fetchRawUptime(), out tempUptime))
                    {
                        uptime = tempUptime;
                        Console.WriteLine("Successfully restored uptime");
                    }
                    else
                    {
                        MessageBox.Show("An unexpected error has occured - your uptime couldn't be restored. Please contact support@themadgamers.co.uk if this continues occuring.", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    timeLeft = writeFile.fetchCountdown();
                    hasCrashDetected = true;
                }
                else
                {
                    Console.WriteLine("User chose no - discarding changes to uptime");
                }
            }
            else if(writeFile.fetchCountdown() != 0) {

                DialogResult result = MessageBox.Show("It appears the software may have crashed since the last uptime. Would you like to restore the uptime?", "Unexpected close detected.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int tempUptime;
                    if (int.TryParse(writeFile.fetchRawUptime(), out tempUptime))
                    {
                        uptime = tempUptime;
                        Console.WriteLine("Successfully restored uptime");
                    }
                    else
                    {
                        MessageBox.Show("An unexpected error has occured - your uptime couldn't be restored. Please contact support@themadgamers.co.uk if this continues occuring.", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    timeLeft = writeFile.fetchCountdown();
                    hasCrashDetected = true;
                }
                else
                {
                    Console.WriteLine("User chose no - discarding changes to uptime");
                }
            }
        }
        private void checkLogFiles()
        {
            if(Directory.GetFiles(appdataFolder + "/subathontool/logs").Length > 9)
            {
                Console.WriteLine("Detected there are more than 9 log files in the logs folder. Opening clearLogs dialog");
                
                clear.promptCleanPnl.Visible = true;
                clear.clearFilesPnl.Visible = false;
                clear.Show();
            }
        }
        // Panel mainTabsPanel = new Panel();
        private void main_Load(object sender, EventArgs e)
        {
            this.Icon = subathontool.Properties.Resources.subathon;
            statusStrip1.Visible = false;
            // innerTwitch.Controls.Add(vScrollBar1);
            // reconnect(toJoin);
           /** mainTabsPanel.BackColor = Color.Black;
            mainTabsPanel.Controls.Add(mainTabs);
            mainTabsPanel.BringToFront();
            mainTabsPanel.Dock = DockStyle.Fill; **/
            this.Controls.Remove(mainTabs);
            //this.Controls.Add(mainTabsPanel);
            mainTabs.Controls.Remove(pictureBox1);
           // mainTabs.Dock = DockStyle.Fill;
            mainTabs.Controls.Remove(welcomePanel);
            helpTabPage.Controls.Remove(welcomePanel);
            //controlBorder.Controls.Remove(welcomePanel);
            panel3.Visible = false;
            changelogView.Visible = false;
            welcomePanel.Parent = this;
            this.Controls.Add(pictureBox1);
            this.Controls.Add(welcomePanel);
            controlBorder.Visible = false;
            // welcomePanel.Dock = DockStyle.Fill;
           // mainTabs.Parent = mainTabsPanel;
            welcomePanel.BringToFront();
            pictureBox1.BringToFront();


            updateBitsDictionary();
            Console.WriteLine("Parent: " + welcomePanel.Parent.Name.ToString() + welcomePanel.Size + "tab pages: " + mainTabs.Visible + "MainTabs: " + mainTabs.Parent.Name);
            welcomePanel.Size = this.Size;
            welcomePanel.Controls.Remove(mainTabs);
            // this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            apiCheck.Start();
            pictureBox1.Dock = DockStyle.Fill;
            checkChangelogShow();
            timer1.Start();
            updatedToChangelog.Text = "Updated to version " + updateCheck.currentVersion() + " - Click to see changes!";
            trayIcon = new NotifyIcon(component);
            trayIcon.Text = "Subathon Tool";
            trayIcon.Icon = icon;
            trayIcon.Visible = true;
            trayIcon.ContextMenuStrip = rightClickMenu;
            rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                options,
                seperator,
                exit
           });

            options.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            options.Text = "Options";
            options.Click += new EventHandler(options_Click);

            exit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            exit.Text = "Exit";
            exit.Click += new EventHandler(exit_Click);



            activatewindow.activateBtn.Click += ActivateBtn_Click;
            activatewindow.activationBrow.AddressChanged += activateDirect;


            activatewindow.Load += Activatewindow_Load;


            checkGamewisp();

            // taskbarNotify.BalloonTipClicked += onIconClick;
            checkJsonSettings();
            versionLbl.Text = "Current Version: " + updateCheck.currentVersion();
            //settings.BackgroundImage = Properties.Resources.setting;
            checkDocumentsFolder();
            checkIsOnline();
            //settings.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.White;
            cancelCountDown.Enabled = false;
            writeFile.logFile("=== Subathon Tool Initialising === ");
            writeFile.logFile(DateTime.Now.Day + " - " + DateTime.Now.Month + " - " + DateTime.Now.Year);
            Console.WriteLine("=== Subathon Tool Initialising === " + DateTime.Now.Day + " - " + DateTime.Now.Month + " - " + DateTime.Now.Year);
            if(toJoin != null)
            {
                if (toJoin.Substring(toJoin.IndexOf(("#")) + 1) != null && toJoin.Substring(toJoin.IndexOf(("#")) + 1) != "")
                {
                    Label soonLabel = new Label();
                    clipsTabPage.Controls.Add(soonLabel);
                    soonLabel.AutoSize = false;
                    soonLabel.Dock = DockStyle.Fill;
                    soonLabel.TextAlign = ContentAlignment.MiddleCenter;
                    soonLabel.Font = new Font("Gill Sans MT", 24, FontStyle.Bold);
                    soonLabel.Text = "\u26A0 This feature is not yet available. \u26A0";

                }
                else
                {
                    Label warningLabel = new Label();
                    clipsTabPage.Controls.Add(warningLabel);
                    warningLabel.AutoSize = false;
                    warningLabel.Dock = DockStyle.Fill;
                    warningLabel.TextAlign = ContentAlignment.MiddleCenter;
                    warningLabel.Font = new Font("Gill Sans MT", 24, FontStyle.Bold);
                    warningLabel.Text = "\u26A0 This feature is not yet available \u26A0";
                }
            }

            this.FormBorderStyle = FormBorderStyle.None;
            if (hasUpdated == true)
            {
                welcomeTimer.Start();

                welcomeUser.AutoSize = false;
                welcomeUser.TextAlign = ContentAlignment.MiddleCenter;
                welcomeUser.Dock = DockStyle.Fill;
                welcomeUser.ForeColor = Color.White;
                twitchEmote.Location = new Point()
                {
                    X = 416,
                    Y = 164
                };
                
            }
            warnPanel.Dock = DockStyle.Fill;
            warnlabel.Location = new Point(warnPanel.Width / 2 - 180, (warnPanel.Height / 2) - 32);
            hideWarn.Location = new Point((warnPanel.Width / 2) - 80, (warnPanel.Height / 2) + 30);
            
            // Could check viewer list here - not advised, as it causes visual hangs while the API is called. Use Threads instead.

            // checkList("https://tmi.twitch.tv/group/user/" + toJoin.Substring(toJoin.IndexOf(("#")) + 1) + "/chatters");

            //Begin checking if the software is activated.
            try
            {
                string keyLocation = "";
                if (Environment.Is64BitOperatingSystem)
                {
                    keyLocation = @"SOFTWARE\WOW6432Node\TheMadGamers";
                }
                else
                {
                    keyLocation = @"SOFTWARE\TheMadGamers";
                }
                RegistryKey activation = Registry.LocalMachine.OpenSubKey(keyLocation, false);
                
                registryActivated = (string)activation.GetValue("isActivated", "false");
                
                registryUser = (string)activation.GetValue("Username", "null");
                registryKey = (string)activation.GetValue("Key", "null");
                Console.WriteLine($"Found Registry value - Activated: {registryActivated} User: {registryUser} Key: {registryKey}");
            }
            catch(Exception err)
            {
                Console.Write(err + Environment.NewLine);
                
            }
            checkActivation();
            loadSettings();
            verifyUptime();
            if (allowLogMaintenance)
            {
                checkLogFiles();
            }
            
        }
        

        void setTitle()
        {
            Text = activatedUsername + " - " + Text;
            this.Text += " [" + updateCheck.currentVersion() + "]";
            if (IsAdministrator()) { this.Text += " (Administrator)"; }
            //mainWelcomeLbl.Text = "Welcome, " + activatedUsername + "! " + Environment.NewLine + "You are using the Subathon Tool, version " + updateCheck.currentVersion() + Environment.NewLine + mainWelcomeLbl.Text;
            if (hasUpdated)
            {
                welcomeUser.Text = "Hi, " + activatedUsername;
            }
            controlText.Text = Text;
            Console.WriteLine(Text);
        }
        private void setUsernames(string username)
        {
            Text = username + " - " + Text;
            //mainWelcomeLbl.Text = "Welcome, " + username + "! " + Environment.NewLine + "You are using the Subathon Tool, version " + updateCheck.currentVersion() + Environment.NewLine + mainWelcomeLbl.Text;
            if (hasUpdated == true) {
                welcomeUser.Text = "Hi, " + username;
            }
        }
        private void Activatewindow_Load(object sender, EventArgs e)
        {
            //  activatewindow.activateBtn.Click += ActivateBtn_Click;
        }

        private void writeActivationFile()
        {
                //MessageBox.Show("Setting Registry values");
            if (IsAdministrator())
            {
                string keyLocation = "";
                if (Environment.Is64BitOperatingSystem)
                {
                    keyLocation = @"SOFTWARE\WOW6432Node\TheMadGamers";
                }
                else
                {
                    keyLocation = @"SOFTWARE\TheMadGamers";
                }
                try
                {
                    RegistryKey activation = Registry.LocalMachine.CreateSubKey(keyLocation);
                    
                    activation.SetValue("Username", activatewindow.username.Text);
                    activation.SetValue("isActivated", "true");
                    activation.SetValue("Key", activatewindow.activationCode.Text);
                    
                    try
                    {
                        if (File.Exists(appdataFolder + "/subathontool/activation.json"))
                        {
                            writeFile.logFile("Deleting file 'activation.json'");
                            File.Delete(appdataFolder + "/subathontool/activation.json");
                            writeFile.logFile("File 'activation.json' successfully deleted");
                            
                        }
                    }catch(Exception err)
                    {
                        
                        writeFile.logFile("ERROR: Couldn't delete 'activation.json' - error encountered: " + err);
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("An error was encountered while trying to set the activation details. " + Environment.NewLine + Environment.NewLine + error, "An Error occurred setting activation details.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writeFile.logFile("Exception caught while setting registry value: " + error);
                    
                }
            }
            else
            {
                try
                {
                    activatewindow.Close();
                    this.Hide();
                    MessageBox.Show("Oops! For first load, the Subathon Tool needs to be running in administrator mode. Hold on!", "Oops! Permissions required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Process newSubathon = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "subathontool.exe";
                    startInfo.Verb = "runas";
                    newSubathon.StartInfo = startInfo;
                    newSubathon.Start();
                }
                catch (System.ComponentModel.Win32Exception err)
                {
                    writeFile.logFile("Couldn't start subathontool.exe as admin - " + err);
                }
                Application.Exit();

            }
            
        }


        private void ActivateBtn_Click(object sender, EventArgs e)
        {
            var toSend = $"{activatewindow.username.Text}&{activatewindow.activationCode.Text}";
            var md5Send = MD5H(toSend);
            if (!activatewindow.activationBrow.Address.Contains("login.php?"))
            {
                activatewindow.activationBrow.Load("https://dev.themadgamers.co.uk/application/subtool/login.php?name=" + activatewindow.username.Text + "&code=" + md5Send);
            }

            Console.WriteLine("loaded to url - " + activatewindow.activationBrow.Address);

        }

        private void loadStateChange(object sender, CefSharp.AddressChangedEventArgs e)
        {
            Console.WriteLine(e.Address);
        }

        private void activateDirect(object sender, CefSharp.AddressChangedEventArgs e)
        {
            Console.WriteLine("[1]" + e.Address);
            if (checkingReActivation != true && hasActivatedOnce != true)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object, CefSharp.AddressChangedEventArgs>(activateDirect), sender, e);
                    return;
                }
                else
                {

                    Console.WriteLine("URL changed - " + e.Address);
                    if (e.Address.Contains("?state=success"))
                    {

                        if (e.Address.Contains("=active"))
                        {
                            this.Show();
                            writeActivationFile();
                            activatedUsername = activatewindow.username.Text;
                            setUsernames(activatewindow.username.Text);
                            activatewindow.Hide();
                            MessageBox.Show("Mike's Subathon Tool has been successfully activated. Thank you for your support in the development of the Subathon Tool and its services. " + Environment.NewLine + "If you require any assistance, feel free to contact the team on Twitter @MadGamerBot - or alternatively you can join the Subathon Tool support Discord channel here: n9WxdDF", "[Beta] Activation Successful.");
                            hasActivatedOnce = true;
                            setTitle();
                        }
                        else if (e.Address.Contains("=beta"))
                        {
                            activatewindow.Show();
                            activatewindow.invalidCodeLbl.Visible = true;
                            activatewindow.invalidCodeLbl.Text = "Your currently activated key is a beta key. Please purchase the Subathon Tool to upgrade. If you have already, please contact the support team and we'll look into the issue. ";
                            this.Hide();

                            Console.WriteLine("User registered a beta key.");
                        }
                        else if (e.Address.Contains("=inactive"))
                        {
                            activatewindow.Show();
                            activatewindow.invalidCodeLbl.Visible = true;
                            activatewindow.invalidCodeLbl.Text = "Activation Failed. Your key is currently inactive. Please contact support.";
                            this.Hide();
                        }
                        else if (e.Address.Contains("=revoked"))
                        {
                            activatewindow.Show();
                            activatewindow.invalidCodeLbl.Visible = true;
                            activatewindow.invalidCodeLbl.Text = "Activation Failed. Your key has either been revoked, or is otherwise invalid.";
                            this.Hide();
                        }
                    }
                    else
                    {
                        activatewindow.Show();
                        activatewindow.invalidCodeLbl.Visible = true;
                        activatewindow.invalidCodeLbl.Text = "Activation Failed. If you haven't, please purchase a License or contact support.";
                        this.Hide();
                    }
                }
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Settings will appear here!");
            if (!settingsForm.Visible)
            {
                settingsForm.Show();
                settingsForm.BackColor = Color.White;
            }
            else
            {
                settingsForm.BringToFront();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public string convertToTime(int value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);
            // string str = time.ToString(@"hh\:mm\:ss");
            string hoursStartsWith = null;
            string minsStartsWith = null;
            string secsStartsWith = null;
            if (time.TotalHours < 10)
            {
                hoursStartsWith = "0";
            }
            else
            {
                hoursStartsWith = null;
            }
            if (time.Minutes < 10)
            {
                minsStartsWith = "0";
            }
            else
            {
                minsStartsWith = null;
            }
            if (time.Seconds < 10)
            {
                secsStartsWith = "0";
            }
            else
            {
                secsStartsWith = null;
            }
            string str = string.Format("{0}:{1}:{2}", hoursStartsWith + time.TotalHours.ToString().Split((char)'.')[0], minsStartsWith + time.Minutes, secsStartsWith + time.Seconds);
            return str;
        }
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public void raiseToast()
        {
            try
            {

                trayIcon.BalloonTipTitle = "Subathon Tool - Time is up!";
                trayIcon.BalloonTipText = "The countdown for the Subathon is over!";
                trayIcon.ShowBalloonTip(4000);

            } catch (Exception err)
            {
                writeFile.logFile("Encountered error displaying notification - " + err);
            }
        }
        int uptimeInc = 0;
        void uptime_Tick(object sender, EventArgs e)
        {
            TimeSpan time = TimeSpan.FromSeconds(uptime);
            string hoursStartsWith = null;
            string minsStartsWith = null;
            string secsStartsWith = null;
            uptimeInc += 1;
            if(uptimeInc == 20)
            {
                uptimeInc = 0;
                writeFile.writeUptime(uptime);
                writeFile.writeCountdown(timeLeft);
            }
            if (time.TotalHours < 10)
            {
                hoursStartsWith = "0";
            }
            else
            {
                hoursStartsWith = null;
            }
            if (time.Minutes < 10)
            {
                minsStartsWith = "0";
            }
            else
            {
                minsStartsWith = null;
            }
            if (time.Seconds < 10)
            {
                secsStartsWith = "0";
            }
            else
            {
                secsStartsWith = null;
            }
            uptime = uptime + 1;
            var hoursToDisplay = time.TotalHours.ToString().Split((char)',')[0];
            string str = string.Format("{0}:{1}:{2}", hoursStartsWith + hoursToDisplay.Split((char)'.')[0], minsStartsWith + time.Minutes, secsStartsWith + time.Seconds);
            uptimeLabel.Text = str;
            if (Directory.Exists(userDocsFolder + "/subathontool") == true)
            {
                writeFile.updateCountdown(userDocsFolder + "/subathontool/uptime.txt", str);
            }
            else
            {
                Directory.CreateDirectory(userDocsFolder + "/subathontool");
            }
        }
        void subathonCount_Tick(object sender, EventArgs e)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            string hoursStartsWith = null;
            string minsStartsWith = null;
            string secsStartsWith = null;
            if (time.TotalHours < 10)
            {
                hoursStartsWith = "0";
            }
            else
            {
                hoursStartsWith = null;
            }
            if (time.Minutes < 10)
            {
                minsStartsWith = "0";
            }
            else
            {
                minsStartsWith = null;
            }
            if (time.Seconds < 10)
            {
                secsStartsWith = "0";
            }
            else
            {
                secsStartsWith = null;
            }
            var hourTimeToDisplay = time.TotalHours.ToString().Split((char)',');
            string str = string.Format("{0}:{1}:{2}", hoursStartsWith + hourTimeToDisplay[0].Split((char)'.')[0], minsStartsWith + time.Minutes, secsStartsWith + time.Seconds);
            if (timeLeft > 0)
            {
                isCounting = true;
                timeLeft = timeLeft - 1;
                countdownTextBx.Text = timeLeft + " seconds" + Environment.NewLine + str + Environment.NewLine + "Total New Subs: " + totalAddedSubs + Environment.NewLine + "Total Added Re-subs: " + totalAddedResubs + Environment.NewLine + "Total Added Bits: " + totalAddedBits;
                if (Directory.Exists(userDocsFolder + "/subathontool") == true)
                {
                    writeFile.updateCountdown(userDocsFolder + "/subathontool/countdown.txt", str);
                    writeFile.subValue(totalAddedSubs);
                    writeFile.resubValue(totalAddedResubs);
                }
                else
                {
                    Directory.CreateDirectory(userDocsFolder + "/subathontool");
                }
                hasTimerFinished = false;
            }
            else
            {
                try
                {
                    raiseToast();
                    hasTimerFinished = true;

                }
                catch (Exception err)
                {
                    writeFile.logFile("Encountered error running Toast notification function:  " + err);
                }


                subathonCount.Stop();
                writeFile.updateCountdown(userDocsFolder + "/subathontool/countdown.txt", "00:00:00");
                try
                {
                    if (useChatCommands)
                    {
                        sendMessage($"[Subathon] The Subathon timer is up! Here's an overview of the stream!: Total from New Subs: {totalAddedSubs} | Total from Re-subs: {totalAddedResubs} | Total Giveaways: {readGiveawayCount()}");
                    }
                }
                catch (Exception err)
                {
                    writeFile.logFile("Encountered error while attempting to send message. " + err);
                    Console.WriteLine("Encountered error while attempting to send message. " + err);
                }
                try
                {
                    if (playSoundFile != null && playSoundFile != "" && playSoundOnFinish != false)
                    {
                        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
                        player.URL = playSoundFile;

                        player.controls.play();


                    }
                    else
                    {
                        writeFile.logFile("Play Sound location appears to be null - skipping sound play. - " + playSoundFile);
                    }


                }
                catch (Exception error)
                {
                    writeFile.logFile("Encountered error playing file '" + playSoundFile + "' on timer end | " + error);
                }

                beginCountDown.Enabled = true;
                cancelCountDown.Enabled = false;
                timeLeft = 0;
                isCounting = false;
                countdownTextBx.Text = "";


            }
        }
        public string readGiveawayCount()
        {
            try
            {
                var giveawayCount = File.ReadAllText(userDocsFolder + "/subathontool/giveawaycount.txt");
                return giveawayCount;
            }
            catch (FileNotFoundException err)
            {
                writeFile.logFile($"Encountered error while checking giveaway count {err}");
                return "0";
            }
            catch (Exception error)
            {
                writeFile.logFile($"Encountered error while checking giveaway count {error}");
                return "FUNC ERROR";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int value;

            cancelCountDown.Enabled = true;

            if (int.TryParse(beginningTimeTxt.Text, out value) && beginningTimeTxt.Text != null)
            {
                if (!hasCrashDetected)
                {
                    timeLeft = value;
                }
                // MessageBox.Show(value.ToString());
                subathonCount.Start();
                callViewersCheck(0);
                beginCountDown.Enabled = false;
                chatBox.Visible = true;
                twitchChat.Start();
                if (!isConnected)
                {
                    writeFile.logFile("Sending connection to chat");
                    reconnect(toJoin);
                }
                else
                {
                    writeFile.logFile("[WARN] : Ignoring connection; the IRC connection is already established.");
                }

            }
            else
            {
                beginningTimeTxt.ForeColor = Color.Red;
                error.Visible = true;
                writeFile.logFile("[ERROR] : Specified value isn't a number. Connection cancelled.");
            }




        }
        bool toggleAddTime = false;
        void manAdd_Click(object sender, EventArgs e)
        {
            
            Console.WriteLine("updating mainwindow timer");
            if (toggleAddTime)
            {
                countdownGroup.Size = new Size(214, 123);
                gamewispGroup.Visible = true;
                toggleAddTime = false;
                addTimePanel.Visible = false;
                removeTimePanel.Visible = false;
            }else
            {
                countdownGroup.Size = new Size(405, 123);
                gamewispGroup.Visible = false;
                addTimePanel.Visible = true;
                removeTimePanel.Visible = false;
                toggleAddTime = true;
            }
        }

        private void manRemove_Click(object sender, EventArgs e)
        {
            
            if (toggleAddTime)
            {
                countdownGroup.Size = new Size(214, 123);
                gamewispGroup.Visible = true;
                toggleAddTime = false;
                removeTimePanel.Visible = false;
                addTimePanel.Visible = false;
            }else
            {
                countdownGroup.Size = new Size(405, 123);
                gamewispGroup.Visible = false;
                removeTimePanel.Visible = true;
                addTimePanel.Visible = false;
                toggleAddTime = true;
            }
        }
        string totalAddedSubs = "00:00:00";
        int addedSubs;

        string totalAddedResubs = "00:00:00";
        int addedResubs;

        string totalAddedTips = "00:00:00";
        int addedTips;

        string totalAddedBits = "00:00:00";
        int addedBits;
        public void addTime(int value, string type)
        {
            if (type == "sub")
            {
                addedSubs = addedSubs + value;
                totalAddedSubs = convertToTime(addedSubs);
            }
            else if (type == "resub")
            {
                addedResubs = addedResubs + value;
                totalAddedResubs = convertToTime(addedResubs);
            }
            else if (type == "bits")
            {
                addedBits = addedBits + value;
                totalAddedBits = convertToTime(addedBits);
            }
            else if(type == "tip")
            {
                addedTips = addedTips + value;
                totalAddedTips = convertToTime(addedTips);
            }
            timeLeft = timeLeft + value;
            Console.WriteLine("updating mainwindow timer - : " + value);


        }
        public bool isCountingDown()
        {
            if (timeLeft <= 0 && subathonCount.Enabled != true)
            {
                return false;
            } else
            {
                return true;
            }

        }

        public void exitSubathon(object sender, EventArgs e)
        {
            this.Close();
            try
            {
                activatewindow.activationBrow.Dispose();
                activatewindow.Close();
            }
            catch (Exception err)
            {
                writeFile.logFile("Encountered error trying to dispose of the browser object. " + err);
            }
            

            writeFile.logFile("Exiting software. Closing background processes.");
            //Bits.disconnect();
            try
            {
                f.softwareExit = true;
                f.Close();

            }
            catch(Exception err)
            {
                writeFile.logFile("Encountered error exiting F - " + err);
            }
            try
            {
                clear.exit();
            }catch(Exception err)
            {
                writeFile.logFile("Encountered error attempting to close the clearLog window - " + err);
            }
            Dispose();
            writeFile.logFile(" ");
        }
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            storeOptions.Dispose();
            writeFile.writeUptime(0);
            
            if (isCountingDown())
            {
                DialogResult result = MessageBox.Show("It appears you're mid-way through a Subathon! If you exit now, the timer will reset and you'll have to start again. Are you sure you wish to exit? ", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Bits.disconnect(0, "Client is shutting down.");
                    trayIcon.Visible = false;
                    writeFile.logFile("Closing Twitch chat window.");
                    f.softwareExit = true;
                    f.Close();
                    writeFile.logFile("Chat Window closed successfully.");
                    writeFile.logFile("Clearing text file.");
                    try
                    {
                        writeFile.resubValue("");
                        writeFile.subValue("");
                        writeFile.writeGiveaways("0");
                        writeFile.updateCountdown(userDocsFolder + "/subathontool/countdown.txt", "");
                        writeFile.logFile("Successfully cleared text file. ");
                        writeFile.writeCountdown(0);
                    }
                    catch (Exception err)
                    {
                        writeFile.logFile("Encountered error clearing text file: " + err);
                    }
                    writeFile.logFile(" ");
                }
                else
                {
                    e.Cancel = true;
                }
            }else
            {
                Bits.disconnect(0, "Client is shutting down.");
                trayIcon.Visible = false;
                writeFile.logFile("Closing Twitch chat window.");
                f.softwareExit = true;
                f.Close();
                writeFile.logFile("Chat Window closed successfully.");
                writeFile.logFile("Clearing text file.");
                try
                {
                    writeFile.resubValue("");
                    writeFile.subValue("");
                    writeFile.writeGiveaways("0");
                    writeFile.updateCountdown(userDocsFolder + "/subathontool/countdown.txt", "");
                    writeFile.logFile("Successfully cleared text file. ");
                    writeFile.writeCountdown(0);
                }
                catch (Exception err)
                {
                    writeFile.logFile("Encountered error clearing text file: " + err);
                }
                writeFile.logFile(" ");
                Application.Exit();
            }


        }
        about about = new about();
        private void giveawayBtn_Click(object sender, EventArgs e)
        {
            // giveawayLoad.Start();
            giveaway g = new giveaway(this);
            g.Show();
        }

        private void beginningTimeTxt_Click(object sender, EventArgs e)
        {
            beginningTimeTxt.ForeColor = Color.Black;
            error.Visible = false;
        }

        private void beginningTimeTxt_TextChanged(object sender, EventArgs e)
        {
            beginningTimeTxt.ForeColor = Color.Black;
            error.Visible = false;
        }

        private void cancelCountDown_Click(object sender, EventArgs e)
        {
            countdownTextBx.Text = "";

            int value;
            if (int.TryParse(beginningTimeTxt.Text, out value) && beginningTimeTxt.Text != null)
            {
                subathonCount.Stop();
                timeLeft = 0;
                
                beginCountDown.Enabled = false;
            }
            else
            {
                beginningTimeTxt.ForeColor = Color.Red;
                error.Visible = true;
            }
            beginCountDown.Enabled = true;
            cancelCountDown.Enabled = false;
        }
        private int checkSubValue()
        {
            try
            {
                if (Directory.Exists(appdataFolder + "/subathontool"))
                {
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());
                        int value;

                        if (data2.subscriberAddsValue != null)
                        {
                            return data2.subscriberAddsValue;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(appdataFolder + "/subathontool");
                    return 0;
                }
                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));


            }
            catch (Exception error)
            {
                writeFile.logFile("Caught Exception while reading JSON file: "
                    + error);
                return 0;
                //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string checkGamingMode()
        {
            try
            {
                if (Directory.Exists("data"))
                {
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());

                        if (data2.gamingMode == true)
                        {
                            return "true";
                        }
                        else
                        {
                            return "false";
                        }
                    }
                }
                else
                {
                    return "false";
                }
                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));


            }
            catch (Exception error)
            {
                writeFile.logFile("Caught Exception while reading JSON file: "
                    + error);
                return "false";

                //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void checkGamewisp()
        {
            if (useGamewisp)
            {
                gamewisp.connect();
            } else
            {
                writeFile.logFile("Gamewisp options are disabled - skipping.");
            }
        }
        private void checkJsonSettings() //Checks the settings file in the appdata folder for the countdown value. 
        {
            try
            {
                if (Directory.Exists(appdataFolder + "/subathontool"))
                {
                    writeFile.logFile("AppData folder found - reading settings.json file now");
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());
                        //  int value;
                        writeFile.logFile("Found JSON data: " + data2);
                        if (data2.defaultCountdownValue != null)
                        {
                            beginningTimeTxt.Text = data2.defaultCountdownValue.ToString();
                        }
                        if (data2.channeltojoin != null || data2.channeltojoin != "")
                        {
                            toJoin = data2.channeltojoin;
                        }
                        if (data2.useTwitchDetails)
                        {
                            username = data2.twitchUsername;
                            password = data2.userOAuth;
                            Console.WriteLine("Found twitch settings - setting username to " + username);
                        }
                        else
                        {
                            username = "justinfan3456";
                            password = "Kappa123";
                            Console.WriteLine("Detected useTwitch false - setting username to justinfan3456");
                            writeFile.logFile("No twitch account was provided - setting username to justinfan3456");
                        }
                        OpensNewWindow = data2.chatOpensNewWindow;
                        Console.WriteLine("Found channel " + toJoin + " - " + data2.channeltojoin);
                        reSubValue = data2.resubsAddValue;
                        newSubValue = data2.subscriberAddsValue;
                        isGamingModeActive = data2.gamingMode;
                        useChatCommands = data2.useChatCommands;
                        resetUptime = data2.resetUptime;
                        announceNewSub = data2.announceNewSub;
                        announceResub = data2.announceReSub;
                        announceCheers = data2.announceBits;
                        newSubAddTime = data2.subscriberAddsValue;
                        useGamewisp = data2.allowGamewisp;
                        useMultiplier = data2.enableResubMultiplier;
                        multiplyAmount = data2.multiplierAmount;
                        getAPIDetails(data2.channeltojoin.Substring(1));
                        useBitsAdd = data2.useBitsAddTime;
                        bitsAdd = data2.eachBitAdds;
                        useDonationCmd = data2.useDonationCmd;
                        tipsAdd = data2.tipsAdd;
                        channelForGiveaway = data2.channelforgiveaway;
                        maxNumber = data2.maxNumberRNG;
                        allowStaffControl = data2.allowStaffControl;
                        allowLogMaintenance = data2.allowLogMaintenance;
                        // Check which index was selected from the Round seconds dropdown.
                        // 0 = round up (first option in drop down)
                        // 1 = round down (second option in drop down)
                        if (data2.roundSecondsIndex == 0)
                        {
                            roundUp = true;
                        }
                        else
                        {
                            roundUp = false;
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(appdataFolder + "/subathontool");
                    writeFile.logFile("Detected appdata folder doesn't exist - creating now.");
                }
                
            }
            catch (Exception error)
            {
                writeFile.logFile("Caught Exception while reading JSON file: "
                    + error);
                Console.Write(error + Environment.NewLine);
            }
            pictureBox1.Image = global::subathontool.Properties.Resources.tmg_load;

        }
        void activationCheckAddress(object sender, CefSharp.AddressChangedEventArgs e)
        {
            Console.WriteLine("[2]" + e.Address);
            if (InvokeRequired)
            {
                Invoke(new Action<object, CefSharp.AddressChangedEventArgs>(activationCheckAddress), sender, e);
                return;
            }
            else
            {

                if (e.Address.Contains("?state=success"))
                {

                    
                    if (e.Address.Contains("=inactive"))
                    {
                        activatewindow.Show();
                        activatewindow.invalidCodeLbl.Visible = true;
                        activatewindow.invalidCodeLbl.Text = "Activation Failed. Your key is currently inactive. if you believe this is an error, please contact support.";
                        this.Hide();
                    }
                    else if (e.Address.Contains("=revoked"))
                    {
                        activatewindow.Show();
                        activatewindow.invalidCodeLbl.Visible = true;
                        activatewindow.invalidCodeLbl.Text = "Activation Failed. Your key has either been revoked, or is otherwise invalid.";
                        this.Hide();
                    }
                    else if (e.Address.Contains("=beta"))
                    {
                        activatewindow.Show();
                        activatewindow.invalidCodeLbl.Visible = true;
                        activatewindow.invalidCodeLbl.Text = "Your currently activated key is a beta key. Please purchase the Subathon Tool to upgrade. If you have already, please contact the support team and we'll look into the issue. ";
                        this.Hide();
                    }
                    else
                    {
                        if(registryActivated != "true" || registryKey == "null" || registryUser == "null")
                        {
                            writeActivationFile();

                        }
                        this.Show();
                        activatewindow.Hide();
                        Console.WriteLine("[3] " + activatedUsername + " | " + attemptedName);
                        activatedUsername = attemptedName;
                        Console.WriteLine("[3] " + activatedUsername + " | " + attemptedName);
                        Console.WriteLine("User has a paid version.");
                    }
                    setTitle();
                }
                else if (e.Address.Contains("?state=failure"))
                {
                    activatewindow.Show();
                    this.Hide();
                    MessageBox.Show("We couldn't verify your activation. Please re-enter the details or contact support. ", "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        string activatedUsername = "";
        string attemptedName = "";
        private void checkActivation() {

            try
            {
                
                if (registryActivated != "true")
                {
                    if (IsAdministrator())
                    {
                        checkingReActivation = false;
                        activatewindow.Show();
                        activatewindow.invalidCodeLbl.Visible = false;
                        waitForLoad.Start();
                        
                    }
                    else
                    {
                        try
                        {
                            activatewindow.Close();
                            this.Hide();
                            Process newSubathon = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = "subathontool.exe";
                            startInfo.Verb = "runas";
                            newSubathon.StartInfo = startInfo;
                            newSubathon.Start();
                        }
                        catch (System.ComponentModel.Win32Exception err)
                        {
                            writeFile.logFile("Couldn't start subathontool.exe as admin - " + err);
                        }
                        Application.Exit();
                    }
                }
                else
                {
                    
                    Timer waitTimer = new Timer();
                    waitTimer.Interval = 100;

                    checkingReActivation = true;
                    var md5code = MD5H(registryUser + "&" + registryKey);
                    activatewindow.username.Text = registryUser;
                    activatewindow.activationCode.Text = registryKey;

                    activatewindow.Show();

                    waitTimer.Tick += (Sender, E) =>
                    {
                        waitTimer.Stop();

                        activatewindow.activationBrow.AddressChanged += activationCheckAddress;

                        if (!activatewindow.activationBrow.Address.Contains("login.php?"))
                        {
                            activatewindow.loadBrowser("https://dev.themadgamers.co.uk/application/subtool/login.php?name=" + registryUser + "&code=" + md5code);
                            Console.WriteLine("[1]" + activatedUsername + " | " + attemptedName);
                            attemptedName = registryUser;
                            Console.WriteLine("[1]" + activatedUsername + " | " + attemptedName);
                        }

                        activatewindow.Hide();
                    };
                    waitTimer.Start();

                }




            }
            catch (Exception error)
            {
                writeFile.logFile("Caught Exception while reading JSON file: " + error);
                Console.Write("Caught Exception while reading JSON file: " + error + Environment.NewLine);
                if (IsAdministrator())
                {
                    checkingReActivation = false;
                    activatewindow.Show();
                    activatewindow.invalidCodeLbl.Visible = false;
                    waitForLoad.Start();

                }else
                {
                    try
                    {
                        activatewindow.Close();
                        this.Hide();
                        Process newSubathon = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "subathontool.exe";
                        startInfo.Verb = "runas";
                        newSubathon.StartInfo = startInfo;
                        newSubathon.Start();
                    }
                    catch(System.ComponentModel.Win32Exception err)
                    {
                        writeFile.logFile("Couldn't start subathontool.exe as admin - " + err);
                    }
                    Application.Exit();
                }
            }
        }



        public class data
        {
            public string channeltojoin { get; set; }
            public string channelforgiveaway { get; set; }
            public string timerCompleteSoundFile { get; set; }
            public bool timerCompletePlay { get; set; }
            public bool checkViewerList { get; set; }
            public bool refreshViewerSound { get; set; }
            public int defaultCountdownValue { get; set; }
            public int subscriberAddsValue { get; set; }
            public bool gamingMode { get; set; }
            public int resubsAddValue { get; set; }
            public bool enableSongRequest { get; set; }
            public string[] regularsList { get; set; }
            public bool useTwitchDetails { get; set; }
            public string twitchUsername { get; set; }
            public string userOAuth { get; set; }
            public bool chatOpensNewWindow { get; set; }
            public bool useChatCommands { get; set; }
            public bool announceNewSub { get; set; }
            public bool announceReSub { get; set; }
            public bool announceBits { get; set; }
            public bool useDonationCmd { get; set; }
            public int maxNumberRNG { get; set; }
            public bool allowGamewisp { get; set; }
            public bool enableResubMultiplier { get; set; }
            public int multiplierAmount { get; set; }
            public bool useBitsAPI { get; set; }
            public string bitsOAuth { get; set; }
            public bool useBitsAddTime { get; set; }
            public decimal eachBitAdds { get; set; }
            public int tipsAdd { get; set; }
            public int roundSecondsIndex { get; set; }
            public bool resetUptime { get; set; }
            public bool allowStaffControl { get; set; }
            public bool allowLogMaintenance { get; set; }
        }
        public class activation {
            public bool isActivated { get; set; }
            public string activatedTo { get; set; }
            public string activationKey { get; set; }
        }
        public class gamewispAccount
        {
            public string channel { get; set; }
            public string OAuth { get; set; }
            public string refreshToken { get; set; }
            public int tierAmount { get; set; }
            public string[] tiers { get; set; }
        }




        private void feedbackBtn_Click(object sender, EventArgs e)
        {
            if (feedback.Visible)
            {
                feedback.BringToFront();
            }
            else
            {
                feedback.Show();
            }

            this.ShowIcon = true;
        }

        private void checkDocumentsFolder() // Checks the documents folder and fetches the location of the sound to play on timer complete.
        {
            if (!Directory.Exists(userDocsFolder + "/subathontool/sounds"))
            {
                writeFile.logFile("Folder doesn't exist in user's documents - creating now. [" + userDocsFolder + "]");
                Directory.CreateDirectory(userDocsFolder + "/subathontool/sounds");
            }
            else
            {
                if (File.Exists(appdataFolder + "/subathontool/settings.json"))
                {
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());

                        writeFile.logFile("Found JSON data: " + data2);
                        if (data2.timerCompleteSoundFile != null && data2.timerCompleteSoundFile != "")
                        {
                            playSoundFile = data2.timerCompleteSoundFile;
                        }
                        playSoundOnFinish = data2.timerCompletePlay;
                    }
                }
            }



        }

        private void SubscriberSR_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void moderatorSR_CheckedChanged(object sender, EventArgs e)
        {
            if (moderatorSR.Checked)
            {
                SubscriberSR.Checked = false;
                noLimitSR.Checked = false;
                regularsSR.Checked = false;

            }
        }

        private void regularsSR_CheckedChanged(object sender, EventArgs e)
        {
            if (regularsSR.Checked)
            {
                moderatorSR.Checked = false;
                noLimitSR.Checked = false;
                SubscriberSR.Checked = false;

            }
        }

        private void noLimitSR_CheckedChanged(object sender, EventArgs e)
        {
            if (SubscriberSR.Checked)
            {
                moderatorSR.Checked = false;
                SubscriberSR.Checked = false;
                regularsSR.Checked = false;

            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!SubscriberSR.Enabled || !moderatorSR.Enabled)
            {
                MessageBox.Show("This is a planned feature, but currently is a while off being implemented. For now, you'll have to use the new Regulars feature in the settings window." + Environment.NewLine + "Sorry for any inconveniences caused.", "Feature Unavailable!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void checkCodes()
        {


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            checkCodes();
        }
        bool firstShow = true;
        private void hideForm()
        {

            this.Hide();
        }

        private void waitForLoad_Tick(object sender, EventArgs e)
        {
            waitForLoad.Stop();
            this.Hide();
        }
        private void hideChangelog()
        {
            changelogView.Visible = false;
            streamStatusPct.Location = new Point(
                streamStatusPct.Location.X,
                streamStatusPct.Location.Y - changelogView.Height
                );
            streamStatusLbl.Location = new Point(
                 streamStatusLbl.Location.X,
                 streamStatusLbl.Location.Y - changelogView.Height
                 );
            mainTabs.Size = new Size(mainTabs.Width, mainTabs.Height + 22);
            tabControl1.Size = new Size(tabControl1.Width, tabControl1.Height + 22);
            saveSettings.Location = new Point(
                saveSettings.Location.X,
                saveSettings.Location.Y + 22
                );
            panel3.Size = new Size(panel3.Size.Width, panel3.Size.Height + 22);
            //mainTabsPanel.Location = new Point(mainTabsPanel.Location.X, mainTabsPanel.Location.Y - 22);
            //Console.WriteLine($"Main Tabs size: {mainTabs.Location} \n Panel Size: {mainTabsPanel.Location} \n Form size: {this.Size}");
        }
        private void closeLabl_Click(object sender, EventArgs e)
        {
            hideChangelog();
            File.WriteAllText(appdataFolder + "/subathontool/change.subtool", "false");
            // groupBox1.Location = groupBox1.Location - changelogView.Height;
        }
        changelog changelog = new changelog();
        private void changelogView_Click(object sender, EventArgs e)
        {
            hideChangelog();
            File.WriteAllText(appdataFolder + "/subathontool/change.subtool", "false");
            showChangelog();
        }

        private void updatedToChangelog_Click(object sender, EventArgs e)
        {
            hideChangelog();
            File.WriteAllText(appdataFolder + "/subathontool/change.subtool", "false");
            showChangelog();
        }
        private void showChangelog()
        {
            if (!changelog.Visible)
            {
                changelog.Show();
            }
            else
            {
                changelog.BringToFront();
                changelog.WindowState = FormWindowState.Normal;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //this.FormBorderStyle = FormBorderStyle.Sizable;
            
            this.ShowIcon = true;
            if (!hasUpdated)
            {
                statusStrip1.Visible = true;
                panel3.Visible = true;
            }
            pictureBox1.Visible = false;
            mainTabs.Visible = true;
            controlBorder.Visible = true;
            Console.WriteLine(chatBox.Size.Height + $" [{panel1.Size.Height}] - " + viewerList.Height);
            chatBox.Size = new Size(chatBox.Size.Width, 444);
            Console.WriteLine(chatBox.Size.Height + $" [{panel1.Size.Height}] - " + viewerList.Height);
        }
        private void reconnect(string channel)
        {
            connectionMessageReceived = false;
            connectionVerification.Start();
            writeFile.logFile("Connecting to servers...");

            tcpClient = new TcpClient("irc.chat.twitch.tv", 80);
            tcpClient.ReceiveTimeout = -1;
            isConnected = true;
            
            // userChatMsg.Enabled = true;
            // sendMsg.Enabled = true;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());


            writer.WriteLine("PASS " + password + Environment.NewLine
                + "NICK " + username + Environment.NewLine
                + "USER " + username + " 8 * :" + username);
            writer.Flush();
            Console.WriteLine("Logging in with username " + username);
            Console.WriteLine("Joining " + toJoin);
            writer.WriteLine("JOIN " + channel);
            chan = channel;
            writer.WriteLine("CAP REQ :twitch.tv/membership");
            writer.WriteLine("CAP REQ :twitch.tv/tags");
            writer.WriteLine("CAP REQ :twitch.tv/commands");
            writer.Flush();
			writeFile.logFile("Connected.");
        }
        void sendMessage(string message)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :{message}");
            writer.Flush();
            Console.WriteLine("Attempting to send message " + $":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG #{chan} :{message}");
        }
        public string lastMessage;
        public string lastSpeaker;
        
        private void twitchChat_Tick(object sender, EventArgs e)
        {
           // this.TopMost = false;
            chatBox.Height = 444;
            if (!tcpClient.Connected)
            {
                reconnect(toJoin);
                writeFile.logFile("Detected client disconnected - reconnecting.");
                isConnected = false;
                // userChatMsg.Enabled = false;
            }

            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                var message = reader.ReadLine();
                if (message.Contains("tmi.twitch.tv 001 justinfan") && message.Contains("Welcome, GLHF!"))
                {
                    connectionMessageReceived = true;
                    Console.WriteLine("Received connection message: ");
                    Console.WriteLine("[" + message + "]");
                }
                writeFile.logFile("[IRC]" + message);
                Console.WriteLine("[IRC] " + message);
                // string speaker = message.Substring(1, message.IndexOf("!") - 1);
                var iColon = message.IndexOf(":", 1);
                var userNotice = message.Split((char)59);
                bool isAdded = false;
                if (message.Contains(";"))
                {

                }
                
                if (message.Contains(":tmi.twitch.tv USERNOTICE"))
                {
                    char character = '\u2605';
                    var displayMsg = "";
                    for (var i = 0; i < userNotice.Length; i++)
                    {
                        displayMsg = userNotice[i];
                    }

                    var tier = userNotice[10].Substring(userNotice[10].IndexOf((char)61) + 1);

                    if (userNotice[7].Contains("resub"))
                    {
                        int months = 1;
                        int.TryParse(userNotice[8].Substring(userNotice[8].IndexOf((char)61) + 1), out months);

                        int multiplied = months * multiplyAmount;
                        int totalToAdd = reSubValue + multiplied;
                        var toShow = userNotice[4].Substring(userNotice[5].IndexOf((char)61) + 1) + " for subscribing for " + userNotice[8].Substring(userNotice[8].IndexOf((char)61) + 1) + " months in a row!";

                        if (announceResub)
                        {
                            sendMessage("[Subathon] Re-subscription detected! Welcome back, " + userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1) + " for " + userNotice[8].Substring(userNotice[8].IndexOf((char)61) + 1) + " months in a row.");
                        }

                        chatBox.Items.Add($"{(char)character} {userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1)} for {userNotice[8].Substring(userNotice[8].IndexOf((char)61) + 1)} months in a row!");
                        Console.Write($"User Re-subscribed with the following info: \n     Name: {userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1)} \n     Months: {userNotice[8].Substring(userNotice[8].IndexOf((char)61) + 1)} \n     Tier: {userNotice[10].Substring(userNotice[10].IndexOf((char)61) + 1)} \n ");

                        
                        
                        if(tier.ToLower() == "prime")
                        {
                            if (useMultiplier)
                            {
                                addTime(totalToAdd, "resub");
                                Console.WriteLine("Should add multiplier to re-sub value - Total to add: " + totalToAdd);
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is enabled. Time added should be: " + totalToAdd + " for Twitch Prime Tier. ");
                            }
                            else
                            {
                                addTime(reSubValue, "resub");
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is disabled. Time added should be: " + reSubValue + " for Twitch Prime Tier. ");
                            }
                            subsSum = subsSum + (decimal)4.99;

                        }
                        else if(tier == "1000")
                        {
                           
                            if (useMultiplier)
                            {
                                addTime(totalToAdd, "resub");
                                Console.WriteLine("Should add multiplier to re-sub value - Total to add: " + totalToAdd);
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is enabled. Time added should be: " + totalToAdd + " for Tier 1.");
                            }
                            else
                            {
                                addTime(reSubValue, "resub");
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is disabled. Time added should be: " + reSubValue + " for Tier 1");
                            }
                            subsSum = subsSum + (decimal)4.99;
                        }
                        else if(tier == "2000")
                        {
                            if (useMultiplier)
                            {
                                addTime(totalToAdd * 2, "resub");
                                Console.WriteLine("Should add multiplier to re-sub value - Total to add: " + totalToAdd);
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is enabled. Time added should be: " + totalToAdd + " multiplied by 2 for Tier 2. Value: " + (reSubValue * 2));
                            }
                            else
                            {
                                addTime(reSubValue * 2, "resub");
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is disabled. Time added should be: " + reSubValue + " multiplied by 2 for Tier 2. Value: " + (reSubValue * 2));
                            }
                            subsSum = subsSum + (decimal)9.99;
                        }
                        else if(tier == "3000")
                        {
                            if (useMultiplier)
                            {
                                addTime(totalToAdd * 6, "resub");
                                Console.WriteLine("Should add multiplier to re-sub value - Total to add: " + totalToAdd);
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is enabled. Time added should be: " + totalToAdd + " multiplied by 6 for Tier 3. Value: " + (totalToAdd * 6));
                            }
                            else
                            {
                                addTime(reSubValue * 6, "resub");
                                totalreSubs++;
                                writeFile.logFile("[DEBUG] : Multiplier is disabled. Time added should be: " + reSubValue + " multiplied by 6 for Tier 3. Value: " + (reSubValue * 6));
                            }
                            subsSum = subsSum + (decimal)24.99;
                        }
                        else
                        {
                            Console.WriteLine("Unknown Tier Value: " + tier);
                            writeFile.logFile("[ERROR] : could not add time for re-sub - the tier value [" + tier + "] is unknown. Please contact support." );
                        }
                        writeFile.writeTotalReSubs(totalreSubs.ToString());
                    }
                    else if (userNotice[7].Contains("sub"))
                    {
                        Console.WriteLine($"User {userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1)} just subscribed with the {tier} tier!");
                        if (tier.ToLower() == "prime")
                        {
                            Console.WriteLine($"[Prime] Sub should add: {newSubValue}");
                            writeFile.logFile("[DEBUG] : New Sub detected. Time added should be: " + newSubValue + " for Prime Tier.");
                            addTime(newSubValue, "sub");
                            totalSubs++;
                            writeFile.writeTotalSubs(totalSubs.ToString());
                            subsSum = subsSum + (decimal)4.99;
                        }
                        else if(tier == "1000")
                        {
                            Console.WriteLine($"[Standard] Sub should add: {newSubValue}");
                            writeFile.logFile("[DEBUG] : New Sub detected. Time added should be: " + newSubValue + " for Tier 1.");
                            addTime(newSubValue, "sub");
                            totalSubs++;
                            writeFile.writeTotalSubs(totalSubs.ToString());
                            subsSum = subsSum + (decimal)4.99;
                        }
                        else if(tier == "2000")
                        {
                            Console.WriteLine($"[Tier 2 - $9.99] Sub should add: {newSubValue * 2}");
                            writeFile.logFile("[DEBUG] : New Sub detected. Time added should be: " + (newSubValue * 2) + " for Tier 2.");
                            addTime(newSubValue * 2, "sub");
                            totalSubs++;
                            writeFile.writeTotalSubs(totalSubs.ToString());
                            subsSum = subsSum + (decimal)9.99;
                        }
                        else if(tier == "3000")
                        {
                            Console.WriteLine($"[Tier 3 - $24.99] Sub should add: {newSubValue * 6}");
                            writeFile.logFile("[DEBUG] : New Sub detected. Time added should be: " + (newSubValue * 6) + " for Tier 1.");
                            addTime(newSubValue * 6, "sub");
                            totalSubs++;
                            writeFile.writeTotalSubs(totalSubs.ToString());
                            subsSum = subsSum + (decimal)24.99;
                        }
                        else
                        {
                            Console.WriteLine("Unknown tier value: " + tier);

                            writeFile.logFile("[ERROR] : Unknown tier value [" + tier + "] on New Sub.");
                        }
                    }
                    else if (userNotice[7].ToLower().Contains("purchase"))
                    {

                    }
                    /**
         * 
         *@badges=subscriber/0,bits/100;
         * color=#FF1F1F;
         * display-name=Nervous_Habit;
         * emotes=151888:0-7;
         * id=b69b5ff7-873f-4d08-9a73-d6dea0db09f8;
         * login=nervous_habit;
         * mod=0;
         * msg-id=purchase;
         * msg-param-asin=B06XK192R7;
         * msg-param-channelID=35833485;
         * msg-param-imageURL=https://images-na.ssl-images-amazon.com/images/I/61dovyukBQL.jpg;
         * msg-param-title=Warframe\sProminence\sBundle;
         * msg-param-userID=69777126;
         * room-id=35833485;
         * subscriber=1;
         * system-msg=Purchased\sWarframe\sProminence\sBundle\sin\schannel.;
         * tmi-sent-ts=1491342820515;
         * turbo=0;
         * user-id=69777126;
         * user-type= :tmi.twitch.tv USERNOTICE #brozime :zimeGrin
         * **/
                }
                if (iColon > 0)
                {
                    var command = message.Substring(1, iColon);
                    if (command.Contains("PRIVMSG"))
                    {

                        var iBang = command.IndexOf("!");
                        if (iBang > 0)
                        {
                            var speaker = command.Substring(0, iBang);
                            if (message.Contains("just subscribed"))
                            {

                                if (speaker == "twitchnotify")
                                {
                                    char character = '\u2605';
                                    // MessageBox.Show("User just subscribed to channel!");

                                    totalMsg += 1;
                                    isAdded = true;
                                    Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                    if (message.ToLower().Contains("twitch prime"))
                                    {
                                        Console.WriteLine("Sub was Twitch prime. ");
                                    }
                                    
                                    var splitMsg = message.Split((char)32);
                                    
                                    Console.WriteLine("Subscriber detected, adding " + newSubValue);
                                    writeFile.logFile("New subscriber detected - " + message);
                                    chatBox.Items.Add($"{(char)character} {splitMsg[3].TrimStart((char)58)} subscribed!");
                                    if (announceNewSub)
                                    {
                                        sendMessage("[Subathon] New subscriber detected! Welcome, " + splitMsg[3].TrimStart((char)58));
                                    }
                                    
                                }
                                else
                                {
                                    writeFile.logFile("Fake subscription detected!");
                                }


                            }
                            else if (message.Contains("subscribed for"))
                            {

                                if (speaker == "twitchnotify")
                                {
                                    // MessageBox.Show("User just subscribed to channel!");
                                    char character = '\u2605';

                                    chatBox.Items.Add((char)character + "  " + message);
                                    totalMsg += 1;
                                    isAdded = true;
                                    Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                    
                                    var splitMsg = message.Split((char)32);
                                    int months = 1;
                                    int.TryParse(splitMsg[6], out months);

                                    int multiplied = months * multiplyAmount;
                                    int totalToAdd = reSubValue + multiplied;
                                    if (useMultiplier)
                                    {
                                        addTime(totalToAdd, "resub");
                                        totalSubs++;
                                    }
                                    else
                                    {
                                        addTime(reSubValue, "resub");
                                        totalSubs++;
                                    }
                                    

                                    totalreSubs++;
                                    writeFile.writeTotalReSubs(totalreSubs.ToString());
                                    Console.WriteLine("Re sub detected - Adding " + reSubValue);
                                    writeFile.logFile("Re-subscription detected - " + splitMsg[3] + "subscribed for " + splitMsg[6] + "months");
                                    if (announceResub)
                                    {
                                        sendMessage("[Subathon] Re-subscription detected! Welcome back, " + splitMsg[3].TrimStart((char)58) + " for " + splitMsg[6] + " months in a row.");
                                    }

                                }
                                else
                                {
                                    writeFile.logFile("Fake subscription detected!");
                                }


                            }

                            else if (message.Contains("!subathontool") || message.Contains(">subathontool") || message.Contains("`subathontool"))
                            {
                                if (useChatCommands && isConnected)
                                {
                                    sendMessage($"{chan} is currently using Mike's Subathon Tool [version {updateCheck.currentVersion()}] | The total subscribers are: {totalSubs} | The timer is currently running at: {File.ReadAllText(userDocsFolder + "/subathontool/countdown.txt")}");
                                }
                                else
                                {
                                    writeFile.logFile($"Detected {speaker} ran command subathontool, but Chat commands are disabled, or the client is currently disconnected. ");
                                }
                            }
                            else
                            {
                                /**
                        0    [0]   [IRC] @badges=subscriber/0;
                             [1]   color=#FF0000;
                             [2]   display-name=;
                             [3]   emotes=;
                             [4]   id=9fb1bb9b-8e83-491c-9029-0a85061d7e70;
                             [5]   login=janwaat;
                             [6]   mod=0;
                             [7]   msg-id=resub;
                             [8]   msg-param-months=11;
                             [9]   room-id=26610234;
                             [10]   subscriber=1;
                             [11]   system-msg=Janwaat\ssubscribed\sfor\s11\smonths\sin\sa\srow!;
                             [12]   tmi-sent-ts=1475762097292;
                             [13]   turbo=0;
                             [14]   user-id=7772638;
                             [15]   user-type= :tmi.twitch.tv USERNOTICE #cohhcarnage
                                
                                **/

                             /**   var Name = userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1);

                                var usertypeMsg = userNotice[12].Substring(userNotice[12].IndexOf((char)61) + 1);

                                var userMessage = userNotice[userNotice.Count() - 1].Substring(userNotice[userNotice.Count() - 1].IndexOf("USERNOTICE " + chan + " :" + 1));
                                isAdded = true;
                                totalMsg += 1;
                                listBox1.Items.Add($"{Name}: {userMessage}");
                                lastMessage = userMessage;
                                lastSpeaker = Name;
                                Console.WriteLine("success"); **/
                            }
                        }

                    }
                    else
                    {
                        if (message.Contains("PING"))
                        {
                            writer.Write(message.Replace("PING", "PONG") + "\r\n");
                            Console.WriteLine("Found PING message, sending PONG \n " + message);
                            writer.Flush();
                        }
                    }
                }
                //[IRC] @badges=moderator/1,subscriber/12,bits/1000;bits=1;color=#570080;display-name=MadMikeGamerXL1;emotes=25:26-30;id=359f7557-8454-418f-a8ab-0c5f946ec00f;mod=1;room-id=32775646;subscriber=1;tmi-sent-ts=1484159640054;turbo=0;user-id=49705491;user-type=mod :madmikegamerxl1!madmikegamerxl1@madmikegamerxl1.tmi.twitch.tv PRIVMSG #reninsane :cheer1 developer bits \o/ Kappa
                if (message.Contains(";"))
                {
                    try
                    {
                        if (userNotice[1].Contains("bits="))
                        {
                            var bitsAmount = userNotice[1].Substring(userNotice[1].IndexOf("=") + 1);
                            var userCheered = userNotice[3].Substring(userNotice[3].IndexOf("=") + 1);
                            char character = '\u2605';
                            Console.WriteLine("User cheered " + bitsAmount);
                            int bitsCheered = 0;
                            int.TryParse(bitsAmount, out bitsCheered);
                            chatBox.Items.Add((char)character + userCheered + " just cheered " + bitsAmount);
                            updateBitsBalance(userCheered, bitsCheered);
                            updateBitsDictionary();

                            if (useBitsAdd)
                            {
                                decimal totalToAdd = 0;
                                totalToAdd = bitsCheered * bitsAdd; // 5 * 
                                decimal roundedTotal = totalToAdd;
                                
                                if(roundUp) { roundedTotal = Math.Ceiling(totalToAdd); Console.WriteLine($"Rounded {totalToAdd} UP to {roundedTotal}"); }
                                else { roundedTotal = Math.Floor(totalToAdd); Console.WriteLine($"Rounded {totalToAdd} DOWN to {roundedTotal}"); }

                                addTime((int)roundedTotal, "bits");
                                decimal value = bitsCheered / 100;
                                bitsSum += value;
                                Console.WriteLine($"[VALUE] Updated Bits sum for {bitsCheered} bits - ${value}. The total from bits so far is: ${bitsSum}");
                                if (announceCheers)
                                {
                                    sendMessage($"[Subathon] {userCheered} just cheered {bitsAmount}, adding {totalToAdd} second(s) to the timer. [{File.ReadAllText(userDocsFolder + "/subathontool/countdown.txt")}]");
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("Encountered error trying to read bits message. " + err);
                        writeFile.logFile("Encountered error trying to read bits message. " + err);
                    }
                }
                //catch (System.IndexOutOfRangeException notFound) { }
                // catch (Exception err) { Console.WriteLine("Encountered error trying to read bits message. " + err); }

                /**
                 --BITS--
                [0]  @badges=subscriber/1,bits/10000;
                [1]  bits=300;
                [2]  color=#CC00BB;
                [3]  display-name=skellybeth;
                [4]  emotes=;
                [5]  id=84cc1938-e021-4c47-bbb7-cd29a37ee1bc;
                [6]  mod=0;
                [7]  room-id=38197393;
                [8]  subscriber=1;
                [9]  turbo=1;
                [10] user-id=91970736;
                [11] user-type= :skellybeth!skellybeth@skellybeth.tmi.twitch.tv PRIVMSG #buttonboy13 :cheer100 cheer100 cheer100 whoosh
                 **/
                if (!isAdded)
                {

                    if (message.Contains("@badges=") && message.Contains("PRIVMSG"))
                    {
                        Console.WriteLine("success");

                        var Name = userNotice[2].Substring(userNotice[2].IndexOf((char)61) + 1);

                        // var usertypeMsg = userNotice[12].Substring(userNotice[12].IndexOf((char)61) + 1);
                        int addOn = 11 + chan.Length;
                        //var userMessage = userNotice[11].Substring(userNotice[11].IndexOf("PRIVMSG #" + chan + " :") + addOn);
                        var userMessage = message.Split(' ');
                        List<string> messageString = new List<string>();
                        var count = userMessage.Count() - 3;
                        bool isTipCommand = false;
                        for (var i = 4; i < userMessage.Count();)
                        {
                            if (i == 4) {
                                var firstWord = userMessage[i].Substring(userMessage[i].IndexOf(":") + 1);
                                messageString.Add(firstWord);
                                i++;

                            }
                            else
                            {
                                messageString.Add(userMessage[i]);
                                i++;
                            }
                        }
                        var splitMsg = messageString;
                        if(messageString[0] == "!addtip" && messageString.Count > 2 )
                        {
                            Console.WriteLine(Name + " - " + toJoin.Substring(toJoin.IndexOf(("#")) + 1));

                            
                            if (userNotice[5].Contains("mod=1"))
                            {
                                Console.WriteLine("user is mod. Adding time - " + userNotice[5]);
                                decimal amount;
                                if (decimal.TryParse(messageString[2], out amount))
                                {
                                    chatBox.Items.Add('\u2605' + $" {messageString[1]} just tipped {messageString[2]}!");

                                    decimal totalToAdd = 0;
                                    totalToAdd = amount * tipsAdd; 
                                    decimal roundedTotal = totalToAdd;
                                    if (roundUp)
                                    {
                                        roundedTotal = Math.Ceiling(totalToAdd);
                                        Console.WriteLine($"Rounding {totalToAdd} tip up to: {roundedTotal}");
                                    }
                                    else
                                    {
                                        roundedTotal = Math.Floor(totalToAdd);
                                        Console.WriteLine($"Rounding {totalToAdd} tip down to {roundedTotal}");
                                    }
                                    addTime((int)roundedTotal, "tip");
                                    tipsSum += amount;
                                }
                            }
                            else if (Name.ToLower() == toJoin.Substring(toJoin.IndexOf(("#")) + 1).ToLower())
                            {
                                Console.WriteLine("user [" + Name +  "] is broadcaster. Adding time - " + userNotice[5]);
                                decimal amount;
                                if (decimal.TryParse(messageString[2], out amount))
                                {
                                    chatBox.Items.Add('\u2605' + $" {messageString[1]} just tipped {messageString[2]}!");

                                    decimal totalToAdd = 0;
                                    totalToAdd = amount * tipsAdd;
                                    decimal roundedTotal = totalToAdd;
                                    if (roundUp)
                                    {
                                        roundedTotal = Math.Ceiling(totalToAdd);
                                        Console.WriteLine($"Rounding {totalToAdd} tip up to: {roundedTotal}");
                                    }
                                    else
                                    {
                                        roundedTotal = Math.Floor(totalToAdd);
                                        Console.WriteLine($"Rounding {totalToAdd} tip down to {roundedTotal}");
                                    }
                                    addTime((int)roundedTotal, "tip");
                                    tipsSum += amount;
                                }
                            }
                            else if(Name.ToLower() == "madmikegamerxl1" || Name.ToLower() == "ttgimp" || Name.ToLower() == "themadgamersuk")
                            {
                                if (allowStaffControl)
                                {
                                    decimal amount;
                                    if (decimal.TryParse(messageString[2], out amount))
                                    {
                                        writeFile.logFile($"[Enabled] Staff member '{Name}' ran command !addtip {messageString[1]} {messageString[2]}");
                                        chatBox.Items.Add('\u2605' + $" {messageString[1]} just tipped {messageString[2]}!");

                                        decimal totalToAdd = 0;
                                        totalToAdd = amount * tipsAdd;
                                        decimal roundedTotal = totalToAdd;
                                        if (roundUp)
                                        {
                                            roundedTotal = Math.Ceiling(totalToAdd);
                                            Console.WriteLine($"Rounding {totalToAdd} tip up to: {roundedTotal}");
                                        }
                                        else
                                        {
                                            roundedTotal = Math.Floor(totalToAdd);
                                            Console.WriteLine($"Rounding {totalToAdd} tip down to {roundedTotal}");
                                        }
                                        addTime((int)roundedTotal, "tip");
                                        tipsSum += amount;
                                    }
                                }
                                else
                                {
                                    writeFile.logFile($"[Disabled] Staff member '{Name}' tried to add time, but staff permissions are disabled. Command: !addtip {messageString[1]} {messageString[2]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("User ["+ Name + "] is not mod: " + userNotice[5]);
                                writeFile.logFile("User wasn't mod. Failed to add tip to countdown. ");
                            }
                            
                        }
                        else if(messageString[0] == "!addtime" && messageString.Count > 1) 
                        {
                            /** Available formats:
                             * !addtime [secs]
                             * !addtime [type - bits] [amountofbits]
                             * !addtime [type - sub|resub]
                             * **/
                            string type = "none";
                            int amount;
                            int manualBits;
                            if (int.TryParse(messageString[1], out amount))
                            {
                                addTime(amount, type);
                                chatBox.Items.Add((char)'\u2605' + $"Time was added by {Name}: {amount}secs. No Type Provided");
                            }
                            else if(messageString[1].ToLower() == "bits" && int.TryParse(messageString[2], out manualBits))
                            {
                                type = "bits";

                                decimal totalToAdd = 0;
                                totalToAdd = manualBits * bitsAdd;
                                decimal roundedTotal = totalToAdd;

                                if (roundUp) { roundedTotal = Math.Ceiling(totalToAdd); Console.WriteLine($"Rounded {totalToAdd} UP to {roundedTotal}"); }
                                else { roundedTotal = Math.Floor(totalToAdd); Console.WriteLine($"Rounded {totalToAdd} DOWN to {roundedTotal}"); }
                                amount = (int)roundedTotal;
                                addTime(amount, type);
                                chatBox.Items.Add((char)'\u2605' + $"{Name} manually registered a cheer for {manualBits} bits, adding {amount}secs");
                            }
                            else if(messageString[1].ToLower() == "sub")
                            {
                                type = messageString[1].ToLower();
                                amount = newSubValue;
                                addTime(amount, type);
                                chatBox.Items.Add((char)'\u2605' + $"{Name} manually registered a sub with type: {type} adding {amount}secs.");
                            }
                            else if(messageString[1].ToLower() == "resub")
                            {
                                type = messageString[1].ToLower();
                                amount = reSubValue;
                                addTime(amount, type);
                                chatBox.Items.Add((char)'\u2605' + $"{Name} manually registered a sub with type: {type} adding {amount}secs.");
                            }

                        }
                        else if(messageString[0] == "!subathonstats")
                        {
                            if(useChatCommands && isConnected)
                            {
                                sendMessage($"The current Subathon stats are: Chat Lines: {totalLines} Total subs: {totalSubs} Total Re-subs: {totalreSubs} Total Giveaways: {readGiveawayCount()} Total time added (subs): {totalAddedSubs} Total added Re-subs: {totalAddedResubs} Total added Bits: {totalAddedBits}");
                            }
                        }
                        isAdded = true;
                        totalMsg += 1;
                        chatBox.Items.Add($"{Name}: {string.Join(" ", messageString.ToArray())}");
                        lastMessage = string.Join(" ", messageString.ToArray());
                        lastSpeaker = Name;
                        Console.WriteLine("success");

                        totalLines++;
                        chatPage.Text = $"Chat: [{totalLines}]";
                    }

                }



            }
        }
        public int totalLines;
        private string _selectedMenuItem;

        private void copyMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(chatBox.SelectedItem.ToString());
        }
        
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = chatBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                _selectedMenuItem = chatBox.Items[index].ToString();
                listBoxContext.Show(Cursor.Position);
                listBoxContext.Visible = true;
            }
            else
            {
                listBoxContext.Visible = false;
            }
        }
        private void ban(string user)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :.ban {user}");
            writer.Flush();

            Console.WriteLine($"{username}: Banned  {user}");
            writeFile.logFile($"{username}: Banned  {user}");
        }
        private void unban(string user)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :.unban {user}");
            writer.Flush();

            Console.WriteLine($"{username}: Unbanned  {user}");
            writeFile.logFile($"{username}: Unbanned  {user}");
        }
        private void mod(string user)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :.mod {user}");
            writer.Flush();

            Console.WriteLine($"{username}: Added {user} as moderator");
            writeFile.logFile($"{username}: Added {user} as moderator");
        }
        private void unmod(string user)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :.unmod {user}");
            writer.Flush();

            Console.WriteLine($"{username}: Removed {user} as moderator");
            writeFile.logFile($"{username}: Removed {user} as moderator");
        }
        private void timeout(string user, int time)
        {
            if (isConnected)
            {
                writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :.timeout {user} {time}");
                writer.Flush();

                Console.WriteLine($"{username}: Timed out {user} for {time} seconds.");
                writeFile.logFile($"{username}: Timed out {user} for {time} seconds.");

            }
        }
        private void timeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 600);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 600 seconds.");
        }
        string selectedUsername;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var iColon = chatBox.SelectedItem.ToString().IndexOf(":", 1);

                if (iColon > 0)
                {
                    var command = chatBox.SelectedItem.ToString().Substring(1, iColon);
                    if (command.Contains("PRIVMSG"))
                    {
                        var iBang = command.IndexOf("!");
                        if (iBang > 0)
                        {
                            var speaker = command.Substring(0, iBang);
                            selectedUsername = speaker;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                writeFile.logFile("Encountered error selecting listbox item. " + err);
            }




        }

        private void purgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 1);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 1 second(s).");
        }

        private void minuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 60);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 60 seconds.");
            
        }
        
        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 600);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 600 seconds.");
        }

        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 3600);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 3600 seconds.");
        }

        private void hoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeout(selectedUsername, 86400);
            Console.WriteLine("Sending timeout for " + selectedUsername + " for 86400 seconds.");
        }

        private void banToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ban(selectedUsername);
        }

        private void unmodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unmod(selectedUsername);
        }

        private void permitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                sendMessage($"!permit {selectedUsername}");
            }

        }

        private void unbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            unban(selectedUsername);
        }

        private void listBoxContext_Opening(object sender, CancelEventArgs e)
        {

        }
        /// giveaway g = new giveaway();
        private void giveawayLoad_Tick(object sender, EventArgs e)
        {

        }

        private void changelogView_Paint(object sender, PaintEventArgs e)
        {

        }
        public int welcomeStage = 0;
        private void welcomeTimer_Tick(object sender, EventArgs e)
        {
            int fadeSpeed = 3;

            // Console.WriteLine("Welcome stage : " + welcomeStage);
            welcomePanel.Visible = true;
            welcomePanel.Dock = DockStyle.Fill;
            copyright.Location = new Point(
                        620, 447);

            if (welcomeStage == 0)
            {
                if (!pictureBox1.Visible)
                {

                    welcomeUser.ForeColor = Color.FromArgb(welcomeUser.ForeColor.R - fadeSpeed, welcomeUser.ForeColor.G - fadeSpeed, welcomeUser.ForeColor.B - fadeSpeed);
                    if (welcomeUser.ForeColor.R >= this.BackColor.R || welcomeUser.ForeColor.R < 1)
                    {
                        welcomeTimer.Stop();
                        welcomeUser.ForeColor = Color.Black;
                        // welcomeUser.Visible = false;
                        welcomeStage++;
                        welcomeTimer.Start();
                    }
                }

            }
            else if (welcomeStage == 1)
            {
                if (!pictureBox1.Visible)
                {

                    welcomeUser.ForeColor = Color.FromArgb(welcomeUser.ForeColor.R + fadeSpeed, welcomeUser.ForeColor.G + fadeSpeed, welcomeUser.ForeColor.B + fadeSpeed);
                    if (welcomeUser.ForeColor.R >= this.BackColor.R)
                    {
                        // welcomeTimer.Stop();
                        twitchEmote.Visible = false;
                        welcomeUser.ForeColor = this.BackColor;
                        welcomeUser.Visible = false;
                        welcomeStage++;
                        welcomeUser.Text = "We've updated your experience...";

                        this.welcomeUser.Font = new Font("MS Reference Sans Serif", 19);

                        // welcomeTimer.Start();
                    }
                }
            }
            else if (welcomeStage == 2)
            {
                welcomeUser.Visible = true;
                welcomeUser.ForeColor = Color.FromArgb(welcomeUser.ForeColor.R - fadeSpeed, welcomeUser.ForeColor.G - fadeSpeed, welcomeUser.ForeColor.B - fadeSpeed);
                if (welcomeUser.ForeColor.R >= this.BackColor.R || welcomeUser.ForeColor.R < 1)
                {
                    welcomeTimer.Stop();
                    welcomeUser.ForeColor = Color.Black;
                    welcomeUser.Visible = false;
                    welcomeStage++;

                    welcomePanel.Visible = false;
                }
            }


        }

        private void aboutBtn_click(object sender, EventArgs e)
        {
            if (about.Visible)
            {
                about.BringToFront();
            }
            else
            {
                about.Show();
            }

        }

        private void waitForExit_Tick(object sender, EventArgs e)
        {
            waitForExit.Stop();
            Application.Exit();
        }
        
        private void checkList(string url)
        {
            staff.Clear();
            admins.Clear();
            globalMods.Clear();
            moderators.Clear();
            viewers.Clear();

            staffNode.Nodes.Clear();
            adminsNode.Nodes.Clear();
            globalModsNode.Nodes.Clear();
            moderatorNode.Nodes.Clear();
            viewerNode.Nodes.Clear();

            viewerList.Nodes.Remove(staffNode);
            viewerList.Nodes.Remove(adminsNode);
            viewerList.Nodes.Remove(globalModsNode);
            viewerList.Nodes.Remove(moderatorNode);
            viewerList.Nodes.Remove(viewerNode);

            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = wc.DownloadString(url);
                    jsonParse = JsonConvert.DeserializeObject(json);


                    var viewerCheck = jsonParse.chatters.viewers;


                    for (var i = 0; i < jsonParse.chatters.moderators.Count; i++)
                    {
                        // moderators.Add(jsonParse.chatters.moderators[i]);
                        string mod = jsonParse.chatters.moderators[i];
                        moderators.Add(mod);
                    }
                    for (var i = 0; i < jsonParse.chatters.staff.Count; i++)
                    {
                        string staffMember = jsonParse.chatters.staff[i];
                        staff.Add(staffMember);
                    }
                    for (var i = 0; i < jsonParse.chatters.admins.Count; i++)
                    {
                        string admin = jsonParse.chatters.admins[i];
                        admins.Add(admin);
                    }
                    for (var i = 0; i < jsonParse.chatters.global_mods.Count; i++)
                    {
                        string globalMod = jsonParse.chatters.global_mods[i];
                        globalMods.Add(globalMod);
                    }

                    for (var i = 0; i < viewerCheck.Count; i++)
                    {
                        string user = jsonParse.chatters.viewers[i];
                        viewers.Add(user);
                        // Console.WriteLine(user);
                    }
                    setViewerList();
                }
                catch (Exception error)
                {
                    writeFile.logFile("Parsing URL tmi.twitch.tv failed - " + Environment.NewLine + error);
                }
            }
        }
        public List<string> staff = new List<string>();
        public List<string> admins = new List<string>();
        public List<string> globalMods = new List<string>();
        public List<string> moderators = new List<string>();
        public List<string> viewers = new List<string>();
        TreeNode staffNode = new TreeNode();
        TreeNode adminsNode = new TreeNode();
        TreeNode globalModsNode = new TreeNode();
        TreeNode moderatorNode = new TreeNode();
        TreeNode viewerNode = new TreeNode();
        public void setViewerList()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(setViewerList));
               // Console.WriteLine("-- Invoke is required. -- ");
            }
            else
            {
                
                if (staff.Count > 0)
                {

                    staffNode.Text = "Staff";
                    staffNode.ImageIndex = 10;
                    staffNode.SelectedImageIndex = 10;
                    foreach (string staffMember in staff)
                    {
                        bool staffExist = false;

                        foreach (TreeNode node in staffNode.Nodes)
                        {
                            if (node.Text == staffMember)
                            {
                                staffExist = true;
                            }
                        }
                        if (!staffExist)
                        {
                            TreeNode staffUsername = new TreeNode();
                            staffUsername.Text = staffMember;
                            staffNode.Nodes.Add(staffUsername);
                        }

                    }
                    bool hasStaff = false;
                    foreach (TreeNode node in viewerList.Nodes)
                    {
                        if (node.Text == "Staff")
                        {
                            hasStaff = true;
                        }
                    }
                    if (!hasStaff)
                    {
                        viewerList.Nodes.Add(staffNode);
                    }

                }



                if (admins.Count > 0)
                {

                    adminsNode.Text = "Admins";
                    adminsNode.ImageIndex = 12;
                    adminsNode.SelectedImageIndex = 12;
                    foreach (string adminMembers in admins)
                    {

                        bool hasAdminName = false;
                    foreach (TreeNode node in adminsNode.Nodes)
                    {
                        if (node.Text == adminMembers)
                        {
                            hasAdminName = true;
                        }
                    }
                    if (!hasAdminName)
                    {
                        TreeNode adminUsername = new TreeNode();
                        adminUsername.Text = adminMembers;
                        adminsNode.Nodes.Add(adminUsername);
                    }


                    
                }
                bool hasAdmin = false;
                foreach (TreeNode node in viewerList.Nodes)
                {
                    if (node.Text == "Admins")
                    {
                        hasAdmin = true;
                    }
                }
                if (!hasAdmin)
                {
                    viewerList.Nodes.Add(adminsNode);
                }
            }
            
            

            if (globalMods.Count > 0)
            {
                
                globalModsNode.Text = "Global Moderators";
                    globalModsNode.ImageIndex = 13;
                    globalModsNode.SelectedImageIndex = 13;
                foreach (string globalmods in globalMods)
                {

                    bool hasGlobal = false;
                    foreach (TreeNode node in globalModsNode.Nodes)
                    {
                        if (node.Text == globalmods)
                        {
                            hasGlobal = true;
                        }
                    }
                    if (!hasGlobal)
                    {
                        TreeNode globalModUsername = new TreeNode();
                        globalModUsername.Text = globalmods;
                        globalModsNode.Nodes.Add(globalModUsername);
                    }

                    
                }
                bool hasGlobalM = false;
                foreach (TreeNode node in viewerList.Nodes)
                {
                    if (node.Text == "Global Moderators")
                    {
                        hasGlobalM = true;
                    }
                }
                if (!hasGlobalM)
                {
                    viewerList.Nodes.Add(globalModsNode);
                }
            }







            if (moderators.Count > 0)
            {
                
                moderatorNode.Text = "Moderators";
                    moderatorNode.ImageIndex = 9;
                    moderatorNode.SelectedImageIndex = 9;
                foreach (string mod in moderators)
                {

                    bool hasMod = false;
                    foreach (TreeNode node in moderatorNode.Nodes)
                    {
                        if (node.Text == mod)
                        {
                            hasMod = true;
                        }
                    }
                    if (!hasMod)
                    {
                        TreeNode moderator = new TreeNode();
                        moderator.Text = mod;
                        moderatorNode.Nodes.Add(moderator);
                    }



                }
                bool hasMods = false;
                foreach (TreeNode node in viewerList.Nodes)
                {
                    if (node.Text == "Moderators")
                    {
                        hasMods = true;
                    }
                }
                if (!hasMods)
                {
                    viewerList.Nodes.Add(moderatorNode);
                }
            }


            
            if (viewers.Count > 0)
            {
                
                viewerNode.Text = "Viewers";

                foreach (string viewer in viewers)
                {
                    bool hasViewer = false;
                    foreach (TreeNode node in viewerNode.Nodes)
                    {
                        if (node.Text == viewer)
                        {
                            hasViewer = true;
                        }
                    }
                    if (!hasViewer)
                    {
                        TreeNode viewerUsername = new TreeNode();
                        viewerUsername.Text = viewer;
                        viewerNode.Nodes.Add(viewerUsername);
                    }

                    
                }
                bool hasViewers = false;
                foreach (TreeNode node in viewerList.Nodes)
                {
                    if (node.Text == "Viewers")
                    {
                        hasViewers = true;
                    }
                }
                if (!hasViewers)
                {
                    viewerList.Nodes.Add(viewerNode);
                }
            }
            }
        }
        public void callViewersCheck(int request)
        {
            if (request == 0 && lastCheckOnline)
            {
                checkList("https://tmi.twitch.tv/group/user/" + toJoin.Substring(toJoin.IndexOf(("#")) + 1) + "/chatters");
               // Console.WriteLine("https://tmi.twitch.tv/group/user/" + toJoin.Substring(toJoin.IndexOf(("#")) + 1) + "/chatters");
            }else if (request == 1)
            {
                checkList("https://tmi.twitch.tv/group/user/" + toJoin.Substring(toJoin.IndexOf(("#")) + 1) + "/chatters");
            }

        }
        System.Threading.Thread viewerListThread;
        private void apiCheck_Tick(object sender, EventArgs e)
        {
            checkIsOnline();
            controlIcon.BackgroundImage = Icon.ToBitmap();
        }
        bool isBitsStoreVisible = false;
        private void bitsStore_Click(object sender, EventArgs e)
        {
            if (BStore.Visible)
            {
                BStore.BringToFront();
            }
            else
            {
                BStore.Show();
                BStore.bitsIcon.Tick += (some, a) =>
                {
                    if (BStore.Visible)
                    {
                        string img = $"{Application.StartupPath}/data/icons/{BStore.currentIcon}.ico";
                        this.Icon = Icon.ExtractAssociatedIcon(img);
                    }
                    else
                    {
                        this.Icon = subathontool.Properties.Resources.subathon;
                    }
                };
                BStore.VisibleChanged += (some, a) =>
                {
                   // isBitsStoreVisible = bitsStore.Visible;

                };
            }
        }

        private void bitsView_Click(object sender, EventArgs e)
        {
            string toShow = "";
            for (int o = 0; o < cheerUsers.Count;)
            {
                Console.WriteLine(cheerUsers[o]);

                Console.WriteLine("Found user - User has cheered: " + cheerTotal[o]);
                toShow += Environment.NewLine + $"{cheerUsers[o]}: {cheerTotal[o]}";


                o++;
            }

            Bits.checkChannel(channelID);
            //  MessageBox.Show($"Twitch returned: {Environment.NewLine} User: {returned.user} {Environment.NewLine} User_ID: {returned.user_id} {Environment.NewLine} Channel: {returned.channel} {Environment.NewLine}Channel_ID: {returned.channel_id} {Environment.NewLine} Time: {returned.time} {Environment.NewLine} chat_message: {returned.messageFromUser} {Environment.NewLine} Message bits: {returned.messageBits} {Environment.NewLine} Total Bits from user: {returned.totalUserBits} {Environment.NewLine} Context: {returned.context}");
            // MessageBox.Show("The total bits today: " + toShow, "Bits Total");
        }

        private void closeConnection_Click(object sender, EventArgs e)
        {
            Bits.disconnect(-1, "Terminated Connection.");
        }
        connection gamewisp;

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {


            gamewisp.connect();
            Timer t = new Timer();
            t.Interval = 3000;
            t.Start();
            t.Tick += (send, j) =>
            {
                if (gamewisp.finished)
                {
                    t.Stop();
                    if (!gamewisp.wasSuccessful)
                    {
                        listBox2.Items.Add("Gamewisp Connection Failed - " + gamewisp.error);
                        listBox2.SelectedItem = "Gamewisp Connection Failed - " + gamewisp.error;
                    }
                    else
                    {
                        listBox2.Items.Add("Gamewisp was successfully integrated!");
                        listBox2.SelectedItem = "Gamewisp was successfully integrated!";
                    }
                }
            };
        }

        private void listBox2_MouseHover(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                toolTip1.Show(listBox2.SelectedItem.ToString(), listBox2);
            }
        }

        private void cheeredAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These are users that have spent Twitch Bits in your channel. In the future, you'll be able to specify a minimum amount of bits to cheer (once the Twitch Bits API has been integrated fully. " + Environment.NewLine + "NOTE: This currently requires that your viewers have their cheer badge shown. If it's not already, advise they run /cheerbadge in your chat to equip it.", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void bitsTEst_Click(object sender, EventArgs e)
        {
            // Console.Write(Bits.testOAuth(channelID) + Environment.NewLine);
            Bits.checkChannel(channelID);

        }
        string[] colours = new string[] { "#FFFFFF", "#E0D9EC", "#C1B3DA", "#A28DC8", "#8367B6", "#6441A4" };
        
        Timer chatTimer = new Timer();
        Timer giveawayTimer = new Timer();
        Timer bitsTimer = new Timer();
        Timer feedbackTimer = new Timer();
        Timer madgamerbotTimer = new Timer();
        Timer settingsTimer = new Timer();
        Timer aboutTimer = new Timer();
        Buttons.twitchChat chatButtons;
        Buttons.giveaway giveawayButtons;
        Buttons.bitsStore bitsButtons;
        Buttons.feedback feedbackButtons;
        Buttons.MadGamerBot madgamerbotButtons;
        Buttons.settings settingsButtons;
        Buttons.About aboutButtons;

        private void button1_MouseEnter(object sender, EventArgs e)
        { //Twitch Hex Code: #6441A4
          //Hex codes generated from http://www.perbang.dk/rgbgradient/
            chatButtons.setType("enter");
            
            chatButtons.setI(0);
            if (!chatTimer.Enabled)
            {
                chatTimer.Start();
            }
            
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            chatButtons.setType("leave");   
            chatButtons.setI(5);
            if (!chatTimer.Enabled)
            {
                chatTimer.Start();
            }
            
        }

        private void giveawayBtn_MouseEnter(object sender, EventArgs e)
        {
            giveawayButtons.setType("enter");
            giveawayButtons.setI(0);
            if (!giveawayTimer.Enabled)
            {
                giveawayTimer.Start();
            }
        }

        private void giveawayBtn_MouseLeave(object sender, EventArgs e)
        {
            giveawayButtons.setType("leave");
            giveawayButtons.setI(5);
            if (!giveawayTimer.Enabled)
            {
                giveawayTimer.Start();
            }
        }

        private void bitsStore_MouseEnter(object sender, EventArgs e)
        {
            bitsButtons.setType("enter");
            bitsButtons.setI(0);
            if (!bitsTimer.Enabled)
            {
                bitsTimer.Start();
            }
        }

        private void bitsStore_MouseLeave(object sender, EventArgs e)
        {
            bitsButtons.setType("leave");
            bitsButtons.setI(5);
            if (!bitsTimer.Enabled)
            {
                bitsTimer.Start();
            }

        }

        private void feedbackBtn_MouseEnter(object sender, EventArgs e)
        {
            feedbackButtons.setType("enter");
            feedbackButtons.setI(0);
            if (!feedbackTimer.Enabled)
            {
                feedbackTimer.Start();
            }
        }

        private void feedbackBtn_MouseLeave(object sender, EventArgs e)
        {
            feedbackButtons.setType("leave");
            feedbackButtons.setI(5);
            if (!feedbackTimer.Enabled)
            {
                feedbackTimer.Start();
            }
        }

        private void madgamerbotBtn_MouseEnter(object sender, EventArgs e)
        {
            madgamerbotButtons.setType("enter");
            madgamerbotButtons.setI(0);
            if (!madgamerbotTimer.Enabled)
            {
                madgamerbotTimer.Start();
            }
        }

        private void madgamerbotBtn_MouseLeave(object sender, EventArgs e)
        {
            madgamerbotButtons.setType("leave");
            madgamerbotButtons.setI(5);
            if (!madgamerbotTimer.Enabled)
            {
                madgamerbotTimer.Start();
            }
        }

        private void settings_MouseEnter(object sender, EventArgs e)
        {
            settingsButtons.setType("enter");
            settingsButtons.setI(0);
            if (!settingsTimer.Enabled)
            {
                settingsTimer.Start();
            }
        }

        private void settings_MouseLeave(object sender, EventArgs e)
        {
            settingsButtons.setType("leave");
            settingsButtons.setI(5);
            if (!settingsTimer.Enabled)
            {
                settingsTimer.Start();
            }
        }

        private void aboutBtn_MouseEnter(object sender, EventArgs e)
        {
            aboutButtons.setType("enter");
            aboutButtons.setI(0);
            if (!aboutTimer.Enabled)
            {
                aboutTimer.Start();
            }
        }

        private void aboutBtn_MouseLeave(object sender, EventArgs e)
        {
            aboutButtons.setType("leave");
            aboutButtons.setI(5);
            if (!aboutTimer.Enabled)
            {
                aboutTimer.Start();
            }
        }

        private void removeTimeBtnClick(object sender, EventArgs e)
        {
            int value;
            if(removeTimeTxt.Text != null && int.TryParse(removeTimeTxt.Text, out value))
            {
                timeLeft = timeLeft - value;
                countdownGroup.Size = new Size(214, 123);
                gamewispGroup.Visible = true;
                toggleAddTime = false;
                removeTimePanel.Visible = false;
                addTimePanel.Visible = false;
            }
            else
            {
                removeTimeTxt.ForeColor = Color.Red;
            }
            
        }

        private void applyReg_Click(object sender, EventArgs e)
        {
            int value;
            if(addTimeTxt.Text != null && int.TryParse(addTimeTxt.Text, out value))
            {
                addTime(value, "none");
                countdownGroup.Size = new Size(214, 123);
                gamewispGroup.Visible = true;
                toggleAddTime = false;
                addTimePanel.Visible = false;
                removeTimePanel.Visible = false;

            }
            else
            {
                addTimeTxt.ForeColor = Color.Red;
            }
            
        }

        private void removeTimeTxt_Click(object sender, EventArgs e)
        {
            removeTimeTxt.ForeColor = Color.Black;
        }

        private void addTimeTxt_Click(object sender, EventArgs e)
        {
            addTimeTxt.ForeColor = Color.Black;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            closeControl.BackColor = ColorTranslator.FromHtml("#6441A4");
            closeControl.ForeColor = Color.White;
        }

        private void minimiseControl_MouseEnter(object sender, EventArgs e)
        {
            minimiseControl.BackColor = ColorTranslator.FromHtml("#6441A4");
            minimiseControl.ForeColor = Color.White;
        }

        private void closeControl_MouseLeave(object sender, EventArgs e)
        {
            closeControl.BackColor = Color.Transparent;
            closeControl.ForeColor = Color.Black;
        }

        private void minimiseControl_MouseLeave(object sender, EventArgs e)
        {
            minimiseControl.BackColor = Color.Transparent;
            minimiseControl.ForeColor = Color.Black;
        }

        private void minimiseControl_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void closeControl_Click(object sender, EventArgs e)
        {
            storeOptions.Dispose();
            Application.Exit();
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _offset = new Point(e.X, e.Y);
            }
        }
        
        private void controlBorder_MouseUp(object sender, MouseEventArgs e)
        {
            _offset = Point.Empty;
        }

        private void controlBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if(_offset != Point.Empty)
            {
                Point newlocation = this.Location;
                newlocation.X += e.X - _offset.X;
                newlocation.Y += e.Y - _offset.Y;
                this.Location = newlocation;
            }
        }

        private void controlBorder_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void pauseTimer_ButtonClick(object sender, EventArgs e)
        {
            if (isSubathonPaused)
            {
                isSubathonPaused = false;
                pauseTimer.Image = Properties.Resources.pause_icon_26;
                resumeSubathon();
            }
            else
            {
                isSubathonPaused = true;
                pauseTimer.Image = Properties.Resources.play_icon;
                pauseSubathon();
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            gamewisp.connect();
        }
        public void loadSettings()
        {
            countdownValueCombo.SelectedIndex = 0;
            resetUptimeRadio.Checked = true;
            roundingCheers.SelectedIndex = 1;
            userDocPath.AppendText(userDocsFolder + @"\subathontool\sounds\");
            userDocPath.SelectionStart = userDocPath.TextLength;
            userDocPath.ScrollToCaret();
            try
            {
                if (Directory.Exists("data"))
                {
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());
                        int value;
                        if (data2.channelforgiveaway != null || data2.channelforgiveaway != "")
                        {
                            giveawayChannel.Text = data2.channelforgiveaway;
                        }
                        if (data2.channeltojoin != null || data2.channeltojoin != "")
                        {
                            channelJoin.Text = data2.channeltojoin;
                        }
                        if (data2.checkViewerList == true)
                        {
                            checkAPI.Checked = true;
                        }
                        if (data2.refreshViewerSound == true)
                        {
                            refreshViewerSoundCheck.Checked = true;
                        }
                        if (data2.defaultCountdownValue != null)
                        {
                            defaultCountdownTxt.Text = data2.defaultCountdownValue.ToString();
                        }
                        if (data2.subscriberAddsValue != null)
                        {
                            subscriberAdds.Text = data2.subscriberAddsValue.ToString();
                        }
                        if (data2.chatOpensNewWindow)
                        {
                            separateWindowChat.Checked = true;
                        }
                        else
                        {
                            homeScreenChat.Checked = true;
                        }
                        resetUptimeRadio.Checked = data2.resetUptime;
                        pauseUptimeRadio.Checked = !data2.resetUptime;
                        maxNumberRNG.Text = data2.maxNumberRNG.ToString();
                        monthsAffectAmount.Checked = data2.enableResubMultiplier;
                        multiplierResubs.Text = data2.multiplierAmount.ToString();
                        useTwitchAccount.Checked = data2.useTwitchDetails;
                        playSoundFinishCheck.Checked = data2.timerCompletePlay;
                        enableSR.Checked = data2.enableSongRequest;
                        announceReSubs.Checked = data2.announceReSub;
                        announceSubs.Checked = data2.announceNewSub;
                        allowChatCmd.Checked = data2.useChatCommands;
                        allowGamewisp.Checked = data2.allowGamewisp;
                        chatOAuth.Text = data2.userOAuth;
                        chatUsername.Text = data2.twitchUsername;
                        resubValueAdd.Text = data2.resubsAddValue.ToString();
                        gamewispResubLbl.Enabled = data2.allowGamewisp;
                        gamewispResubValue.Enabled = data2.allowGamewisp;
                        gamewispSubLbl.Enabled = data2.allowGamewisp;
                        gamewispSubsValue.Enabled = data2.allowGamewisp;
                        connUseBits.Checked = data2.useBitsAPI;
                        bitsAddCheckBox.Checked = data2.useBitsAddTime;
                        bitsAddText.Text = data2.eachBitAdds.ToString();
                        donationCommands.Checked = data2.useDonationCmd;
                        tipsAddText.Text = data2.tipsAdd.ToString();
                        roundingCheers.SelectedIndex = data2.roundSecondsIndex;
                        allowStaffControlCheck.Checked = data2.allowStaffControl;
                        enableLogMaintenance.Checked = data2.allowLogMaintenance;
                        if (data2.useBitsAPI)
                        {
                            connBitsOAuth.Text = data2.bitsOAuth;
                        }
                        var slash = (char)92;
                        string[] path = data2.timerCompleteSoundFile.Split(slash);
                        timerFileName.Text = path[path.Length - 1];
                        if (data2.gamingMode == true)
                        {
                            gamingModeCheck.Checked = true;
                        }
                        else
                        {
                            gamingModeCheck.Checked = false;
                        }


                        for (var i = 0; i < data2.regularsList.Length; i++)
                        {
                            regularsList.Items.Add(data2.regularsList[i]);
                        }
                    }
                    if (enableSR.Checked == true)
                    {
                        srOptions.Enabled = true;

                    }
                    else
                    {
                        srOptions.Enabled = false;
                    }
                }
                else
                {
                    Directory.CreateDirectory("data");
                }
                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));


            }
            catch (Exception error)
            {
                writeFile.logFile("Caught Exception while reading JSON file: "
                    + error);
                //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveSettings_Click(object sender, EventArgs e)
        {
            int bitsAddInt = 0;
            decimal bitsAddDec = 0;
            if (bitsAddCheckBox.Checked)
            {

                try
                {
                    decimal.TryParse(bitsAddText.Text, out bitsAddDec);

                    /** if (bitsAddInt > 0) { }
                     else
                     {
                         MessageBox.Show("Bits Add Error - You can't enter a value less than 1.", "Error: Bits Add option must be greater than 0.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         bitsAddCheckBox.Checked = false;
                         bitsAdd.Text = "0";
                     }**/
                }
                catch (Exception err)
                {
                    MessageBox.Show("You've entered an incorrect value for the 'Bits Add' option.", "NaN Error: Bits Add ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writeFile.logFile("Encountered error saving bits add option - " + err);
                    bitsAddText.Text = "0";
                    bitsAddCheckBox.Checked = false;
                }
            }
            int value1 = 0;
            if (maxNumberRNG.Text != "" && maxNumberRNG != null && int.TryParse(maxNumberRNG.Text, out value1) && maxNumberRNG.Text != null && value1 > 0)
            {

            }
            else
            {
                maxNumberRNG.Text = "100";
                value1 = 100;
                writeFile.logFile("[WARN] maxNumberRNG detected as null or NaN. Defaulting to 100");
            }


            if (allowChatCmd.Checked || announceReSubs.Checked || announceSubs.Checked)
            {
                if (!useTwitchAccount.Checked)
                {
                    MessageBox.Show("It appears you've selected to either announce New/Re-subs in chat, or have selected to allow chat commands, but Use Twitch Account is currently unselected. Without entering a twitch account, the Subathon Tool will operate as a 'guest' or read-only chat, and cannot send messages. Please enter a twitch account.", "Twitch Account Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    announceReSubs.Checked = false;
                    announceSubs.Checked = false;
                }
            }
            string playPath;
            string twitchUsername;
            string twitchOAuth;
            if (timerFileName.Text != null && timerFileName.Text != "")
            {
                playPath = userDocsFolder + (char)92 + "subathontool" + (char)92 + "sounds" + (char)92 + timerFileName.Text;
            }
            else
            {
                playPath = "";
                playSoundFinishCheck.Checked = false;
            }
            if (chatUsername.Text != null && chatUsername.Text != "" && chatOAuth.Text != null && chatOAuth.Text != "")
            {
                twitchUsername = chatUsername.Text;
                twitchOAuth = chatOAuth.Text;
            }
            else
            {
                twitchUsername = "";
                twitchOAuth = "";
                useTwitchAccount.Checked = false;

            }
            int subscriberInt;
            int value;
            int multiplier;
            JArray array = new JArray();
            JObject o = new JObject();
            for (var i = 0; i < regularsList.Items.Count; i++)
            {
                regularsList.SelectedIndex = i;
                array.Add(regularsList.SelectedItem);
            }
            bool newWindow;
            if (separateWindowChat.Checked)
            {
                newWindow = true;
            }
            else
            {
                newWindow = false;
            }
            if (connUseBits.Checked)
            {
                if (connBitsOAuth.Text == null || connBitsOAuth.Text == "")
                {
                    connUseBits.Checked = false;
                    MessageBox.Show("You've selected to use the Bits API, but haven't provided an OAuth key - We cannot connect without. The option has been deselected automatically.", "Insufficient Credentials Provided", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            var defaultCountdown = int.TryParse(defaultCountdownTxt.Text, out value);
            var subscriberAdd = int.TryParse(subscriberAdds.Text, out subscriberInt);
            var multiplierConvert = int.TryParse(multiplierResubs.Text, out multiplier);
            int tipsAddInt;
            var tips = int.TryParse(tipsAddText.Text, out tipsAddInt);

            try
            {

                data Settings = new data
                {
                    channeltojoin = channelJoin.Text,
                    channelforgiveaway = giveawayChannel.Text,
                    timerCompleteSoundFile = playPath,
                    timerCompletePlay = playSoundFinishCheck.Checked,
                    checkViewerList = checkAPI.Checked,
                    refreshViewerSound = refreshViewerSoundCheck.Checked,
                    defaultCountdownValue = value,
                    subscriberAddsValue = subscriberInt,
                    gamingMode = gamingModeCheck.Checked,
                    resubsAddValue = Convert.ToInt32(resubValueAdd.Text),
                    enableSongRequest = enableSR.Checked,
                    regularsList = array.ToObject<string[]>(),
                    useTwitchDetails = useTwitchAccount.Checked,
                    twitchUsername = twitchUsername,
                    userOAuth = twitchOAuth,
                    chatOpensNewWindow = false,
                    useChatCommands = allowChatCmd.Checked,
                    announceNewSub = announceSubs.Checked,
                    announceReSub = announceReSubs.Checked,
                    announceBits = announceCheerCheck.Checked,
                    maxNumberRNG = value1,
                    allowGamewisp = allowGamewisp.Checked,
                    enableResubMultiplier = monthsAffectAmount.Checked,
                    multiplierAmount = multiplier,
                    useBitsAPI = connUseBits.Checked,
                    bitsOAuth = connBitsOAuth.Text,
                    useBitsAddTime = bitsAddCheckBox.Checked,
                    eachBitAdds = bitsAddDec,
                    useDonationCmd = donationCommands.Checked,
                    tipsAdd = tipsAddInt,
                    roundSecondsIndex = roundingCheers.SelectedIndex,
                    resetUptime = resetUptimeRadio.Checked,
                    allowStaffControl = allowStaffControlCheck.Checked,
                    allowLogMaintenance = enableLogMaintenance.Checked,
                };
                string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                //write string to file
                System.IO.File.WriteAllText(appdataFolder + "/subathontool/settings.json", json);
                
            }
            catch (Exception error)
            {
                writeFile.logFile("Exception caught while setting JSON object: " +
                    error);
            }
            checkJsonSettings();
        }

        private void twitterSocial_MouseEnter(object sender, EventArgs e)
        {
            twitterSocial.BackColor = ColorTranslator.FromHtml("#CCCCCC");
        }

        private void twitterSocial_MouseLeave(object sender, EventArgs e)
        {
            twitterSocial.BackColor = Color.White;
        }

        private void discordSocial_MouseEnter(object sender, EventArgs e)
        {
            discordSocial.BackColor = ColorTranslator.FromHtml("#CCCCCC");
        }

        private void discordSocial_MouseLeave(object sender, EventArgs e)
        {
            discordSocial.BackColor = Color.White;
        }

        private void freshDeskSocial_MouseEnter(object sender, EventArgs e)
        {
            freshDeskSocial.BackColor = ColorTranslator.FromHtml("#CCCCCC");
        }

        private void freshDeskSocial_MouseLeave(object sender, EventArgs e)
        {
            freshDeskSocial.BackColor = Color.White;
        }

        private void emailSocial_MouseEnter(object sender, EventArgs e)
        {
            emailSocial.BackColor = ColorTranslator.FromHtml("#CCCCCC");
        }

        private void emailSocial_MouseLeave(object sender, EventArgs e)
        {
            emailSocial.BackColor = Color.White;
        }

        private void AboutPicture_MouseEnter(object sender, EventArgs e)
        {
            AboutPicture.BackColor = ColorTranslator.FromHtml("#CCCCCC");
        }

        private void AboutPicture_MouseLeave(object sender, EventArgs e)
        {
            AboutPicture.BackColor = Color.White;
        }

        private void AboutPicture_Click(object sender, EventArgs e)
        {
            about.Show();
        }

        private void emailSocial_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:twitch.themadgamers@outlook.com");
        }

        private void freshDeskSocial_Click(object sender, EventArgs e)
        {
            Process.Start("http://themadgamers.freshdesk.com");
        }

        private void discordSocial_Click(object sender, EventArgs e)
        {
            Process.Start("http://discord.gg/n9WxdDF");
        }

        private void twitterSocial_Click(object sender, EventArgs e)
        {
            Process.Start("http://twitter.com/madgamerbot");
        }

        private void welcomePanel_VisibleChanged(object sender, EventArgs e)
        {
            if (welcomePanel.Visible)
            {
                statusStrip1.Visible = false;
                panel3.Visible = false;
            }else
            {
                statusStrip1.Visible = true;
                panel3.Visible = true;
            }
        }

        private void removeUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                viewerLists.Items.Remove(viewerLists.SelectedItem);
            }
            catch (Exception error)
            {
                writeFile.logFile("Couldn't remove user from list - " + error);
            }
        }
        private void checkGiveawayList(string url)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    giveawayViewers = wc.DownloadString(url);
                    giveawayViewersParse = JsonConvert.DeserializeObject(giveawayViewers);


                    var viewerCheck = giveawayViewersParse.chatters.viewers;


                    for (var i = 0; i < giveawayViewersParse.chatters.moderators.Count; i++)
                    {
                        viewerLists.Items.Add(giveawayViewersParse.chatters.moderators[i]);
                    }
                    for (var i = 0; i < viewerCheck.Count; i++)
                    {

                        viewerLists.Items.Add(giveawayViewersParse.chatters.viewers[i]);

                    }
                    totalUsers.Text = "Total Viewers:" + viewerLists.Items.Count;


                }
                catch (Exception error)
                {
                    writeFile.logFile("Parsing URL tmi.twitch.tv failed - " + Environment.NewLine + error);
                }
            }
        }
        private void refreshList_Click(object sender, EventArgs e)
        {
            viewerLists.Items.Clear();
            
            if (channelForGiveaway != null || channelForGiveaway != "")
            {
                checkGiveawayList("https://tmi.twitch.tv/group/user/" + channelForGiveaway + "/chatters");
            }
            Console.WriteLine("|| Checking: https://tmi.twitch.tv/group/user/" + channelForGiveaway + "/chatters ||");
            totalUsers.Text = "Total Viewers:" + viewerLists.Items.Count;
        }

        private void viewJsonBtn_Click(object sender, EventArgs e)
        {
            var txtWindow = new textWindow();
            txtWindow.textBox1.ReadOnly = true;
            txtWindow.textBox1.Text = giveawayViewers;

            txtWindow.Show();
        }
        int winnersCount;
        private void beginUserBtn_Click(object sender, EventArgs e)
        {
            var winner = new winner();
            winner.Show();
            winnersCount++;
            writeFile.writeGiveaways(winnersCount.ToString());
            var random = new Random();
            
            var randomNumber = random.Next(0, viewerLists.Items.Count);
            viewerLists.SelectedIndex = randomNumber;
            winner.username.Text = viewerLists.SelectedItem.ToString();
            winner.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        int rng;
        private void generateRNG_Click(object sender, EventArgs e)
        {
            Random RNG = new Random();
            if (maxNumber < 1)
            {
                MessageBox.Show("Could not initialise Giveaway - Max number is smaller than 1. Please set a value in the Settings menu.", "Giveaway Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (!isConnected)
                {
                    userGuesses.Items.Add("-- IRC Connection isn't open; connecting.");
                    reconnect(toJoin);
                    twitchChat.Start();
                }
                var numToGuess = RNG.Next(1, maxNumber);
                rng = numToGuess;
                if (!hideRNG.Checked)
                {
                    RNGOutput.Text = numToGuess.ToString();
                }
                generateRNG.Enabled = false;
                giveawayIsActive = true;
                giveawayType = "num";
                numberGiveawayTimer.Start();
            }
        }

        private void hideRNG_CheckedChanged(object sender, EventArgs e)
        {
            if (hideRNG.Checked)
            {
                RNGOutput.Text = "HIDDEN";
            }
            else
            {
                RNGOutput.Text = rng.ToString();
            }
        }
        string lastReadMessage = "";
        string lastReadUser = "";

        
        private void numberGiveawayTimer_Tick(object sender, EventArgs e)
        {
            if (giveawayType == "num")
            {

                int value1;
                if (lastSpeaker != null && lastMessage != null)
                {
                    if (int.TryParse(lastMessage, out value1) && lastMessage != null)
                    {

                        if (lastMessage != lastReadMessage)
                        {
                            lastReadUser = lastSpeaker;
                            lastReadMessage = lastMessage;
                            if (value1 <= maxNumber)
                            {
                                userGuesses.Items.Add($"{lastSpeaker} : {lastMessage}");
                                // Console.WriteLine("User entered thing");
                                if (lastMessage == rng.ToString())
                                {
                                    numberGiveawayTimer.Stop();
                                    giveawayIsActive = false;
                                    giveawayType = null;
                                    //  MessageBox.Show($"{lastSpeaker} guessed correctly!");
                                    userGuesses.Items.Add($"{lastSpeaker} guessed correctly!");
                                    winnersCount++;
                                    writeFile.writeGiveaways(winnersCount.ToString());
                                }

                            }

                        }

                    }
                }
            }
            else if(giveawayType == "word")
            {
                if(lastSpeaker != null && lastMessage != null)
                {
                    if(lastSpeaker == lastReadUser)
                    {
                        if(lastMessage != lastReadMessage)
                        {
                            lastReadUser = lastSpeaker;
                            lastReadMessage = lastMessage;
                            manageWordGiveaway();
                        }
                    }
                    else
                        {
                            lastReadUser = lastSpeaker;
                            lastReadMessage = lastMessage;
                            manageWordGiveaway();
                        }
                }
            }
            else if(giveawayType == null) {
                userGuesses.Items.Add("Error - the Giveaway type is undefined. Cancelling.");
            }

        }
        private void manageWordGiveaway()
        {
            Console.WriteLine("Managing.");
            var messageSplit = lastMessage.Split((char)' ');
            if (messageSplit.Count() < 2)
            {
                wordGuesses.Items.Add($"{lastSpeaker}: {lastMessage}");
                if (lastMessage == currentWordGiveawayText.Text)
                {
                    if (wordGiveFirstUser.Checked) //If the first user to guess correctly is to win, this happens.
                    {
                        numberGiveawayTimer.Stop();
                        giveawayType = null;
                        giveawayIsActive = false;
                        wordGuesses.Items.Add($"{lastSpeaker} guessed correctly!");
                        winnersCount++;
                        writeFile.writeGiveaways(winnersCount.ToString());

                        startWordGiveawayBtn.Text = "Start Giveaway";
                        startWordGiveawayBtn.BackColor = Color.FromArgb(128, 255, 128);
                    }
                    else //If the tool is to wait for the streamer to draw from every viewer to guess the word correctly, this happens.
                    {
                        if (giveawayUsers.Contains(lastSpeaker))
                        {
                            wordGuesses.Items.Add($"-- {lastSpeaker} is already in the Giveaway. Skipping user --");
                        }
                        else
                        {
                            wordGuesses.Items.Add($"Added {lastSpeaker} to the draw. Remember to click 'Draw Winner!'");
                            giveawayUsers.Add(lastSpeaker);
                        }
                    }
                }
            }
        }
        private void hideWarn_Click(object sender, EventArgs e)
        {
            warnPanel.Visible = false;
        }

        private void multiplierResubs_TextChanged(object sender, EventArgs e)
        {

        }

        private void expandCountdownBtn_Click(object sender, EventArgs e)
        {
            if (expandedCountdown)
            {
                expandedCountdown = false;
                expandCountdownBtn.BackgroundImage = Properties.Resources.expand;
                countdownMinsPanel.Visible = false;
            }
            else
            {
                expandedCountdown = true;
                expandCountdownBtn.BackgroundImage = Properties.Resources.collapse;
                countdownMinsPanel.Visible = true;
            }
        }

        private void expandSubsAddBtn_Click(object sender, EventArgs e)
        {
            if (expandedSubs)
            {
                expandedSubs = false;
                expandSubsAddBtn.BackgroundImage = Properties.Resources.expand;
                subsMinsPanel.Visible = false;

            }else
            {
                expandedSubs = true;
                expandSubsAddBtn.BackgroundImage = Properties.Resources.collapse;
                subsMinsPanel.Visible = true;
            }
        }

        private void expandResubsAddBtn_Click(object sender, EventArgs e)
        {
            if (expandedResubs)
            {
                expandedResubs = false;
                expandResubsAddBtn.BackgroundImage = Properties.Resources.expand;
                resubsMinsPanel.Visible = false;
            }
            else
            {
                expandedResubs = true;
                expandResubsAddBtn.BackgroundImage = Properties.Resources.collapse;
                resubsMinsPanel.Visible = true;
            }
        }

        private void expandBitsAddBtn_Click(object sender, EventArgs e)
        {
            if (expandedBits)
            {
                expandedBits = false;
                expandBitsAddBtn.BackgroundImage = Properties.Resources.expand;
                bitsMinsPanel.Visible = false;
            }
            else
            {
                expandedBits = true;
                expandBitsAddBtn.BackgroundImage = Properties.Resources.collapse;
                bitsMinsPanel.Visible = true;
            }
        }

        private void expandTipsAddBtn_Click(object sender, EventArgs e)
        {
            if (expandedTips)
            {
                expandedTips = false;
                expandTipsAddBtn.BackgroundImage = Properties.Resources.expand;
                tipsMinsPanel.Visible = false;
            }
            else
            {
                expandedTips = true;
                expandTipsAddBtn.BackgroundImage = Properties.Resources.collapse;
                tipsMinsPanel.Visible = true;
            }
        }

        private void expandGWSubBtn_Click(object sender, EventArgs e)
        {
            if (expandedGWSubs)
            {
                expandedGWSubs = false;
                expandGWSubBtn.BackgroundImage = Properties.Resources.expand;
                gwMinsPanel.Visible = false;
            }
            else
            {
                expandedGWSubs = true;
                expandGWSubBtn.BackgroundImage = Properties.Resources.collapse;
                gwMinsPanel.Visible = true;
            }
        }

        private void expandGWResubBtn_Click(object sender, EventArgs e)
        {
            if (expandedGWResubs)
            {
                expandedGWResubs = false;
                expandGWResubBtn.BackgroundImage = Properties.Resources.expand;
                gwResubsMinsPanel.Visible = false;
            }
            else
            {
                expandedGWResubs = true;
                expandGWResubBtn.BackgroundImage = Properties.Resources.collapse;
                gwResubsMinsPanel.Visible = true;
            }
        }

        private void applyCountdownMinsBtn_Click(object sender, EventArgs e)
        {
            int result;
            if(countdownValueCombo.SelectedIndex == 1) //Hours selected
            {
                if (int.TryParse(countdownConverterTxt.Text, out result))
                {
                    int toSet = (result * 60 * 60);
                    defaultCountdownTxt.Text = toSet.ToString();
                }
            }
            else if(countdownValueCombo.SelectedIndex == 0) // Minutes Selected
            {
                if(int.TryParse(countdownConverterTxt.Text, out result))
                {
                    int toSet = (result * 60);
                    defaultCountdownTxt.Text = toSet.ToString();
                }
            }
        }

        private void slot1Combo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void slot0Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot0Combo);
            slot0BuyPnl.Location = slot0Btn.Location;
            slot0BuyPnl.Visible = true;
        }
        public void setItems()
        {
            Console.WriteLine("Loading Bits Store Options");
            storeOptions.loadOptions();

            if (storeOptions.slot0Name.Text != "" && storeOptions.slot0Name.Text != null)
            {
                slot0Btn.Enabled = true;
                slot0Btn.Text = storeOptions.slot0Name.Text;
                if (storeOptions.slot0ImagePath.Text != null && storeOptions.slot0ImagePath.Text != "")
                {
                    try
                    {
                        Console.WriteLine("Found path " + storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        Image slot0Image = new Bitmap(storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        slot0Btn.BackgroundImage = slot0Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot1Name.Text != "" && storeOptions.slot1Name.Text != null)
            {
                slot1Btn.Enabled = true;
                slot1Btn.Text = storeOptions.slot1Name.Text;

                if (storeOptions.slot1Picture.Text != null && storeOptions.slot1Picture.Text != "")
                {
                    try
                    {
                        Image slot1Image = new Bitmap(storeOptions.slot1Picture.Text.Replace(@"\\", @"\"));
                        slot1Btn.BackgroundImage = slot1Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot1Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot2Name.Text != "" && storeOptions.slot2Name.Text != null)
            {
                slot2Btn.Enabled = true;
                slot2Btn.Text = storeOptions.slot2Name.Text;
                if (storeOptions.slot2Picture.Text != null && storeOptions.slot2Picture.Text != "")
                {
                    try
                    {
                        Image slot2Image = new Bitmap(storeOptions.slot2Picture.Text.Replace(@"\\", @"\"));
                        slot2Btn.BackgroundImage = slot2Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot2Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot3Name.Text != "" && storeOptions.slot3Name.Text != null)
            {
                slot3Btn.Enabled = true;
                slot3Btn.Text = storeOptions.slot3Name.Text;
                if (storeOptions.slot3Picture.Text != null && storeOptions.slot3Picture.Text != "")
                {
                    Image slot3Image = new Bitmap(storeOptions.slot3Picture.Text.Replace(@"\\", @"\"));
                    slot3Btn.BackgroundImage = slot3Image;
                }
            }
            if (storeOptions.slot4Name.Text != "" && storeOptions.slot4Name.Text != null)
            {
                slot4Btn.Enabled = true;
                slot4Btn.Text = storeOptions.slot4Name.Text;
                if (storeOptions.slot4Picture.Text != null && storeOptions.slot4Picture.Text != "")
                {
                    try
                    {
                        Image slot4Image = new Bitmap(storeOptions.slot4Picture.Text.Replace(@"\\", @"\"));
                        slot4Btn.BackgroundImage = slot4Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot4Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }

            }
            if (storeOptions.slot5Name.Text != "" && storeOptions.slot5Name.Text != null)
            {
                slot5Btn.Enabled = true;
                slot5Btn.Text = storeOptions.slot5Name.Text;
                if (storeOptions.slot5Picture.Text != null && storeOptions.slot5Picture.Text != "")
                {
                    try
                    {
                        Image slot5Image = new Bitmap(storeOptions.slot5Picture.Text.Replace(@"\\", @"\"));
                        slot5Btn.BackgroundImage = slot5Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot5Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                }
            }
            if (storeOptions.slot6Name.Text != "" && storeOptions.slot6Name.Text != null)
            {
                slot6Btn.Enabled = true;
                slot6Btn.Text = storeOptions.slot6Name.Text;
                if (storeOptions.slot6Picture.Text != null && storeOptions.slot6Picture.Text != "")
                {
                    try
                    {
                        Image slot6Image = new Bitmap(storeOptions.slot6Picture.Text.Replace(@"\\", @"\"));
                        slot6Btn.BackgroundImage = slot6Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot6Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot7Name.Text != "" && storeOptions.slot7Name.Text != null)
            {
                slot7Btn.Enabled = true;
                slot7Btn.Text = storeOptions.slot7Name.Text;
                if (storeOptions.slot7Picture.Text != null && storeOptions.slot7Picture.Text != "")
                {
                    try
                    {
                        Image slot7Image = new Bitmap(storeOptions.slot7Picture.Text.Replace(@"\\", @"\"));
                        slot7Btn.BackgroundImage = slot7Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot7Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot8Name.Text != "" && storeOptions.slot8Name.Text != null)
            {
                slot8Btn.Enabled = true;
                slot8Btn.Text = storeOptions.slot8Name.Text;
                if (storeOptions.slot8Picture.Text != null && storeOptions.slot8Picture.Text != "")
                {
                    try
                    {
                        Image slot8Image = new Bitmap(storeOptions.slot8Picture.Text.Replace(@"\\", @"\"));
                        slot8Btn.BackgroundImage = slot8Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot8Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot9Name.Text != "" && storeOptions.slot9Name.Text != null)
            {
                slot9Btn.Enabled = true;
                slot9Btn.Text = storeOptions.slot9Name.Text;
                if (storeOptions.slot9Picture.Text != null && storeOptions.slot9Picture.Text != "")
                {
                    try
                    {
                        Image slot9Image = new Bitmap(storeOptions.slot9Picture.Text.Replace(@"\\", @"\"));
                        slot9Btn.BackgroundImage = slot9Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot9Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot10Name.Text != "" && storeOptions.slot10Name.Text != null)
            {
                slot10Btn.Enabled = true;
                slot10Btn.Text = storeOptions.slot10Name.Text;
                if (storeOptions.slot10Picture.Text != null && storeOptions.slot10Picture.Text != "")
                {
                    try
                    {
                        Image slot10Image = new Bitmap(storeOptions.slot10Picture.Text.Replace(@"\\", @"\"));
                        slot10Btn.BackgroundImage = slot10Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot10Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot11Name.Text != "" && storeOptions.slot11Name.Text != null)
            {
                slot11Btn.Enabled = true;
                slot11Btn.Text = storeOptions.slot11Name.Text;
                if (storeOptions.slot11Picture.Text != null && storeOptions.slot11Picture.Text != "")
                {
                    try
                    {
                        Image slot11Image = new Bitmap(storeOptions.slot11Picture.Text.Replace(@"\\", @"\"));
                        slot11Btn.BackgroundImage = slot11Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot11Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
        }
        private void bitsStorePage_Click(object sender, EventArgs e)
        {
            hideSlots();
            
        }
        
        private int getItemPrice(TextBox item)
        {
            int price;
            if(int.TryParse(item.Text, out price)){
                return price;
            }
            else
            {
                return -1;
            }
            
        }
        private void loadStore()
        {
            if (storeOptions.slot0Name.Text != "" && storeOptions.slot0Name.Text != null)
            {
                slot0Btn.Enabled = true;
                slot0Btn.Text = storeOptions.slot0Name.Text;
                if (storeOptions.slot0ImagePath.Text != null && storeOptions.slot0ImagePath.Text != "")
                {
                    try
                    {
                        Console.WriteLine("Found path " + storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        Image slot0Image = new Bitmap(storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\"));
                        slot0Btn.BackgroundImage = slot0Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot0ImagePath.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot1Name.Text != "" && storeOptions.slot1Name.Text != null)
            {
                slot1Btn.Enabled = true;
                slot1Btn.Text = storeOptions.slot1Name.Text;

                if (storeOptions.slot1Picture.Text != null && storeOptions.slot1Picture.Text != "")
                {
                    try
                    {
                        Image slot1Image = new Bitmap(storeOptions.slot1Picture.Text.Replace(@"\\", @"\"));
                        slot1Btn.BackgroundImage = slot1Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot1Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot2Name.Text != "" && storeOptions.slot2Name.Text != null)
            {
                slot2Btn.Enabled = true;
                slot2Btn.Text = storeOptions.slot2Name.Text;
                if (storeOptions.slot2Picture.Text != null && storeOptions.slot2Picture.Text != "")
                {
                    try
                    {
                        Image slot2Image = new Bitmap(storeOptions.slot2Picture.Text.Replace(@"\\", @"\"));
                        slot2Btn.BackgroundImage = slot2Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot2Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot3Name.Text != "" && storeOptions.slot3Name.Text != null)
            {
                slot3Btn.Enabled = true;
                slot3Btn.Text = storeOptions.slot3Name.Text;
                if (storeOptions.slot3Picture.Text != null && storeOptions.slot3Picture.Text != "")
                {
                    Image slot3Image = new Bitmap(storeOptions.slot3Picture.Text.Replace(@"\\", @"\"));
                    slot3Btn.BackgroundImage = slot3Image;
                }
            }
            if (storeOptions.slot4Name.Text != "" && storeOptions.slot4Name.Text != null)
            {
                slot4Btn.Enabled = true;
                slot4Btn.Text = storeOptions.slot4Name.Text;
                if (storeOptions.slot4Picture.Text != null && storeOptions.slot4Picture.Text != "")
                {
                    try
                    {
                        Image slot4Image = new Bitmap(storeOptions.slot4Picture.Text.Replace(@"\\", @"\"));
                        slot4Btn.BackgroundImage = slot4Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot4Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }

            }
            if (storeOptions.slot5Name.Text != "" && storeOptions.slot5Name.Text != null)
            {
                slot5Btn.Enabled = true;
                slot5Btn.Text = storeOptions.slot5Name.Text;
                if (storeOptions.slot5Picture.Text != null && storeOptions.slot5Picture.Text != "")
                {
                    try
                    {
                        Image slot5Image = new Bitmap(storeOptions.slot5Picture.Text.Replace(@"\\", @"\"));
                        slot5Btn.BackgroundImage = slot5Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot5Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                }
            }
            if (storeOptions.slot6Name.Text != "" && storeOptions.slot6Name.Text != null)
            {
                slot6Btn.Enabled = true;
                slot6Btn.Text = storeOptions.slot6Name.Text;
                if (storeOptions.slot6Picture.Text != null && storeOptions.slot6Picture.Text != "")
                {
                    try
                    {
                        Image slot6Image = new Bitmap(storeOptions.slot6Picture.Text.Replace(@"\\", @"\"));
                        slot6Btn.BackgroundImage = slot6Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot6Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot7Name.Text != "" && storeOptions.slot7Name.Text != null)
            {
                slot7Btn.Enabled = true;
                slot7Btn.Text = storeOptions.slot7Name.Text;
                if (storeOptions.slot7Picture.Text != null && storeOptions.slot7Picture.Text != "")
                {
                    try
                    {
                        Image slot7Image = new Bitmap(storeOptions.slot7Picture.Text.Replace(@"\\", @"\"));
                        slot7Btn.BackgroundImage = slot7Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot7Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot8Name.Text != "" && storeOptions.slot8Name.Text != null)
            {
                slot8Btn.Enabled = true;
                slot8Btn.Text = storeOptions.slot8Name.Text;
                if (storeOptions.slot8Picture.Text != null && storeOptions.slot8Picture.Text != "")
                {
                    try
                    {
                        Image slot8Image = new Bitmap(storeOptions.slot8Picture.Text.Replace(@"\\", @"\"));
                        slot8Btn.BackgroundImage = slot8Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot8Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot9Name.Text != "" && storeOptions.slot9Name.Text != null)
            {
                slot9Btn.Enabled = true;
                slot9Btn.Text = storeOptions.slot9Name.Text;
                if (storeOptions.slot9Picture.Text != null && storeOptions.slot9Picture.Text != "")
                {
                    try
                    {
                        Image slot9Image = new Bitmap(storeOptions.slot9Picture.Text.Replace(@"\\", @"\"));
                        slot9Btn.BackgroundImage = slot9Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot9Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot10Name.Text != "" && storeOptions.slot10Name.Text != null)
            {
                slot10Btn.Enabled = true;
                slot10Btn.Text = storeOptions.slot10Name.Text;
                if (storeOptions.slot10Picture.Text != null && storeOptions.slot10Picture.Text != "")
                {
                    try
                    {
                        Image slot10Image = new Bitmap(storeOptions.slot10Picture.Text.Replace(@"\\", @"\"));
                        slot10Btn.BackgroundImage = slot10Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot10Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }

                }
            }
            if (storeOptions.slot11Name.Text != "" && storeOptions.slot11Name.Text != null)
            {
                slot11Btn.Enabled = true;
                slot11Btn.Text = storeOptions.slot11Name.Text;
                if (storeOptions.slot11Picture.Text != null && storeOptions.slot11Picture.Text != "")
                {
                    try
                    {
                        Image slot11Image = new Bitmap(storeOptions.slot11Picture.Text.Replace(@"\\", @"\"));
                        slot11Btn.BackgroundImage = slot11Image;
                    }
                    catch (System.ArgumentException err)
                    {
                        MessageBox.Show("Encountered error loading image - Please check the file '" + storeOptions.slot11Picture.Text.Replace(@"\\", @"\") + "' still exists. " + Environment.NewLine + err.GetBaseException());
                    }
                    
                }
            }
        }
        private void slot1Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot1Combo);
            slot1BuyPnl.Location = slot1Btn.Location;
            slot1BuyPnl.Visible = true;
        }

        private void hideSlots()
        {
            slot0BuyPnl.Visible = false;
            slot1BuyPnl.Visible = false;
            slot2BuyPnl.Visible = false;
            slot3BuyPnl.Visible = false;
            slot4BuyPnl.Visible = false;
            slot5BuyPnl.Visible = false;
            slot6BuyPnl.Visible = false;
            slot7BuyPnl.Visible = false;
            slot8BuyPnl.Visible = false;
            slot9BuyPnl.Visible = false;
            slot10BuyPnl.Visible = false;
            slot11BuyPnl.Visible = false;
        }
        private void refreshSlot(ComboBox slot)
        {
            slot.Items.Clear();
            foreach(var name in bitsDictionary.Keys)
            {
                slot.Items.Add($"{name} [{bitsDictionary[name]}]");
            }
        }
        private void buyItem(string slotRequest)
        {
            int bits;
            int price;
            string selectedName;
            char space;
            char.TryParse(" ", out space);
            string selected;
            if (slotRequest == "slot0")
            {
                price = getItemPrice(storeOptions.slot0Price);
                selectedName = slot0Combo.GetItemText(slot0Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot0.Text = null;
                refreshSlot(slot0Combo);
            }
            else if (slotRequest == "slot1")
            {
                price = getItemPrice(storeOptions.slot1Price);
                selectedName = slot1Combo.GetItemText(slot1Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot1.Text = null;
                refreshSlot(slot1Combo);
            }
            else if (slotRequest == "slot2")
            {
                price = getItemPrice(storeOptions.slot2Price);
                selectedName = slot2Combo.GetItemText(slot2Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot2.Text = null;
                refreshSlot(slot2Combo);
            }
            else if(slotRequest == "slot3")
            {
                price = getItemPrice(storeOptions.slot3Price);
                selectedName = slot3Combo.GetItemText(slot3Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot3.Text = null;
                refreshSlot(slot3Combo);
            }
            else if (slotRequest == "slot4")
            {
                price = getItemPrice(storeOptions.slot4Price);
                selectedName = slot4Combo.GetItemText(slot4Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot4.Text = null;
                refreshSlot(slot4Combo);
            }
            else if(slotRequest == "slot5")
            {
                price = getItemPrice(storeOptions.slot5Price);
                selectedName = slot5Combo.GetItemText(slot5Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot5.Text = null;
                refreshSlot(slot5Combo);
            }
            else if(slotRequest == "slot6")
            {
                price = getItemPrice(storeOptions.slot6Price);
                selectedName = slot6Combo.GetItemText(slot6Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot6.Text = null;
                refreshSlot(slot6Combo);
            }
            else if(slotRequest == "slot7")
            {
                price = getItemPrice(storeOptions.slot7Price);
                selectedName = slot7Combo.GetItemText(slot7Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot7.Text = null;
                refreshSlot(slot7Combo);
            }
            else if(slotRequest == "slot8")
            {
                price = getItemPrice(storeOptions.slot8Price);
                selectedName = slot8Combo.GetItemText(slot8Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot8.Text = null;
                refreshSlot(slot8Combo);
            }
            else if(slotRequest == "slot9")
            {
                price = getItemPrice(storeOptions.slot9Price);
                selectedName = slot9Combo.GetItemText(slot9Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot9.Text = null;
                refreshSlot(slot9Combo);
            }
            else if(slotRequest == "slot10") {
                price = getItemPrice(storeOptions.slot10Price);
                selectedName = slot10Combo.GetItemText(slot10Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot10.Text = null;
                refreshSlot(slot10Combo);
            }
            else if(slotRequest == "slot11")
            {
                price = getItemPrice(storeOptions.slot11Price);
                selectedName = slot11Combo.GetItemText(slot11Combo.SelectedItem);
                selected = selectedName.Split(space)[0];
                Console.WriteLine("Name: " + selected);
                if (bitsDictionary.TryGetValue(selected, out bits))
                {
                    Console.WriteLine($"Successfully converted selected item {selected} to string and found values. [Bits: {bits}]");
                    if (bits > price)
                    {
                        Console.WriteLine($"User bought {slotRequest}  [{bits} - {price} = {bits - price}]");
                        bitsDictionary[selected] = bits - price;
                    }
                }
                searchSlot11.Text = null;
                refreshSlot(slot11Combo);
            }
            else
            {
                MessageBox.Show("Encountered an error while purchasing - the requested slot does not exist. Please contact support if this continues");
                Console.WriteLine("Requested store slot doesn't exist " + slotRequest);
                writeFile.logFile(" ||  Error ||  The requested store slot doesn't exist: " + slotRequest);
            }
        }
        private void button5_Click(object sender, EventArgs e) //when slot0ConfirmBtn is clicked.
        {
            buyItem("slot0");
        }
        private void slot0Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot0Selected = true;
        }

        private void searchSlot0_TextChanged(object sender, EventArgs e)
        {
            slot0Combo.Items.Clear();

            slot0Combo.SelectionStart = searchSlot0.Text.Length;
            foreach (var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot0.Text.ToLower()))
                {
                    slot0Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void editBits_Click(object sender, EventArgs e)
        {
            storeOptions.Show();
        }
        
        private void slot1Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot1Selected = true;
        }

        private void searchSlot1_TextChanged(object sender, EventArgs e)
        {
            slot1Combo.Items.Clear();
            slot1Combo.SelectionStart = searchSlot1.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot1.Text.ToLower())) {
                    slot1Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot1ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot1");
        }

        private void slot2ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot2");
        }

        private void searchSlot2_TextChanged(object sender, EventArgs e)
        {
            slot2Combo.Items.Clear();
            slot2Combo.SelectionStart = searchSlot2.Text.Length;
            foreach(var item in bitsDictionary.Keys) {
                if (item.ToString().ToLower().Contains(searchSlot2.Text.ToLower()))
                {
                    slot2Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot2Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot2Selected = true;
        }

        private void searchSlot3_TextChanged(object sender, EventArgs e)
        {
            slot3Combo.Items.Clear();
            slot3Combo.SelectionStart = searchSlot3.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot3.Text.ToLower()))
                {
                    slot3Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot3Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot3Selected = true;
        }

        private void slot3ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot3");
        }

        private void searchSlot4_TextChanged(object sender, EventArgs e)
        {
            slot4Combo.Items.Clear();
            slot4Combo.SelectionStart = searchSlot4.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot4.Text.ToLower()))
                {
                    slot4Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot4Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot3Selected = true;
        }

        private void slot4ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot4");
        }

        private void searchSlot5_TextChanged(object sender, EventArgs e)
        {
            slot5Combo.Items.Clear();
            slot5Combo.SelectionStart = searchSlot5.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot5.Text.ToLower()))
                {
                    slot5Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot5Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot5Selected = true;
        }

        private void slot5ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot5");
        }

        private void slot6ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot6");
        }

        private void slot6Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot6Selected = true;
        }

        private void searchSlot6_TextChanged(object sender, EventArgs e)
        {
            slot6Combo.Items.Clear();
            slot6Combo.SelectionStart = searchSlot6.Text.Length;
            foreach(var item in bitsDictionary.Keys) {
                if (item.ToString().ToLower().Contains(searchSlot6.Text.ToLower()))
                {
                    slot6Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void searchSlot7_TextChanged(object sender, EventArgs e)
        {
            slot7Combo.Items.Clear();
            slot7Combo.SelectionStart = searchSlot7.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot7.Text.ToLower()))
                {
                    slot7Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot7Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot7Selected = true;
        }

        private void slot7ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot7");
        }

        private void searchSlot8_TextChanged(object sender, EventArgs e)
        {
            slot8Combo.Items.Clear();
            slot8Combo.SelectionStart = searchSlot8.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot8.Text.ToLower()))
                {
                    slot8Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot8Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot8Selected = true;
        }

        private void slot8ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot8");
        }

        private void searchSlot9_TextChanged(object sender, EventArgs e)
        {
            slot9Combo.Items.Clear();
            slot9Combo.SelectionStart = searchSlot9.Text.Length;
            foreach(var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot9.Text.ToLower()))
                {
                    slot9Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot9Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot9Selected = true;
        }

        private void slot9ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot9");
        }

        private void searchSlot10_TextChanged(object sender, EventArgs e)
        {
            slot10Combo.Items.Clear();
            slot10Combo.SelectionStart = searchSlot10.Text.Length;
            foreach (var item in bitsDictionary.Keys)
            {
                if (item.ToString().ToLower().Contains(searchSlot10.Text.ToLower()))
                {
                    slot10Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot10Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot10Selected = true;
        }

        private void slot10ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot10");
        }

        private void searchSlot11_TextChanged(object sender, EventArgs e)
        {
            slot11Combo.Items.Clear();
            slot11Combo.SelectionStart = searchSlot11.Text.Length;
            foreach(var item in bitsDictionary.Keys) {
                if (item.ToString().ToLower().Contains(searchSlot11.Text.ToLower()))
                {
                    slot11Combo.Items.Add($"{item} [{bitsDictionary[item]}]");
                }
            }
        }

        private void slot11Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            slot11Selected = true;
        }

        private void slot11ConfirmBtn_Click(object sender, EventArgs e)
        {
            buyItem("slot11");
        }

        private void slot2Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot2Combo);
            slot2BuyPnl.Location = slot2Btn.Location;
            slot2BuyPnl.Visible = true;
        }
        private void slot3Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot3Combo);
            slot3BuyPnl.Location = slot3Btn.Location;
            slot3BuyPnl.Visible = true;
        }

        private void slot4Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot4Combo);
            slot4BuyPnl.Location = slot4Btn.Location;
            slot4BuyPnl.Visible = true;
        }

        private void slot5Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot5Combo);
            slot5BuyPnl.Location = slot5Btn.Location;
            slot5BuyPnl.Visible = true;
        }

        private void slot6Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot6Combo);
            slot6BuyPnl.Location = slot6Btn.Location;
            slot6BuyPnl.Visible = true;
        }

        private void slot7Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot7Combo);
            slot7BuyPnl.Location = slot7Btn.Location;
            slot7BuyPnl.Visible = true;
        }

        private void slot8Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot8Combo);
            slot8BuyPnl.Location = slot8Btn.Location;
            slot8BuyPnl.Visible = true;
        }

        private void slot9Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot9Combo);
            slot9BuyPnl.Location = slot9Btn.Location;
            slot9BuyPnl.Visible = true;
        }

        private void slot10Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot10Combo);
            slot10BuyPnl.Location = slot10Btn.Location;
            slot10BuyPnl.Visible = true;
        }

        private void slot11Btn_Click(object sender, EventArgs e)
        {
            hideSlots();
            refreshSlot(slot11Combo);
            slot11BuyPnl.Location = slot11Btn.Location;
            slot11BuyPnl.Visible = true;
        }

        private void mainTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string date = DateTime.UtcNow + ":" + DateTime.UtcNow.Millisecond;
            Console.WriteLine(date.Replace("/", "-").Replace(":", "-").Replace(" ", "_"));
            if(mainTabs.SelectedTab == bitsStorePage)
            {
                hideSlots();
                setItems();
                
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you wish to disconnect? You'll need to reconnect in order for chat to continue being detected. \n \n If you wish to reconnect, you may have to stop the countdown, and restart it.", "Disconnect Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                tcpClient.Close();
            }
        }

        private void channelJoin_Leave(object sender, EventArgs e)
        {
            var text = channelJoin.Text;
            if (text != null && text != "")
            {

                if (text.Substring(0, 1) == "#")
                {
                    Console.WriteLine("Don't need to append # to textbox.");
                }
                else
                {
                    
                    channelJoin.Text = "#" + channelJoin.Text;
                    Console.WriteLine("Detected no # symbol in textbox.");

                    writeFile.logFile("Detected no # symbol in Channel textbox");
                }

            }
        }

        private void genRandomWord_Click(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                generatedRandomWord = client.DownloadString("http://randomword.setgetgo.com/get.php");
                currentWordGiveawayText.Text = generatedRandomWord;
            }
        }

        private void wordGiveRandomUser_CheckedChanged(object sender, EventArgs e)
        {
            wordGiveFirstUser.Checked = !wordGiveRandomUser.Checked;
            wordGiveDrawWinnerBtn.Visible = wordGiveRandomUser.Checked;
            if (wordGiveDrawWinnerBtn.Visible)
            {
                wordGuesses.Size = new Size(254, 160);
            }
            else
            {
                wordGuesses.Size = new Size(254, 186);
            }
        }

        private void wordGiveFirstUser_CheckedChanged(object sender, EventArgs e)
        {
            wordGiveRandomUser.Checked = !wordGiveFirstUser.Checked;
            wordGiveDrawWinnerBtn.Visible = wordGiveRandomUser.Checked;
            if (wordGiveDrawWinnerBtn.Visible)
            {
                wordGuesses.Size = new Size(254, 160);
            }
            else
            {
                wordGuesses.Size = new Size(254, 186);
            }
        }

        private void startWordGiveawayBtn_Click(object sender, EventArgs e)
        {
            if (!giveawayIsActive)
            {
                if(currentWordGiveawayText.Text != null && currentWordGiveawayText.Text != "")
                {
                    giveawayType = "word";
                    giveawayIsActive = true;
                    this.startWordGiveawayBtn.Text = "Abort Giveaway";
                    startWordGiveawayBtn.BackColor = Color.FromArgb(255, 128, 128);
                    numberGiveawayTimer.Start();
                    if (!isConnected)
                    {
                        wordGuesses.Items.Add("-- IRC wasn't open; connecting --");
                        reconnect(toJoin);
                        twitchChat.Start();
                    }
                }
                else
                {
                    wordGuesses.Items.Add(" -- Couldn't start giveaway; there was no specified word.");
                }
            }
            else if(giveawayType == "word")
            {
                startWordGiveawayBtn.Text = "Start Giveaway";
                giveawayIsActive = false;
                numberGiveawayTimer.Stop();
                giveawayType = null;
                startWordGiveawayBtn.BackColor = Color.FromArgb(128, 255, 128);
            }
        }

        private void wordGiveDrawWinnerBtn_Click(object sender, EventArgs e)
        {
            if(giveawayUsers.Count > 0)
            {
                Random user = new Random();
                int winnerID = user.Next(0, giveawayUsers.Count - 1);
                string winner = giveawayUsers[winnerID];
                wordGuesses.Items.Add($"The giveaway has been drawn! {winner} is the winner!");
                // Resets the Giveaway, reverting everything to as it was ready for the next.
                giveawayIsActive = false;
                giveawayType = null;
                giveawayUsers.Clear();
                numberGiveawayTimer.Stop();

                // Increases the value in the giveaway count text file.
                winnersCount++;
                writeFile.writeGiveaways(winnersCount.ToString());
                // Reset the "Start Giveaway" button.

                startWordGiveawayBtn.Text = "Start Giveaway";
                startWordGiveawayBtn.BackColor = Color.FromArgb(128, 255, 128);
            }
        }

        private void addTestBitsBtn_Click(object sender, EventArgs e)
        {
            Random randomBits = new Random();
            int testbits = randomBits.Next(1000);
            updateBitsBalance("test_user", testbits);
            chatBox.Items.Add($"[Test] 'test user' just cheered {testbits}");
        }

        private void updateBitsDictionary()
        {
            foreach (var user in bitsDictionary.Keys)
            {
                var toAdd = $"{user} [{bitsDictionary[user]}]";
                slot0Combo.Items.Add(toAdd);
                slot1Combo.Items.Add(toAdd);
                slot2Combo.Items.Add(toAdd);
                slot3Combo.Items.Add(toAdd);
                slot4Combo.Items.Add(toAdd);
                slot5Combo.Items.Add(toAdd);
                slot6Combo.Items.Add(toAdd);
                slot7Combo.Items.Add(toAdd);
                slot8Combo.Items.Add(toAdd);
                slot9Combo.Items.Add(toAdd);
                slot10Combo.Items.Add(toAdd);
                slot11Combo.Items.Add(toAdd);
                Console.WriteLine($"Found user '{user}' in dictionary - They've cheered: {bitsDictionary[user]} bits");
            }
        }

        private void updateBitsBalance(string user, int bits)
        {
            int bitsvalue = 0;
            if (bitsDictionary.TryGetValue(user, out bitsvalue))
            {
                bitsDictionary[user] += bits;
                Console.WriteLine($"Found user {user} in the Dictionary | Cheered: {bits} | Previous Value: {bitsDictionary[user] - bits}");
            }
            else
            {
                bitsDictionary.Add(user, bits);
                Console.WriteLine($"Didn't find {user} in the Dictionary, creating their key with a value of {bits}");
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            hideSlots();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            callViewersCheck(1);
        }

        private void clearLogsBtn_Click(object sender, EventArgs e)
        {
            clear.promptCleanPnl.Visible = false;
            clear.clearFilesPnl.Visible = true;
            clear.Show();
        }

        private void dash_ReSubLbl_Click(object sender, EventArgs e)
        {

        }

        private void dash_NewSubLbl_Click(object sender, EventArgs e)
        {

        }
        private void aboutDefaultCountdown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is the default value for the Subathon Countdown on the main screen. Any changes will take effect after a restart. ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void subsAddLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is the value that subscribers add to the countdown, in SECONDS! ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void whatisDuration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"This feature allows you to choose whether a re-sub adds extra time. By default, a re-sub will add a constant time, e.g. the value you enter above. However, you can now add your own multiplier for how many months they've re-subbed for. For example: re-sub amount + (months * multiplier) {Environment.NewLine} Credit: Deadpine", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void whatisPrime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"This feature allows you to enter your own values for prime subscribers. If you want prime subscribers to add more time, or less time, than a normal new sub/re-sub, then this is where you'll do it!", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void whatisBitsAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Using this feature, you can specify how much time each bit adds. If a user cheers 1, it'll add as many seconds as you specify. {Environment.NewLine} NOTE: This is calculated in seconds. Due to popular feedback, you're now able to enter values less than 1 second. There is also now a built-in converter to calculate minutes -> seconds in the Subathon Tool.", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void tipsLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is how many seconds a donation adds. Since the amount of time is native to your currency, conversion isn't required. NOTE: Please only use whole numbers (similar to bits, 1.00 of your currency can only add a minimum of 1 second.", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void whatIsCheersAdd(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is how the seconds are rounded for each cheer or tip. For example, if a user cheers 135, and the timer would add 63.2 seconds, is the .2 rounded up, or down? ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void applySubsAddConvrt_Click(object sender, EventArgs e)
        {
            int addValue;
            if(int.TryParse(subsConverterTxt.Text, out addValue))
            {
                subscriberAdds.Text = (addValue * 60).ToString();
            }
        }

        private void applyResubConvrt_Click(object sender, EventArgs e)
        {
            int addValue;
            if(int.TryParse(resubsConverterTxt.Text, out addValue))
            {
                resubValueAdd.Text = (addValue * 60).ToString();
            }
        }

        private void applyBitsConvrt_Click(object sender, EventArgs e)
        {
            int addValue;
            if (int.TryParse(bitsConverterTxt.Text, out addValue))
            {
                bitsAddText.Text = (addValue * 60).ToString();
            }
        }

        private void applyTipConvrt_Click(object sender, EventArgs e)
        {
            int addValue;
            if (int.TryParse(tipsConverterTxt.Text, out addValue))
            {
                tipsAddText.Text = (addValue * 60).ToString();
            }
        }
    }

}
namespace Buttons
{
    public class twitchChat
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
    public class giveaway
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value; 
        }
    }
    public class MadGamerBot
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
    public class settings
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
    public class About
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
    public class bitsStore
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
    public class feedback
    {
        public string Type;
        public int i;
        public void setType(string type)
        {
            this.Type = type;
        }
        public void setI(int value)
        {
            i = value;
        }
    }
}

