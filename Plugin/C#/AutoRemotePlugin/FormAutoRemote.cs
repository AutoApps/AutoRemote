using AutoRemotePlugin.AutoRemote;
using AutoRemotePlugin.AutoRemote.Communications;
using AutoRemotePlugin.AutoRemote.Devices;
using AutoRemotePlugin.AutoRemote.Communications;
using Newtonsoft.Json;
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

namespace AutoRemotePlugin
{
    public partial class FormAutoRemote : Form
    {
        public FormAutoRemote()
        {
            InitializeComponent();
        }
        private static FormAutoRemote Form { get; set; }
        public static void AddLine(String line)
        {
            Form.Invoke((MethodInvoker)delegate
            {
                Form.listBoxMessages.Items.Add(line);
            });
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Form = this;


            CreateHttpServer();
            RegisterMyselfOnOtherDevice();
        }

        private static void RegisterMyselfOnOtherDevice()
        {
            //Instantiate Device (device to send stuff to). In a proper app this device should have been created with a received RequestSendRegistration
            String personalKey = "DEVICE'S PERSONAL KEY"; //see how to get it here http://joaoapps.com/autoremote/personal

            Device device = new Device { localip = "192.168.1.64", port = "1817", key = personalKey };

            //Instantiate Registration Request
            RequestSendRegistration request = new RequestSendRegistration();

            //Send registration request
            request.Send(device);
        }

        private void CreateHttpServer()
        {
            //Instantiate server
            var server = new AutoRemoteHttpServer(2);

            //Setup request handler
            server.ProcessRequest += server_ProcessRequest;

            //Start Server
            server.Start(ConstantsThatShouldBeVariables.LOCAL_IP_ADDRESS, ConstantsThatShouldBeVariables.PORT);
        }

        void server_ProcessRequest(HttpServer.IHttpRequest request, System.IO.StreamWriter responseWriter)
        {
            var sr = new StreamReader(request.Body);

            //read json from request
            var json = sr.ReadToEnd();

            //deserialize json from request
            Request autoRemoteRequest = Communication.GetRequestFromJson(json);

            //get response from request
            Response response = autoRemoteRequest.ExecuteRequest();

            //serialize response
            String jsonResponse = JsonConvert.SerializeObject(response);

            //send response back to sender
            responseWriter.Write(jsonResponse);

        }


    }
}
