using AutoRemotePlugin.AutoRemote.Devices;
using AutoRemotePlugin.AutoRemote.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Communications
{
   public class RequestSendRegistration : Request
    {
        public String id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public String localip { get; set; }
        public String publicip { get; set; }
        public String port { get; set; }
        public Boolean haswifi { get; set; }
        public DeviceAdditionalProperties additional { get; set; }

        public RequestSendRegistration()
        {
            //Any unique Id.
            id = ConstantsThatShouldBeVariables.ID;
            //Name you want to appear in AutoRemote
            name = ConstantsThatShouldBeVariables.NAME;
            //This is always "plugin"
            type = Constants.DEVICE_TYPE;
            //Your local IP. Should be dinamically gotten if possible
            localip = ConstantsThatShouldBeVariables.LOCAL_IP_ADDRESS;
            //Public IP. Should be dinamically gotten if possible
            publicip = null;
            //Any port you want. Should make it user configurable
            port = ConstantsThatShouldBeVariables.PORT.ToString();
            //Always set to true
            haswifi = true;
            //Instantiate the additional device properties that are needed for plugins
            additional = new DeviceAdditionalProperties();

        }

    }
}
