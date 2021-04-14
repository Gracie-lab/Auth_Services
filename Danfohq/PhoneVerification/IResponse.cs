using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.PhoneVerification
{
    public interface IResponse
    {

        string Status { get; set; }

        bool CanUpdate { get; }

        Task UpdateAsync();
    }
}
