using AutoRemotePlugin.AutoRemote.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRemotePlugin.AutoRemote.Communications
{
    public class Message : Request
    {
        /// <summary>
        /// Message text
        /// </summary>
        public String message { get; set; }
        /// <summary>
        /// Optional password. Shouldn't allow incoming messages unless this matches with a user defined password.
        /// </summary>
        public String password { get; set; }
        /// <summary>
        /// Optional Files array. 
        /// </summary>
        public String[] files { get; set; }

        /// <summary>
        /// In this demo it simply writes a line in the main form. You should handle the message in which ever way you want.
        /// </summary>
        /// <returns>The default ResponseNoAction from the super class</returns>
        public override Response ExecuteRequest()
        {
            var baseResponse = base.ExecuteRequest();
            FormAutoRemote.AddLine(message);
            return baseResponse;
        }
    }

    
}
