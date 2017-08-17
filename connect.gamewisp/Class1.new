using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace connect.gamewisp
{
    public class connection
    {
        //https://api.gamewisp.com/pub/v1/oauth/authorize?client_id=18f5ac4aa00324a7867aaa6a7ec797df79946c5&redirect_uri=http://themadgamers.co.uk/subathontool.html&response_type=code&scope=read_only,subscriber_read_limited&state=ASKDLFJsisisks23k
        public bool wasSuccessful = false;
        public bool finished = false;
        public string error;
        private string userDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        WebClient gamewispConnection = new WebClient();
        public bool hasSynced = false;
        public string OAuth;
        public string refreshToken;

        public class successJSON
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string expires_at { get; set; }
            
        }
        public class errorJSON
        {
            public string error { get; set; }
            public string error_description { get; set; }
            
        }
        public class gamewispAccount
        {
            public string channel { get; set; }
            public string OAuth { get; set; }
            public string refreshToken { get; set; }
            public bool hasConnected { get; set; }
        }
        public void checkFile()
        {
            try
            {
                if (Directory.Exists(appdataFolder + "/subathontool"))
                {

                    using (StreamReader file = File.OpenText(appdataFolder + "/subathontool/settings.json"))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        gamewispAccount data2 = (gamewispAccount)serializer.Deserialize(file, typeof(gamewispAccount));
                        hasSynced = data2.hasConnected;
                        OAuth = data2.OAuth;
                        refreshToken = data2.refreshToken;                        
                    }
                }
                else
                {
                    Directory.CreateDirectory(appdataFolder + "/subathontool");

                }
                // data data1 = JsonConvert.DeserializeObject<data>(File.ReadAllText("data/settings.json"));


            }
            catch (Exception error)
            {
                //     MessageBox.Show("Error occured reading settings file. " + error.HResult + " - Check the logs file for details. ", "Error " + error.HResult, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.Write($"Encountered error getting options - \n {error} \n");
            }
        }
        public void connect()
        {
            checkFile();
            if (hasSynced)
            { //User has already gone through the authorization stage.

            }
            else // User hasn't synced their account already 
            {
                authorization Authorization = new authorization();
                Authorization.Show();
                Authorization.browser.AddressChanged += (sender, e) =>
                {
                    var url = Authorization.browser.Address.ToString();
                    string callback;
                    if (url.StartsWith("http://dev.themadgamers.co.uk"))
                    {
                        callback = url;
                        Console.WriteLine("Callback URL: " + callback);
                        if (url.Contains("?error=access"))
                        {
                            wasSuccessful = false;
                            error = "The resource owner or authorization server denied the request";
                            finished = true;
                            Authorization.Invoke((MethodInvoker)delegate
                            {
                                // Authorization.browser.Dispose();
                                Authorization.Close();
                            });
                            MessageBox.Show("Access was denied to the Subathon Tool. Please click 'Accept' in order to continue. If this continues, please contact support.", "Access Denied to Authorize Gamewisp channel.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            String webURL = String.Format(url);
                            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(webURL);
                            request.Credentials = CredentialCache.DefaultCredentials;

                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            if(response.StatusCode == HttpStatusCode.OK)
                            {
                                using (Stream dataStream = response.GetResponseStream())
                                {
                                    using (StreamReader reader = new StreamReader(dataStream, Encoding.UTF8))
                                    {
                                        String aResponse = reader.ReadToEnd();
                                        Console.Write("Found code: " + aResponse + "\n");
                                        try
                                        {
                                            var token = JsonConvert.DeserializeObject<successJSON>(aResponse);
                                            Console.Write($"Found Access Token: {token.access_token} \n Found token type: {token.token_type} \n Refresh Token: {token.refresh_token} \n Expires in: {token.expires_in} \n Expires at: {token.expires_at} \n");
                                            Console.WriteLine("success");
                                            wasSuccessful = true;
                                            finished = true;
                                        }
                                        catch(Exception Error)
                                        {
                                            Console.Write("Found error: " + aResponse + "\n");
                                            try
                                            {
                                                var accessError = JsonConvert.DeserializeObject<errorJSON>(aResponse);
                                                Console.Write($"Error: {accessError.error} \n Error Description: {accessError.error_description} \n");
                                                wasSuccessful = false;
                                                error = accessError.error_description;
                                                finished = true;
                                            }
                                            catch(Exception err)
                                            {
                                                Console.WriteLine("Couldn't parse error from string - server error? - " + err);
                                                wasSuccessful = false;
                                                error = "Couldn't parse error string - assume server error";
                                                finished = true;
                                            }
                                            
                                        }

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Encountered problem requesting the code - " + response.StatusCode.ToString());
                                wasSuccessful = false;
                                error = "Encountered problem requesting the code - " + response.StatusCode.ToString();
                                finished = true;
                            }
                            response.Close();
                            

                           // MessageBox.Show("You have successfully authenticated the Subathon Tool. Success URL: " + url);
                            //http://themadgamers.co.uk/subathontool.html?code=YblDv1kY9IkCvvyghLcNZMO2UKDXhv5MZbqCsc1R&state=ASKDLFJsisisks23k
                            
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine(url);
                    }
                };
                Authorization.FormClosing += (sender, e) =>
                {
                    Authorization.Controls.Remove(Authorization.browser);
                    if (!finished)
                    {
                        error = "The user cancelled.";
                        wasSuccessful = false;
                        finished = true;
                    }
                };
            }

        }
    }
}
