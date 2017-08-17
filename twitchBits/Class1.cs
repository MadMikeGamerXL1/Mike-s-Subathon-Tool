using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;
using System.Timers;
using System.IO;
using Newtonsoft.Json;

namespace twitchBits
{

    public class bits
    {
        WebSocket socket = new WebSocket("wss://pubsub-edge.twitch.tv");
        bool isConnected = false;
        Timer timer;
        Timer pingTimer;
        string oAuth;
        
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        string pingMessage = @"
            {  
                ""type"": ""PING""
            }";
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
            public string bitsOAuth { get; set; }
        }
        public bool checkBitsEnabled()
        {
            
            try
            {
                if (Directory.Exists(appdataFolder + "/subathontool"))
                {
                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        data data2 = (data)serializer.Deserialize(file, typeof(data));
                        if (data2.useBitsAPI)
                        {
                            oAuth = data2.bitsOAuth;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                else
                {
                    Directory.CreateDirectory(appdataFolder + "/subathontool");
                    checkBitsEnabled();
                    return false;
                }
            }
            catch (Exception err)
            {
               // log.logFile("Failed to check if bits is enabled - " + err);
                return false;
            }
        }
        public string testOAuth(int channelID)
        {
            if (checkBitsEnabled())
            {
                string messageSubscribe = @"
            {
            ""type"": ""LISTEN"",
            ""nonce"": ""fslfksfjfjsjfsje"",
            ""data"": {
                ""topics"": [""channel-bitsevents." + channelID + @"""],
                ""auth_token"": """ + oAuth + @"""
                }
            }
            ";
                return messageSubscribe;
            }
            else
            {
                Console.WriteLine("Error: Bits aren't enabled. :( ");
                return "error";
            }
            
        }
        public void checkChannel(int ID)
        {
            if (checkBitsEnabled())
            {
                channelID = ID;
                socket.AutoSendPingInterval = 240000;
                socket.EnableAutoSendPing = true;
                //channelToConnect = channel;
                // Ren - 32775646
                socket.Opened += (sender, e) =>
                {
                    timer = new Timer();
                    timer.Interval = 5000;
                    timer.Elapsed += threadLoop;
                    timer.Start();

                    pingTimer = new Timer();
                    pingTimer.Elapsed += pingLoop;
                    pingTimer.Interval = 7500;
                    pingTimer.Start();

                    Console.WriteLine("Connected to Twitch PubSub - " + socket.State);
                    isConnected = true;
                };

                socket.Error += (sender, e) =>
                {
                    Console.Write("Encountered error - " + e.Exception.Message);
                };
                socket.MessageReceived += messageReceived;
                socket.Closed += closed;
                socket.DataReceived += dataReceived;

                socket.Open();

            }

        }
        public void dataReceived(object sender, DataReceivedEventArgs e)
        {
           Console.WriteLine("Data received - " + e.Data);
        }
        public void messageReceived(object sender, MessageReceivedEventArgs e)
        {
           
            if (e.Message.Contains("PONG"))
            {
                Console.WriteLine("Connection is still alive.");
            }else
            {
                Console.WriteLine("Message received - " + e.Message + Environment.NewLine);
            }
        }
        public void closed(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected from websocket");
            timer.Stop();
        }
        bool hasSent = false;
        string channelToConnect;
        int channelID;
        public void threadLoop(object sender, EventArgs e)
        { // 240000
          // timer.Stop();
            string messageSubscribe = @"
            {
            ""type"": ""LISTEN"",
            ""nonce"": ""officialloop"",
            ""data"": {
                ""topics"": [""whispers." + channelID + @"""],
                ""auth_token"": """ + oAuth + @"""
                }
            }
            ";
            if (isConnected && messageSubscribe != "" && hasSent != true)
            {
                socket.Send(messageSubscribe);
                hasSent = true;
                Console.Write("Sent message to subscribe - " + messageSubscribe + Environment.NewLine + "State: " + socket.State + Environment.NewLine);
                messageSubscribe = "";
                
            }else
            {
                Console.WriteLine("Not connected, or message has been sent already.");
            }
           
        }
        public void pingLoop(object sender, EventArgs e)
        {
            if (isConnected)
            {
                socket.Send(pingMessage);
                Console.WriteLine("Sending PING - " + pingMessage);
            }
            else
            {
                Console.WriteLine("not connected.");
            }
        }
        public void disconnect(int ID, string reason)
        {
            socket.Close(ID, reason);
        }
        public void disconnect()
        {
            socket.Close();
        }
        public void disconnect(string reason)
        {
            socket.Close(reason);
        }
    }
    
    public class returnUser
    {
        public string user;
        public string channel;
        public int user_id;
        public int channel_id;
        public string time;
        public string messageFromUser;
        public int messageBits;
        public int totalUserBits;
        public string context;
    }
    public class toSend
    {
        /**
         * {
  "type": "LISTEN",
  "nonce": "44h1k13746815ab1r2",
  "data": {
    "topics": ["whispers.test_account","video-playback.lirik"],
    "auth_token": "...",
  }
}
         * **/

        public string type { get; set; }
        public string nonce { get; set; }
        public data data { get; set; }
    }
    public class data
    {
        public string[] topics { get; set; }
        public string auth_token { get; set; }
    }
}
