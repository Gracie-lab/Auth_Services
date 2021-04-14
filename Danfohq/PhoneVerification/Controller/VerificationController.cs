using Danfohq.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.PhoneVerification
{
    [ApiController]
    [Route ("verify")]
    public class VerificationController : ControllerBase

    {

        private IVerificationService verificationService = new VerificationService(PhoneVerificationConstants.TWILIO_ACCOUNT_SID, PhoneVerificationConstants.TWILIO_AUTH_TOKEN);


        [HttpPost("send-otp")]
        public IActionResult SendOtp([FromBody] SendSmsRequest sendSmsRequest)
        {
            var response = verificationService.SendSms(sendSmsRequest);
            return Ok(response);
        }

        [HttpPost("verify-phone")]
        public IActionResult VerifyPhoneNumber([FromBody] string otp)
        {
            var response = verificationService.VerifyPhoneNumber(otp);

            return Ok(response);
        }
    }
}
