using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.PhoneVerification
{
    interface IVerificationService
    {
        bool IsInitialized { get; set; }

        bool CanSendSms { get; set; }

        bool FromNumberRequired { get; }

        void Init();

        Task<IResponse> SendSmsAsync(SendSmsRequest sendSmsRequest);

        string ToString();

        Task<IResponse> SendSms(SendSmsRequest sendSmsRequest);

        string VerifyPhoneNumber( string otp);

        String setOtp();
    }
}
