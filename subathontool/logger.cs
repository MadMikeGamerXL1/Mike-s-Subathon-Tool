using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace subathontool
{
    class logger
    {
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        bool hasCreated = false;
        string fileName;
        string logFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public void initCheck()
        {
            
            fileName = File.ReadAllText(appdataFolder + "/subathontool/currentlog.txt");
            fileName += ".log";

        }
        public string getCurrentLog()
        {
            return fileName;
        }
        public void logUpdate(string text)
        {
            initCheck();
            if (Directory.Exists(logFolder + "/subathontool/logs") == true)
            {
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/" + fileName, FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing - " + e.Message);
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                Console.WriteLine("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "] " + text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
            }
            else
            {
                Directory.CreateDirectory(logFolder + "/subathontool/logs");
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing");
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                Console.WriteLine(text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
            }
        }
        public void logFile(string text)
        {
            initCheck();
            if (Directory.Exists(logFolder + "/subathontool/logs") == true)
            {
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/" + fileName, FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing - " + e.Message);
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                //  Console.WriteLine("This is a line of text");
                //    Console.WriteLine("Everything written to Console.Write() or");
                //  Console.WriteLine("Console.WriteLine() will be written to a file");
                Console.WriteLine("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "] " + text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
                // Console.WriteLine("Done");
            }
            else
            {
                Directory.CreateDirectory(logFolder + "/subathontool/logs");
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing");
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                //   Console.WriteLine("This is a line of text");
                // Console.WriteLine("Everything written to Console.Write() or");
                // Console.WriteLine("Console.WriteLine() will be written to a file");
                Console.WriteLine(text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
               // Console.WriteLine("Done");
            }
        }

        public void logAPI(string text)
        {
            initCheck();
            if (Directory.Exists(logFolder + "/subathontool/logs") == true)
            {
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/apilogs.txt", FileMode.Append, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing - " + e.Message);
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                
                Console.WriteLine("[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "] " + text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
               // Console.WriteLine("Done");
            }
            else
            {
                Directory.CreateDirectory(logFolder + "/subathontool/logs");
                FileStream ostrm;
                StreamWriter writer;
                TextWriter oldOut = Console.Out;
                try
                {

                    ostrm = new FileStream(logFolder + "/subathontool/logs/apilogs.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open Redirect.txt for writing");
                    Console.WriteLine(e.Message);
                    return;
                }
                Console.SetOut(writer);
                
                Console.WriteLine(text);
                Console.SetOut(oldOut);
                writer.Close();
                ostrm.Close();
               // Console.WriteLine("Done");
            }
        }
        public void writeGiveaways(string value)
        {
            
            File.WriteAllText(userDocsFolder + "/subathontool/giveawaycount.txt", value);
        }

        public void subValue (string value)
        {
            File.WriteAllText(userDocsFolder + "/subathontool/subvalues.txt", value);
         /**   this.logFile("Writing to subvalues.txt file with value: " + value);
            Console.WriteLine("Writing to subvalues.txt file with value: " + value); **/
        }
        public void resubValue (string value)
        {
            File.WriteAllText(userDocsFolder + "/subathontool/resubvalues.txt", value);
       /**     this.logFile("Writing to resubvalues.txt file with value: " + value);
            Console.WriteLine("Writing to resubvalues.txt file with value: " + value); **/
        }
        public void updateCountdown (string path, string value)
        {
            File.WriteAllText(path, value);
        }
        public void updateUptime(string path, string value)
        {
            File.WriteAllText(path, value);
        }
        public void writeTotalSubs(string value)
        {
            File.WriteAllText(userDocsFolder + "/subathontool/totalsubs.txt", value);
        }public void writeTotalReSubs(string value)
        {
            File.WriteAllText(userDocsFolder + "/subathontool/totalresubs.txt", value);
        }
        public void writeUptime(int value)
        {
            if(!Directory.Exists(userDocsFolder + "/subathontool/restores"))
            {
                Directory.CreateDirectory(userDocsFolder + "/subathontool/restores");
            }
            File.WriteAllText(userDocsFolder + "/subathontool/restores/rawuptime.txt", value.ToString());
        }
        public void writeCountdown(int value)
        {
            if(!Directory.Exists(userDocsFolder + "/subathontool/restores"))
            {
                Directory.CreateDirectory(userDocsFolder + "/subathontool/restores");
            }
            File.WriteAllText(userDocsFolder + "/subathontool/restores/rawcountdown.txt", value.ToString());
        }
        public int fetchCountdown()
        {
            try
            {
                string tempRead = File.ReadAllText(userDocsFolder + "/subathontool/restores/rawcountdown.txt");
                int toReturn;
                if (int.TryParse(tempRead, out toReturn))
                {
                    return toReturn;
                }
                else
                {
                    return 0;
                }
            }
            catch (System.IO.DirectoryNotFoundException error)
            {
                logFile("Couldn't find directory for 'rawcountdown.txt' - " + error);
                Directory.CreateDirectory(userDocsFolder + "/subathontool/restores");
                logFile("Created directory '/subathontool/restores'");
                return 0;
            }
            catch (System.IO.FileNotFoundException error)
            {
                logFile("Couldn't find file 'rawcountdown.txt' - " + error);
                File.Create(userDocsFolder + "/subathontool/restores/rawcountdown.txt");
                logFile("Created file 'rawcountdown.txt'");
                return 0;
            }
        }
        public string fetchRawUptime()
        {
            try
            {
                string read = File.ReadAllText(userDocsFolder + "/subathontool/restores/rawuptime.txt");
                return read;
            }
            catch(Exception err)
            {
                logFile("Encountered Error reading rawuptime.txt - returning a default of 0");
                logFile(err.ToString());
                return "0";
            }
            
        }
    }
}
