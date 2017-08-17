using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace subathontool
{
    public partial class feedback : Form
    {
        logger log = new logger();
        updatecheck updateCheck = new updatecheck();
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public feedback()
        {
            InitializeComponent();
        }

        private void feedback_Load(object sender, EventArgs e)
        {
            
        }
        private void sendEmail(string sender, string senderEmail, string senderUsername, string senderFeedback)
        {
            
            long value = Convert.ToInt64(getRam());
            MailMessage feedback = new MailMessage();
            feedback.From = new MailAddress("visual.support@hotmail.com", "Feedback Tool");
            feedback.To.Add("twitch.themadgamers@outlook.com");
            feedback.Subject = "Feedback report from " + sender + "[" + senderEmail + "]";
            feedback.Body = "Automated feedback form sent via Subathon Tool [" + updateCheck.currentVersion() + "]" +
                Environment.NewLine + "Sender Name: " + sender +
                Environment.NewLine + "Sender Email: " + senderEmail +
                Environment.NewLine + "Sender Username: " + senderUsername +
                Environment.NewLine + "Sender Feedback: " + senderFeedback +
                Environment.NewLine + "System Information: " +
                Environment.NewLine + "OS Version:" + getOS() +
                Environment.NewLine + "64-bit OS: " + System.Environment.Is64BitOperatingSystem +
                Environment.NewLine + "Processor: " + SendBackProcessorName() +
                Environment.NewLine + "Memory Available: " + FormatBytes(value);
            feedback.Attachments.Add(new Attachment(appdataFolder + "/subathontool/log.txt"));
            feedback.Attachments.Add(new Attachment(appdataFolder + "/subathontool/settings.json"));
            if (File.Exists(appdataFolder + "/subathontool/apilogs.txt"))
            {
                feedback.Attachments.Add(new Attachment(appdataFolder + "/subathontool/apilogs.txt"));
            }
            
            
            
        }
        public static string SendBackProcessorName()
        {
            ManagementObjectSearcher mosProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            string Procname = null;

            foreach (ManagementObject moProcessor in mosProcessor.Get())
            {
                if (moProcessor["name"] != null)
                {
                    Procname = moProcessor["name"].ToString();

                }

            }

            return Procname;
        }
        public static string getRam()
        {
            string Query = "SELECT Capacity FROM Win32_PhysicalMemory";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(Query);

            UInt64 Capacity = 0;
            foreach (ManagementObject WniPART in searcher.Get())
            {
                Capacity += Convert.ToUInt64(WniPART.Properties["Capacity"].Value);
            }

            return Capacity.ToString();
        }
        public static string getOS()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
        bool hasSent = false;
        private void sendBtn_Click(object sender, EventArgs e)
        {
            bool sendFailed = false;
            if(senderNameTxt.Text == null || senderNameTxt.Text == "")
            {
                label2.ForeColor = Color.Red;
                sendFailed = true;
            }
            if (senderUsername.Text == null || senderUsername.Text == "")
            {
                label4.ForeColor = Color.Red;
                sendFailed = true;
            }
            if (senderReport.Text == null || senderReport.Text == "")
            {
                label5.ForeColor = Color.Red;
                sendFailed = true;
            }
            if (sendFailed) { MessageBox.Show("You're missing some information!", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if (!hasSent)
                {
                    try
                    {
                        sendEmail(senderNameTxt.Text, senderEmail.Text, senderUsername.Text, senderReport.Text);
                        this.Hide();
                        hasSent = true;
                        senderUsername.Text = "";
                        senderReport.Text = "";
                        senderNameTxt.Text = "";
                        senderEmail.Text = "";
                    }
                    catch (Exception err)
                    {

                        MessageBox.Show("Encountered error sending message: " + err.GetBaseException());
                    }
                }
                else
                {
                    MessageBox.Show("Thank you for submitting the report! If you included an Email address, we will contact you shortly!", "Report Sent Successfully", MessageBoxButtons.OK);
                }
            }

        }

        private void emailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("You're not required to enter an email address. However, if you do, we can contact you to keep you updated, based on the reason for contact. (if you contacted for support, we can help you. If you suggested a feature, I will email you to keep you updated on when it is being/has been added). " + Environment.NewLine + "If you don't provide an email, but the feedback report appears to be requiring assistance, we will contact you via other means (usually Twitter or Twitch PM's.", "Not Required, but encouraged", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void senderNameTxt_MouseClick(object sender, MouseEventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void senderUsername_MouseClick(object sender, MouseEventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void senderReport_MouseClick(object sender, MouseEventArgs e)
        {
            label5.ForeColor = Color.Black;
        }

        private void feedback_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void trickOrTreat1_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
