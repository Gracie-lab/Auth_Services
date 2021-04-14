using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Exceptions
{
    public class SessionException : Exception
    {
        public SessionException(string message) : base(message)
        {

        }
    }
}
