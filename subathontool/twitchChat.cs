using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace subathontool
{
    public partial class twitchChat : Form
    {
        TcpClient tcpClient;
        StreamReader reader;
        System.Threading.Thread t;
        public bool softwareExit = false;
        StreamWriter writer;
        int totalMsg;
        int newSubValue;
        bool useTimer = false;
        string other;
        bool isGamingModeActive = false;
        int reSubValue;
        int totalSubs;
        string oAuth;
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        logger log = new logger();
        updatecheck updatecheck = new updatecheck();
        //  ircClient irc = new ircClient("irc.chat.twitch.tv", 80, "justinfan4495", "Kappa123");
        public bool toChange = false;

        private readonly main _main;
        
        public string lastmessage = null;

      /**  public twitchChat()
        {
            InitializeComponent();
        } **/
        public void updateTime(int value)
        {
            var main = new main();
            main.timeLeft += value;
        }

        public twitchChat(main mainwindow)
           
        {
            InitializeComponent();
            this._main = mainwindow;

        }
        
        public void threadLoop ()
        {
            while (true)
            {


                if (!tcpClient.Connected)
                {
                    reconnect(checkChannelJson());
                    log.logFile("Detected client disconnected - reconnecting.");
                    isConnected = false;
                    userChatMsg.Enabled = false;
                }

                if (tcpClient.Available > 0 || reader.Peek() >= 0)
                {
                    var message = reader.ReadLine();

                    log.logFile("[IRC]" + message);
                Console.WriteLine("[IRC] " + message);
                // string speaker = message.Substring(1, message.IndexOf("!") - 1);
                var iColon = message.IndexOf(":", 1);
                bool isAdded = false;
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
                                        this.addListItem((char)character + "  " + message);
                                        totalMsg += 1;
                                        isAdded = true;
                                        Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                        //   mainWindow.addTime(900);
                                        //   mainWindow.timeLeft += 900;

                                        //  mainWindow.subathonCount.Stop();
                                        //   mainWindow.subathonCount.Start();
                                        this._main.addTime(newSubValue, "sub");
                                        var splitMsg = message.Split((char) 32);
                                        if(!isGamingModeActive)
                                        {
                                            MessageBox.Show("New sub! - " + splitMsg[3].TrimStart((char) 58));
                                        }
                                        
                                        Console.WriteLine("Subscriber detected, adding " + newSubValue);
                                        log.logFile("New subscriber detected - " + message);
                                        totalSubs++;
                                    }
                                    else
                                    {
                                        log.logFile("Fake subscription detected!");
                                    }


                                }
                                else if (message.Contains("subscribed for"))
                                {

                                    if (speaker == "twitchnotify")
                                    {
                                        char character = '\u2605';
                                        // MessageBox.Show("User just subscribed to channel!");
                                        this.addListItem((char)character + "  " + message);
                                        totalMsg += 1;
                                        isAdded = true;
                                        Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                        //   mainWindow.addTime(900);
                                        //   mainWindow.timeLeft += 900;

                                        //  mainWindow.subathonCount.Stop();
                                        //   mainWindow.subathonCount.Start();
                                        this._main.addTime(reSubValue, "resub");
                                        totalSubs++;
                                        var splitMsg = message.Split((char)32);
                                        if (!isGamingModeActive)
                                        {
                                            MessageBox.Show("Re-sub! - " + splitMsg[3].TrimStart((char)58) + " subscribed for " + splitMsg[6] + " months");
                                        }


                                        Console.WriteLine("Re sub detected - Adding " + reSubValue);
                                        log.logFile("Re-subscription detected - " + splitMsg[3] + "subscribed for " + splitMsg[6] + "months");

                                    }
                                    else
                                    {
                                    log.logFile("Fake subscription detected!");
                                    }


                                }
                                else if (message.Contains("!subathontool") || message.Contains(">subathontool") || message.Contains("`subathontool"))
                                {
                                    sendMessage($"{chan} is currently using Mike's Subathon Tool [version {updatecheck.currentVersion()}] | The total subscribers are: {totalSubs} | The timer is currently running at: {File.ReadAllText(userDocsFolder + "/subathontool/countdown.txt")}");
                                }
                                else
                                {
                                    this.addListItem(message);
                                    isAdded = true;
                                    totalMsg += 1;

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
                if (!isAdded)
                {
                    this.addListItem(message);
                }
                
                

            }
            }
        }
        delegate void setTextCallback(string text);
        private void addListItem(string message)
        {
           var thisText = "Twitch Chat  [Total Messages: " + totalMsg + " - Total Subs: " + totalSubs + "]";

           // listBox1.Items.Add(message);

            if (this.listBox1.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(addListItem);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.listBox1.Items.Add(message);
                
                if (listBox1.Items.Count < 0)
                {

                }
                else
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    
                }
            }
/**
            if(this.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(addListItem);
                this.Invoke(d, new object[] { thisText });

            }
            else
            {
                this.Text = thisText;
            } **/
        }
        Point newpoint = new Point
        {
            X = 624,
            Y = 420,
        };
        private void twitchChat_Load(object sender, EventArgs e)
        {
            reconnect(checkChannelJson());
             timer1.Start();
         //   t = new System.Threading.Thread(threadLoop);
            
            this.t = new System.Threading.Thread(new System.Threading.ThreadStart(this.threadLoop));
            this.t.Start();

           // userChatMsg.Enabled = false;
          //  sendMsg.Enabled = false;
        }
        bool canContinue = true;
        
        void timer1_Tick(object sender, EventArgs e)
        {

            var thisText = "Twitch Chat  [Total Messages: " + totalMsg + " - Total Subs: " + totalSubs + "]";
            this.Text = thisText;

            if (useTimer)
            {
                if (!tcpClient.Connected)
                {
                    isConnected = false;
                 /**   userChatMsg.Enabled = false;
                    sendMsg.Enabled = false;**/
                    reconnect(checkChannelJson());
                    log.logFile("Detected client disconnected - reconnecting.");
                }

                if (tcpClient.Available > 0 || reader.Peek() >= 0)
                {
                    var message = reader.ReadLine();

                    log.logFile("[IRC]" + message);
                    Console.WriteLine("[IRC] " + message);
                    // string speaker = message.Substring(1, message.IndexOf("!") - 1);
                    var iColon = message.IndexOf(":", 1);
                    bool isAdded = false;
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
                                        listBox1.Items.Add((char)character + "  " + message);
                                        totalMsg += 1;
                                        isAdded = true;
                                        Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                        //   mainWindow.addTime(900);
                                        //   mainWindow.timeLeft += 900;

                                        //  mainWindow.subathonCount.Stop();
                                        //   mainWindow.subathonCount.Start();
                                        this._main.addTime(newSubValue, "sub");

                                        MessageBox.Show("New sub!");
                                        log.logFile("New subscriber detected - " + message);
                                        totalSubs++;
                                    }
                                    else
                                    {
                                        log.logFile("Fake subscription detected!");
                                    }


                                }
                                else if (message.Contains("subscribed for"))
                                {

                                    if (speaker == "twitchnotify")
                                    {
                                        char character = '\u2605';
                                        // MessageBox.Show("User just subscribed to channel!");
                                        listBox1.Items.Add((char)character + "  " + message);
                                        totalMsg += 1;
                                        isAdded = true;
                                        Console.WriteLine("Detected subscriber - updating mainwindow timer");
                                        //   mainWindow.addTime(900);
                                        //   mainWindow.timeLeft += 900;

                                        //  mainWindow.subathonCount.Stop();
                                        //   mainWindow.subathonCount.Start();
                                        this._main.addTime(reSubValue, "resub");
                                        totalSubs++;
                                        MessageBox.Show("Re-sub!");
                                        log.logFile("Re-subscription detected - " + message);

                                    }
                                    else
                                    {
                                        log.logFile("Fake subscription detected!");
                                    }


                                }
                                else
                                {
                                    listBox1.Items.Add(message);
                                    isAdded = true;
                                    totalMsg += 1;

                                }
                            }

                        }
                    }
                    if (!isAdded)
                    {
                        listBox1.Items.Add(message);
                    }
                    if (listBox1.Items.Count < 0)
                    {

                    }
                    else
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                    this.Text = "Twitch Chat  [Total Messages: " + totalMsg + " - Total Subs: " + totalSubs + "]";

                }
            }
          
        }
        string username = "justinfan3456";
        string password = "Kappa123";
        string chan;
        bool isConnected = false;
        private void reconnect(string channel)
        {
            isConnected = true;
            log.logFile("Connecting to servers...");

            tcpClient = new TcpClient("irc.chat.twitch.tv", 80);
            tcpClient.ReceiveTimeout = -1;
            log.logFile("Connected.");
            userChatMsg.Enabled = true;
            sendMsg.Enabled = true;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            
            
            writer.WriteLine("PASS " + password + Environment.NewLine 
                + "NICK " + username + Environment.NewLine
                + "USER " + username + " 8 * :" + username);
            writer.Flush();
            writer.WriteLine("JOIN " + channel);
            chan = channel;
            writer.Flush();
            
            
            

        }
        void sendMessage(string message)
        {
            writer.WriteLine($":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG {chan} :{message}");
            writer.Flush();
            Console.WriteLine("Attempting to send message " + $":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG #{chan} :{message}");
        }
        public string checkChannelJson()
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
                        isGamingModeActive = data2.gamingMode;
                        reSubValue = data2.resubsAddValue;
                        newSubValue = data2.subscriberAddsValue;
                        if (data2.useTwitchDetails)
                        {
                            username = data2.twitchUsername;
                            password = data2.userOAuth;
                        }
                        if (data2.channeltojoin != null || data2.channeltojoin != "")
                        {
                            return (data2.channeltojoin);
                        }
                        else
                        {
                            return (null);
                        }
                        
                        
                        
                    }
                }
                else
                {
                    Directory.CreateDirectory(appdataFolder + "/subathontool");
                    return (null);
                }
                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));


            }
            catch (Exception error)
            {
                log.logFile("Caught Exception while reading JSON file: "
                    + error);
                //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (null);
            }

        }
        public class data
        {
            public string channeltojoin { get; set; }
            public string channelforgiveaway { get; set; }
            public string timerCompleteSoundFile { get; set; }
            public bool checkViewerList { get; set; }
            public bool refreshViewerSound { get; set; }
            public bool defaultCountdownValue { get; set; }
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
            public bool useBitsAPI { get; set; }
        }

        private void twitchChat_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }
        
        private void twitchChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(softwareExit)
            {
                try
                {
                    t.Abort();
                    
                }
                catch (System.Threading.ThreadAbortException exc)
                {
                    other = exc.ToString();
                }
                
            }
            else
            {
                this.Hide();
                this.Parent = null;
                e.Cancel = true;
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sendMsg_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                sendMessage(userChatMsg.Text);
            }
        }
    }

}
