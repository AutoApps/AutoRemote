using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutoRemotePlugin.AutoRemote.Devices;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Formatting;
using AutoRemotePlugin.AutoRemote.Communications;


namespace AutoRemotePlugin.AutoRemote.Communications
{
    public class CommunicationBaseParams
    {
        public String sender { get { return ConstantsThatShouldBeVariables.ID; } }
        public String type { get; set; }
    }
    public class Communication
    {
        public Communication()
        {
            communication_base_params = new CommunicationBaseParams();
            communication_base_params.type = GetType().Name;
        }
        private HttpClient httpClient = new HttpClient();
        public String key { get; set; }
        public String sender { get { return communication_base_params.sender; } }
        public CommunicationBaseParams communication_base_params { get; set; }

        /// <summary>
        /// Sends the request. The text to send will vary with the request type.
        /// </summary>
        /// <param name="device">Device to send the request to</param>
        public async void Send(Device device)
        {
            key = device.key;
            var url = "http://" + device.localip + ":" + device.port + "/";

            var dataString = JsonConvert.SerializeObject(this);
            var content = new StringContent(dataString);

            //The content var will contain something like this:
            //{
            //    "id": "windows8joao",
            //    "name": "Joao's Windows 8 Machine",
            //    "type": "plugin",
            //    "localip": "192.168.1.72",
            //    "publicip": null,
            //    "port": "1820",
            //    "haswifi": true,
            //    "additional": {
            //        "iconUrl": "http://icons.iconarchive.com/icons/dakirby309/windows-8-metro/256/Folders-OS-Windows-8-Metro-icon.png",
            //        "type": "Windows 8 Plugin by joaomgcd",
            //        "canReceiveFiles": false
            //    },
            //    "ttl": 0,
            //    "collapsekey": null,
            //    "key": "YOUR_DEVICE_KEY",
            //    "sender": "windows8joao",
            //    "communication_base_params": {
            //        "sender": "windows8joao",
            //        "type": "RequestSendRegistration"
            //    }
            //}

            

            Debug.WriteLine("Sending through local ip");
            //send this as json objecto to localip
            Boolean success = await SendContent(url, content);

            //if it fails
            if (!success)
            {
                Debug.WriteLine("Couldn't send through local network. Sending through GCM");
                url = "https://autoremotejoaomgcd.appspot.com/" + GetGCMEndpoint();

                //To send trhough GCM we need to send the request as a form encoded content and add the key and sender parameters
                var postData = new List<KeyValuePair<string, string>> { 
                    new KeyValuePair<string, string>("request", dataString) ,
                    new KeyValuePair<string, string>("key", key) ,
                    new KeyValuePair<string, string>("sender", sender) ,
                };

                var contentGCM = new FormUrlEncodedContent(postData);
                success = await SendContent(url, contentGCM);
                if (success)
                {
                    Debug.WriteLine("Sent through GCM");
                }
                else
                {
                    Debug.WriteLine("Couldn't send");
                }
            }
            else
            {
                Debug.WriteLine("Sent through local ip");
            }

        }

       /// <summary>
       /// Sends some content to a certain URL via HTTP POST
       /// </summary>
       /// <param name="url">Url to send to</param>
       /// <param name="content">Content to send</param>
       /// <returns>true if successful, false if not</returns>
        private async Task<Boolean> SendContent(String url, HttpContent content)
        {
            try
            {
                var result = await httpClient.PostAsync(url, content);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        protected virtual String GetGCMEndpoint()
        {
            return null;
        }
        public static AutoRemote.Communications.Request GetRequestFromJson(string json)
        {
            var comm = JsonConvert.DeserializeObject<Communication>(json);
            var typeString = "AutoRemotePlugin.AutoRemote.Communications." + comm.communication_base_params.type;
            Type type = Type.GetType(typeString);
            Request autoRemoteRequest = (Request)JsonConvert.DeserializeObject(json, type);
            return autoRemoteRequest;
        }
    }
}
