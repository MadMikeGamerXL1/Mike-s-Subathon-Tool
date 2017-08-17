using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace subathontool
{
     
    
    public partial class settingsForm : Form
    {
        // logger log = new logger();
        bitsStoreOptions bitsOptions = new bitsStoreOptions();
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            resetUptimeRadio.Checked = true;
            roundingCheers.SelectedIndex = 1;
            warnPanel.Dock = DockStyle.Fill;
            userDocPath.AppendText(userDocsFolder + @"\subathontool\sounds\");
            userDocPath.SelectionStart = userDocPath.TextLength;
            userDocPath.ScrollToCaret();
            try
            {
                if(Directory.Exists("data"))
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
                        bitsAdd.Text = data2.eachBitAdds.ToString();
                        donationCommands.Checked = data2.useDonationCmd;
                        tipsAdd.Text = data2.tipsAdd.ToString();
                        roundingCheers.SelectedIndex = data2.roundSecondsIndex;
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

                
            } catch(Exception error)
            {
                // log.logFile("Caught Exception while reading JSON file: " 
                 //   + error);
           //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.ShowIcon = false;
            var mainWindow = new main();
            chatUsername.Enabled = useTwitchAccount.Checked;
            chatOAuth.Enabled = useTwitchAccount.Checked;
            oauthKey.Enabled = useTwitchAccount.Checked;

            int cellNum = 0;
            int rowNum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellNum += 1;
                dataGridView1.Rows[rowNum].Cells[0].Value = cellNum;
                rowNum += 1;
            }
        }
        private void tabControlHover(object sender, EventArgs e)
        {
            
            toolTip1.Show("Connect to your Twitch Channel", tabPage1);
        }
        private void onLoseFocus(object sender, EventArgs e)
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
                   // var log = new logger();
                    channelJoin.Text = "#" + channelJoin.Text;
                    Console.WriteLine("Detected no # symbol in textbox.");
                   
                   // log.logFile("Detected no # symbol in Channel textbox");
                }

            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
           // var log = new logger();
           // log.logFile("Exiting Settings. Current values; \n Channel: " + channelJoin.Text);
            Console.WriteLine("Exiting Settings. \n Channel Text: " + channelJoin.Text);
            
            this.Close();
            
        }
        private void boxTextChange(object sender, KeyEventArgs e)
        {
            
        }

        private void applyBtn_Click(object sender, EventArgs e)
        {
            int bitsAddInt = 0;
            decimal bitsAddDec = 0;
            if (bitsAddCheckBox.Checked)
            {
                
                try
                {
                    decimal.TryParse(bitsAdd.Text, out bitsAddDec);
                    
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
               //     log.logFile("Encountered error saving bits add option - " + err);
                    bitsAdd.Text = "0";
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
              //  log.logFile("[WARN] maxNumberRNG detected as null or NaN. Defaulting to 100");
            }


            if (allowChatCmd.Checked || announceReSubs.Checked || announceSubs.Checked)
            {
                if (!useTwitchAccount.Checked) {
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
            for (var i = 0; i < regularsList.Items.Count; i++) {
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
            var tips = int.TryParse(tipsAdd.Text, out tipsAddInt);

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
                };
                string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                //write string to file
                System.IO.File.WriteAllText(appdataFolder + "/subathontool/settings.json", json);
                this.Close();

            } catch(Exception error)
            {
                //log.logFile("Exception caught while setting JSON object: " + 
                  //  error);   
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
            public bool announceBits { get;set; }
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
        }
        
        private void linkRefreshSoundPlay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is the sound that plays when the viewer list is refreshed on the 'Giveaway Tool' window. ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutDefaultCountdown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is the default value for the Subathon Countdown on the main screen. Any changes will take effect after a restart. ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is the value that subscribers add to the countdown, in SECONDS! ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Activate or deactivate gaming mode - any message boxes are disabled unless invoked by the user. (i.e. if you click a question mark. ) This setting is useful for fullscreen games, where message boxes might interrupt gameplay. ", "What is this? ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Play a sound when the subathon timer reaches 0 - place this in the DOCUMENTS FOLDER! " + Environment.NewLine +  " I.e. " + userDocsFolder + @"\subathontool\sounds", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void userDocPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (enableSR.Checked)
            {
                srOptions.Enabled = true;

            }
            else
            {
                srOptions.Enabled = false;
            }
        }

        private void addRegButton_Click(object sender, EventArgs e)
        {
            
            applyReg.Visible = true;
            addRegName.Visible = true;
            addRegPanel.Visible = true;
            applyBtn.Enabled = false;
        }

        private void applyReg_Click(object sender, EventArgs e)
        {
            if (addRegName.Text == null || addRegName.Text == "")
            {

            }
            else
            {
                regularsList.Items.Add(addRegName.Text);
                applyReg.Visible = false;
                addRegName.Visible = false;
                addRegPanel.Visible = false;
                applyBtn.Enabled = true;
            }
            
        }

        private void addRegName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                addRegName.Visible = false;
                applyReg.Visible = false;
                addRegPanel.Visible = false;
                applyBtn.Enabled = true;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (addRegName.Text == null || addRegName.Text == "")
                {

                }
                else
                {
                    regularsList.Items.Add(addRegName.Text);
                    applyReg.Visible = false;
                    addRegName.Visible = false;
                    addRegPanel.Visible = false;
                    applyBtn.Enabled = true;
                }
            }
        }

        private void removeReg_Click(object sender, EventArgs e)
        {
            regularsList.Items.Remove(regularsList.SelectedItem);
        }

        private void playSoundFinishCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (playSoundFinishCheck.Checked) {

                timerFileName.ReadOnly = false;
                userDocPath.Enabled = true;
            }
            else
            {

                timerFileName.ReadOnly = true;
                userDocPath.Enabled = false;
            }
        }

        private void oauthKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.twitchapps.com/tmi");
        }

        private void useTwitchAccount_CheckedChanged(object sender, EventArgs e)
        {
            chatUsername.Enabled = useTwitchAccount.Checked;
            chatOAuth.Enabled = useTwitchAccount.Checked;
            oauthKey.Enabled = useTwitchAccount.Checked;

            allowChatCmd.Enabled = useTwitchAccount.Checked;
            announceReSubs.Enabled = useTwitchAccount.Checked;
            announceSubs.Enabled = useTwitchAccount.Checked;
            previewCmd.Enabled = useTwitchAccount.Checked;
            previewReply.Enabled = useTwitchAccount.Checked;
            label6.Enabled = useTwitchAccount.Checked;
            label9.Enabled = useTwitchAccount.Checked;
            label10.Enabled = useTwitchAccount.Checked;
        }

        private void hideWarn_Click(object sender, EventArgs e)
        {
            warnPanel.Visible = false;

        }

        private void separateWindowChat_CheckedChanged(object sender, EventArgs e)
        {
            if (!separateWindowChat.Checked)
            {
                homeScreenChat.Checked = true;
            }
        }

        private void homeScreenChat_CheckedChanged(object sender, EventArgs e)
        {
            if (!homeScreenChat.Checked)
            {
                separateWindowChat.Checked = true;
            }
        }

        private void allowChatCmd_CheckedChanged(object sender, EventArgs e)
        {
            if (allowChatCmd.Checked)
            {
                previewCmd.Enabled = true;
                previewReply.Enabled = true;
            }
            else
            {
                previewCmd.Enabled = false;
                previewReply.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            int cellNum = 0;
            int rowNum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellNum += 1;
                dataGridView1.Rows[rowNum].Cells[0].Value = cellNum;
                rowNum += 1;
            }
        }

        private void dataGridView1_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            int cellNum = 0;
            int rowNum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellNum += 1;
                dataGridView1.Rows[rowNum].Cells[0].Value = cellNum;
                rowNum += 1;
            }
        }

        private void editBits_Click(object sender, EventArgs e)
        {
            
            if (bitsOptions.Visible)
            {
                bitsOptions.BringToFront();
            }
            else
            {
                bitsOptions.Show();
            }
        }

        private void allowGamewisp_CheckedChanged(object sender, EventArgs e)
        {
            gamewispResubLbl.Enabled = allowGamewisp.Checked;
            gamewispResubValue.Enabled = allowGamewisp.Checked;
            gamewispSubLbl.Enabled = allowGamewisp.Checked;
            gamewispSubsValue.Enabled = allowGamewisp.Checked;
        }

        private void monthsAffectAmount_CheckedChanged(object sender, EventArgs e)
        {
            multiplierResubs.Enabled = monthsAffectAmount.Checked;
        }

        private void whatisBitsAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"Using this feature, you can specify how much time each bit adds. If a user cheers 1, it'll add as many seconds as you specify. {Environment.NewLine} NOTE: This is calculated in seconds. There is currently no built-in converter to calculate minutes -> seconds in the Subathon Tool. 1 second is the lowest it will accept.");
        }
        private void whatisDuration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show($"This feature allows you to choose whether a re-sub adds extra time. By default, a re-sub will add a constant time, e.g. the value you enter above. However, you can now add your own multiplier for how many months they've re-subbed for. For example: re-sub amount + (months * multiplier) {Environment.NewLine} Credit: Deadpine");
        }

        private void countdownSettings_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            connectOauthLbl.Enabled = connUseBits.Checked;
            connBitsOAuth.Enabled = connUseBits.Checked;
        }

        private void bitsAddCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bitsAdd.Enabled = bitsAddCheckBox.Checked;
        }

        private void announceCheerCheck_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void announceReSubs_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tipsLinkLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is how many seconds a donation adds. Since the amount of time is native to your currency, conversion isn't required. NOTE: Please only use whole numbers (similar to bits, 1.00 of your currency can only add a minimum of 1 second.", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void whatIsCheersAdd(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This is how the seconds are rounded for each cheer. For example, if a user cheers 135, and the timer would add 63.2 seconds, is the .2 rounded up, or down? ", "What is this?", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void resetUptimeRadio_CheckedChanged(object sender, EventArgs e)
        {
            resetUptimeRadio.Checked = !pauseUptimeRadio.Checked;
            pauseUptimeRadio.Checked = !resetUptimeRadio.Checked;
        }

        private void pauseUptimeRadio_CheckedChanged(object sender, EventArgs e)
        {
            pauseUptimeRadio.Checked = !resetUptimeRadio.Checked;
            resetUptimeRadio.Checked = !pauseUptimeRadio.Checked;
        }

        private void advancedOptions_Click(object sender, EventArgs e)
        {

        }
    }
}
