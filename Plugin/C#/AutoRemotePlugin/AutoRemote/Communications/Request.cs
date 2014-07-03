using AutoRemotePlugin.AutoRemote.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Communications
{
    public class Request : Communication
    {
        public int ttl { get; set; }
        public String collapsekey { get; set; }

        /// <summary>
        /// Executes the request. Is different for every type of request. The default is to just respond with the response ResponseNoAction 
        /// </summary>
        /// <returns></returns>
        public virtual Response ExecuteRequest()
        {
            return new ResponseNoAction();
        }

        protected override string GetGCMEndpoint()
        {
            return "sendrequest";
        }
    }
}
