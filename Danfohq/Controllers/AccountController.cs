using Danfohq.Auth;
using Danfohq.Dtos;
using Danfohq.Exceptions;
using Danfohq.Models;
using Danfohq.PhoneVerification;
using Danfohq.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService = new AccountService();

        private IVerificationService verificationService = new VerificationService(PhoneVerificationConstants.TWILIO_ACCOUNT_SID, PhoneVerificationConstants.TWILIO_AUTH_TOKEN);

       // public AccountController(IAccountService accountService)
        //{
          //  this.accountService = accountService;
        //}

        private string getIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authorize([FromBody] LoginRequest request)
        {
            AuthorizeResponse response = null;
            try
            {
                response = accountService.Authorize(request, getIpAddress());

                if (response == null)
                    return BadRequest(new { message = "Incorrect phone number or password" });
            }
            catch (InvalidInputException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
           

            try
            {
                var response = accountService.RefreshToken(refreshToken, getIpAddress());

            }
            catch(SessionException e)
            {
                Console.WriteLine(e.Message);
                
            }

            return Ok();

            
        }



    }
}
