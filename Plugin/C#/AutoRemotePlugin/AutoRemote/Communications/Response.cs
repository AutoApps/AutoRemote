using AutoRemotePlugin.AutoRemote.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Communications
{
    public class Response : Communication
    {
        //If the request resulted in error, set the error here
        public String responseError { get; set; }
    }
}
