using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Devices
{
    public class Device
    {
        /// <summary>
        /// Personal key for the device. Check here for more details: http://joaoapps.com/autoremote/personal/
        /// </summary>
        public String key { get; set; }
        /// <summary>
        /// Name of the device
        /// </summary>
        public String name { get; set; }
        /// <summary>
        /// Device's Public IP 
        /// </summary>
        public String publicip { get; set; }
        /// <summary>
        /// Device's Local IP 
        /// </summary>
        public String localip { get; set; }
        /// <summary>
        /// Device's Port
        /// </summary>
        public String port { get; set; }

    }
}
