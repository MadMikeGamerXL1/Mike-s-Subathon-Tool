using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace subathontool
{
    public partial class giveaway : Form
    {
        private readonly main _main;
        logger log = new logger();
        dynamic json;
        dynamic jsonParse;
        int maxNumber;
        public bool giveawayIsActive = false;
        int rng;
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public giveaway(main mainwindow)
        {
            
            InitializeComponent();
            this._main = mainwindow;
        }

        private void giveaway_Load(object sender, EventArgs e)
        {
            checkSettings();
        }

        private void refreshList_Click(object sender, EventArgs e)
        {
            viewerLists.Items.Clear();
            try
            {
                System.Media.SoundPlayer play = new System.Media.SoundPlayer("./data/refreshBtn.wav");
                play.Play();
                
                
            }
            catch (Exception error)
            {
                log.logFile("Couldn't play file horse-neigh - " + error);
            }
            if (currentGiveaway.Text != null || currentGiveaway.Text != "")
            {
                checkList("https://tmi.twitch.tv/group/user/" + currentGiveaway.Text +  "/chatters");
            }

            totalUsers.Text = "Total Viewers:" + viewerLists.Items.Count;
        }

        private void viewJsonBtn_Click(object sender, EventArgs e)
        {
            var txtWindow = new textWindow();
            txtWindow.textBox1.ReadOnly = true;
            txtWindow.textBox1.Text = json;
            
            txtWindow.Show();
        }

        private void checkList(string url)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = wc.DownloadString(url);
                    jsonParse = JsonConvert.DeserializeObject(json);

                    
                    var viewerCheck = jsonParse.chatters.viewers;

                    
                    for (var i = 0; i < jsonParse.chatters.moderators.Count; i++)
                    {
                        viewerLists.Items.Add(jsonParse.chatters.moderators[i]);
                    }
                    for (var i = 0; i < viewerCheck.Count; i++)
                    {

                        viewerLists.Items.Add(jsonParse.chatters.viewers[i]);

                    }
                    totalUsers.Text = "Total Viewers:" +  viewerLists.Items.Count;


                }
                catch (Exception error)
                {
                    log.logFile("Parsing URL tmi.twitch.tv failed - " + Environment.NewLine + error);
                }
            }
        }

        private void howToUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This needs to be changed in the settings window from the main menu. ", "How to Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void checkSettings()
        {
            try
            {

                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));

                using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                {

                    JsonSerializer serializer = new JsonSerializer();
                    data data2 = (data)serializer.Deserialize(file, typeof(data));
                    data objResponse = JsonConvert.DeserializeObject<data>(file.ReadToEnd().ToString());
                    if (data2.channelforgiveaway != null || data2.channelforgiveaway != "")
                    {
                        // giveawayChannel.Text = data2.channelforgiveaway;
                        currentGiveaway.Text = data2.channelforgiveaway;
                    }
                    if (data2.checkViewerList == true)
                    {
                        if (currentGiveaway.Text != null || currentGiveaway.Text != "")
                        {
                            checkList("https://tmi.twitch.tv/group/user/" + currentGiveaway.Text + "/chatters");
                        }
                    }
                    maxNumber = data2.maxNumberRNG;
                    
                }
            }
            catch (Exception error)
            {
                log.logFile("Caught Exception while reading JSON file: "
                    + error);
              //  MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            public int maxNumberRNG { get; set; }

        }
        int winnersCount = 0;
        private void beginUserBtn_Click(object sender, EventArgs e)
        {
            var winner = new winner();
            winner.Show();
            winnersCount++;
            log.writeGiveaways(winnersCount.ToString());
            var random = new Random();
            
            var randomNumber = random.Next(0, viewerLists.Items.Count);
            viewerLists.SelectedIndex = randomNumber;
            winner.username.Text = viewerLists.SelectedItem.ToString();
            winner.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void removeUserBtn_Click(object sender, EventArgs e)
        {
            try
            {
                viewerLists.Items.Remove(viewerLists.SelectedItem);
            }catch (Exception error)
            {
                log.logFile("Couldn't remove user from list - " + error);
            }
        }

        private void viewerLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            totalUsers.Text = "Total Viewers:" + viewerLists.Items.Count;
        }

        private void generateRNG_Click(object sender, EventArgs e)
        {
            Random RNG = new Random();
            if (maxNumber < 1)
            {
                MessageBox.Show("Could not initialise Giveaway - Max number is smaller than 1. Please set a value in the Settings menu.", "Giveaway Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                var numToGuess = RNG.Next(1, maxNumber);
                rng = numToGuess;
                if (!hideRNG.Checked)
                {
                    RNGOutput.Text = numToGuess.ToString();
                }
                generateRNG.Enabled = false;
                giveawayIsActive = true;
                timer1.Start();
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
        string mainMessage;
        string mainSpeaker;
        string lastReadMessage = "";
        string lastReadUser = "";
        

        private void timer1_Tick(object sender, EventArgs e)
        {
           // checkMessages(_main);
            int value1;
            if (_main.lastMessage == null || _main.lastSpeaker == null)
            {
              //  Console.WriteLine($"Last message or last speaker is null. {_main.lastSpeaker} - {_main.lastMessage}");
            }
            else
            {
                if (int.TryParse(_main.lastMessage, out value1) && _main.lastMessage != null)
                {

                    if (_main.lastMessage == lastReadMessage)
                    {
                      //  Console.WriteLine($"Message is same as previous. {_main.lastSpeaker} - {_main.lastMessage} - {lastReadMessage} {lastReadUser}");

                    }
                    else
                    {
                        lastReadUser = _main.lastSpeaker;
                        lastReadMessage = _main.lastMessage;
                        userGuesses.Items.Add($"{_main.lastSpeaker} : {_main.lastMessage}");
                       // Console.WriteLine("User entered thing");
                        if (_main.lastMessage == rng.ToString())
                        {
                            timer1.Stop();
                            MessageBox.Show("User guessed right!");
                        }

                    }

                }
                else
                {
                  //  Console.WriteLine($"Message is not a number {_main.lastSpeaker} - {_main.lastMessage}");
                }
            }
            

        }

        private void userGiveGroup_Enter(object sender, EventArgs e)
        {

        }

        private void numberGiveGroup_Enter(object sender, EventArgs e)
        {

        }
    }
}
/**
using(WebClient wc = new WebClient())
            {
                try {
                    dynamic json = wc.DownloadString("https://tmi.twitch.tv/group/user/reninsane/chatters");
                    dynamic jsonParse = JsonConvert.DeserializeObject(json);
                    
                    var txtWindow = new textWindow();
                    var viewerCheck = jsonParse.chatters.viewers;

                    for (var i = 0; i < viewerCheck.Count; i++)
                    {
                        txtWindow.textBox1.Text = txtWindow.textBox1.Text + Environment.NewLine + jsonParse.chatters.viewers[i];
                        viewerLists.Items.Add(jsonParse.chatters.viewers[i]);

                    }
                    for (var i = 0; i < jsonParse.chatters.moderators.Count; i++)
                    {
                        modList.Items.Add(jsonParse.chatters.moderators[i]);
                    }
                    
                    txtWindow.textBox1.ReadOnly = true;
                    txtWindow.Show();
                }
                catch(Exception error)
                {
                    log.logFile("Parsing URL tmi.twitch.tv failed - " + Environment.NewLine + error);
                }
            }
    **/
