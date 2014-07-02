using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpServer;
using System.Threading;
using System.Net;
using System.IO;

namespace AutoRemotePlugin
{
    public class AutoRemoteHttpServer : IDisposable
    {
        public static IPAddress[] MyLocalIPs
        {
            get
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                return localIPs.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

            }
        }
        public String MyPublicIP
        {
            get
            {

                return null;
            }
        }
        public int Port { get; set; }
        private HttpServer.HttpListener _listener;
        public event Action UPNPFailed;
        public int MaxThreads { get; set; }

        public AutoRemoteHttpServer(int maxThreads)
        {
            this.MaxThreads = maxThreads;
        }

        public void Start(String localIp, int port)
        {
            Port = port;
            _listener = HttpServer.HttpListener.Create(System.Net.IPAddress.Any, port);
            _listener.RequestReceived += new EventHandler<RequestEventArgs>(_listener_RequestReceived);
            _listener.Start(MaxThreads);
        }



        void _listener_RequestReceived(object sender, RequestEventArgs e)
        {
            if (ProcessRequest != null)
            {
                IHttpClientContext context = (IHttpClientContext)sender;
                IHttpRequest request = e.Request;
                IHttpResponse response = request.CreateResponse(context);
                StreamWriter writer = new StreamWriter(response.Body);
                ProcessRequest(request, writer);
                writer.Flush();
                response.Send();
                writer.Close();
            }
        }

        private void OnUPNPFailed()
        {
            if (UPNPFailed != null)
            {
                UPNPFailed();
            }
        }

        public void Dispose()
        { Stop(); }

        public void Stop()
        {
            _listener.Stop();
        }


        public event Action<IHttpRequest, StreamWriter> ProcessRequest;
    }
}
