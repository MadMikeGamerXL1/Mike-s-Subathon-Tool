using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
// Server: irc.chat.twitch.tv:80
namespace subathontool
{
    class ircClient
    {
        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;
        private string username;
        private string channel;
        private logger log;
        public ircClient(string ip, int port, string username, string password)
        {
            tcpClient = new TcpClient(ip, port);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());
            if (ip != null && port != null && username != null && password != null)
            {
            //    log.logFile("Connecting to server: " + ip + " on port: " + port + " . Username: " + username + " | Password: " + password);
            }
            
            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + username);
            outputStream.WriteLine("USER " + username + " 8 * :" + username);
            outputStream.Flush();
            this.username = username;

            
        }
        public void joinRoom(string channel)
        {
            outputStream.WriteLine("JOIN #" + channel);
            if (channel != null) {
               // log.logFile("Sending: JOIN #" + channel + " to server.");
                Console.WriteLine("Sending: JOIN #" + channel + " to server.");
            }
            outputStream.Flush();
            this.channel = channel;

        }
        public void sendIrcMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }
        public void sendChatMessage(string message)
        {
            sendIrcMessage(":" + username + "!" + username + "@" + username + "irc.chat.twitch.tv PRIVMSG #" + channel + " :" + message);
        }
        public string readMessage()
        {
            string message = inputStream.ReadLine();
            // log.logFile("[IRC MSG] " + inputStream.ReadLine());
            Console.WriteLine("[IRC MSG] " + inputStream.ReadLine());
            
            return message;
        }
    }
}