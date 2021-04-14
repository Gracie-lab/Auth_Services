using Danfohq.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Services
{
    interface IAccountService
    {
        AuthorizeResponse Authorize(LoginRequest request, string ipAddress);
        AuthorizeResponse RefreshToken(string token, string ipAddress);


    
    }
}
