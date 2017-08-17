using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subathontool
{
    public partial class clearLogs : Form
    {
        List<string> logList = new List<string>();
        logger Logger = new logger();
        string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public clearLogs()
        {
            InitializeComponent();
            if(Directory.Exists(logFolder + "/subathontool/"))
            {
                Logger.initCheck();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            promptCleanPnl.Visible = false;
            clearFilesPnl.Visible = true;
        }

        private void clearLogs_Load(object sender, EventArgs e)
        {
            promptCleanPnl.Controls.Remove(clearFilesPnl);
            this.Controls.Add(clearFilesPnl);
            logList.AddRange(Directory.GetFiles(logFolder + "/subathontool/logs"));
            foreach (string log in logList)
            {
                logsList.Items.Add(log.Split((char)92).Last());
                Console.WriteLine("Added " + log + " to the logs list.");
            }
        }
        bool forceClose = false;
        private void cancelClearBtn_Click(object sender, EventArgs e)
        {
            forceClose = false;
            this.Close();
        }
        public void exit()
        {
            forceClose = true;
            this.Close();
        }

        private void clearLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (forceClose)
            {
                this.Dispose();
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void clearLogBtn_Click(object sender, EventArgs e)
        {
            foreach (string log in logList)
            {
                try
                {
                    if (log.ToLower().Contains(Logger.getCurrentLog().ToLower()))
                    {
                        Logger.logFile("Skipping the deletion of '" + Logger.getCurrentLog() + "' - this file is the current log file.");
                        Console.WriteLine("Skipping the deletion of '" + Logger.getCurrentLog() + "' - this file is the current log file.");
                    }else
                    {
                        File.Delete(log);
                        logsList.Items.Remove(log.Split((char)92).Last());
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Encountered error: " + err);
                }
            }
            cancelClearBtn.Visible = false;
            clearLogBtn.Text = "Done";
            Console.WriteLine("Removing EventListener for clearLogBtn.Click() event");
            clearLogBtn.Click -= clearLogBtn_Click;
            Console.WriteLine("Removed EventListener for clearLogBtn.Click() event. Adding finishedClear() event");
            clearLogBtn.Click += finishedClear;
            Console.WriteLine("Added EventListener for clearLogBtn.Click() event");
        }
        private void finishedClear(object sender, EventArgs e)
        {
            this.Close();
        }
        string exportedLocation = null;
        private void exportLogBtn_Click(object sender, EventArgs e)
        {
            ExportBrowser.Description = "Please select a location to Export the chosen log file.";
            ExportBrowser.ShowDialog();
            try
            {
            exportedLocation = ExportBrowser.SelectedPath + $"/EXPORT_{logsList.SelectedItem.ToString()}";
            }catch(Exception err)
            {
                MessageBox.Show("We encountered an error while setting the Export Location. Please ensure you selected a file to export, and try again. If the problem persists, contact the support team. ", "Unexpected Error Setting Export Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.logFile("Encountered an error while setting exportLocation string. " + err);
            }
            
            try
            {
                File.Copy(logFolder + "/subathontool/logs/" + logsList.SelectedItem.ToString(), exportedLocation);
            }catch (System.UnauthorizedAccessException err)
            {
                MessageBox.Show("We couldn't export the selected log file. The directory you chose requires elevated permissions. We recommend exporting to somewhere like the Desktop, and manually transferring it here. \n \n " + err, "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.logFile("Couldn't export file - " + err);
                Console.Write("\n Couldn't export file from logs folder. Encountered the following error: \n " + err + "\n");
            }
            catch (Exception err)
            {
                MessageBox.Show("We couldn't export the selected log file. Please ensure you have a target location to export to, and that you have selected a file to export. \n \n " + err, "Unexpected Error exporting Log file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.logFile("Couldn't export file - " + err);
                Console.Write("\n Couldn't export file from logs folder. Encountered the following error: \n " + err + "\n");
            }
        }
        
    }
}
