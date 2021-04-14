using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Danfohq.PhoneVerification
{
    public class TextResponse : IResponse
    {
        private string accountsid;

       
        public string Status { get; set; }

        public bool CanUpdate { get; set; }

        public TextResponse(MessageResource call)
        {
            SetMessage(call);
        }

        private void SetMessage(MessageResource call)
        {
            accountsid = call.Sid;
            Status = call.Status.ToString();
        }
        public async Task UpdateAsync()
        {
            var call = await MessageResource.FetchAsync(accountsid);

            SetMessage(call);
        }
    }
}
