using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Devices
{
    public class DeviceAdditionalProperties
    {
        /// <summary>
        /// The icon URL. Has to be a publicly accessible url of a png or jpg image
        /// </summary>
        public String iconUrl { get { return Constants.ICON_URL; } }
        /// <summary>
        /// The plugin type. Something like 'windows plugin' or 'mac plugin'
        /// </summary>
        public String type { get { return Constants.PLUGIN_TYPE; } }
        /// <summary>
        /// Whether or not this plugin can receive files via HTTP PUT
        /// </summary>
        public Boolean canReceiveFiles { get { return Constants.CAN_RECEIVE_FILES; } }
    }
}
