using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.PhoneVerification
{
    public class SendSmsRequest
    {
       
        public string sendFrom;
        public string sendTo;
        public string message;

        public SendSmsRequest(string from, string to, string message)
        {
            this.sendFrom = from;
            this.sendTo = to;
            this.message = message;
            
        }
    }
}
