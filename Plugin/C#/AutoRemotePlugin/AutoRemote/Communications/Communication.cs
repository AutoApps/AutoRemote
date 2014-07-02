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

            var result = await httpClient.PostAsync(url, content);

            Debug.WriteLine(result.StatusCode);
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
