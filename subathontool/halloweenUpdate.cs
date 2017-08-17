using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace subathontool
{
    public partial class halloweenUpdate : Form
    {
        public bool hasSeen = false;
        public bool hasCompleted = false;
        public halloweenUpdate()
        {
            InitializeComponent();
        }

        private void halloweenUpdate_Load(object sender, EventArgs e)
        {
            string infoDesc = $"So what gives? For a limited time, you can participate in our awesome event. All you have to do, is look around the software! If you've used it before, chances are you know your way around already, so this should be nice and easy for you. {Environment.NewLine} As you look around the different menus, you'll notice hidden Trick or Treat bags. Just click on 'em!Simple, right? Yeah. We thought so. {Environment.NewLine} {Environment.NewLine} So why should you bother? Well, for the limited time, you can win special goodies with it!Don't worry, we're not asking for your address, nor will you receive any real life Trick or Treat bags(BibleThump) - but this could include games! {Environment.NewLine} {Environment.NewLine} Once you've finished, you need to send Mike a message with your code - don't lose it!The most common forms of communcation are listed at the bottom of the window.";
            BackgroundImage = subathontool.Properties.Resources.halloweenbanner;
            if (hasSeen && hasCompleted != true) {
                label1.Text = "Welcome back, survivor!";
                descLbl.Text = "Don't forget, for a limited time, you can still win free games! " + Environment.NewLine + Environment.NewLine + "We see you've participated in it, but haven't completed it! Why not finish off your chance of winning a game?!" + Environment.NewLine + infoDesc;
            }
            if (hasCompleted)
            {
                completedHalloween();
            }
        }
        public void completedHalloween()
        {
            label1.Text = "Congratulations, you found them!";
            descLbl.Text = "You've successfully found them all! You've won a Steam game. Please send this code to Mike, via ONE of the contact methods below! The Halloween event is only redeemable once, so please ensure you send it while you can. If you require assistance, you can Email the support team below. Thank you for participating, and we hope you have a happy Spookathon. ";
            codeLbl.Visible = true;
            codeLbl.Text = "FE56D";
            button1.Text = "Done";
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:twitch.themadgamers@outlook.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("WARNING: If you're going to notify our Discord developer to redeem your code, please ensure you DON'T send it in public rooms - send a direct message to Mike to be secure.", "Discord Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Process.Start("http://discord.gg/0zTQF6aqIP7Af1RA");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("WARNING: If you're going to notify our page to redeem your code, please ensure you DON'T send it in a public tweet - use the Direct Messages to be secure.", "Twitter Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Process.Start("https://www.twitter.com/madgamerbot");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void halloweenUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void halloweenUpdate_VisibleChanged(object sender, EventArgs e)
        {
            if (hasCompleted)
            {
                completedHalloween();
            }
        }
    }
}
