using Danfohq.DataContexts;
using Danfohq.Exceptions;
using Danfohq.Models;
using Danfohq.PhoneVerification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Danfohq.Auth
{
    public class VerificationService : IVerificationService
    {
        private string accountsid;

        private string accountAuth;

        private Random random = new Random();

        private AccountContext accountContext = new AccountContext();
        public VerificationService(string accountsid, string authToken)
        {
            this.accountsid = accountsid;
            this.accountAuth = authToken;
        }


        public bool IsInitialized { get; set; }
        public bool CanSendSms { get { return true; } }

        public bool FromNumberRequired { get { return true; } }

        bool IVerificationService.CanSendSms { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void Init()
        {
            TwilioClient.Init(PhoneVerificationConstants.TWILIO_ACCOUNT_SID, PhoneVerificationConstants.TWILIO_AUTH_TOKEN);

            IsInitialized = true;
        }

        public string setOtp()
        {
            string path = Path.GetRandomFileName();
            return path.Substring(0, 6);

        }

        public async Task<IResponse> SendSmsAsync(SendSmsRequest sendSmsRequest)
        {
            var sendFrom = new PhoneNumber(sendSmsRequest.sendFrom);
            var sendTo = new PhoneNumber(sendSmsRequest.sendTo);
            sendSmsRequest.message = setOtp();
          
            Account ownedAccount = accountContext.Accounts.SingleOrDefault(account => account.PhoneNumber == sendSmsRequest.sendTo);
            if (ownedAccount == null) throw new AccountNotFoundException("Phone number not recognized");
            else
            {
                ownedAccount.otp.OtpCode = sendSmsRequest.message;
                var text = await MessageResource.CreateAsync(
              sendTo,
              from: sendFrom,
              body: sendSmsRequest.message);

                ownedAccount.otp.CreatedAt = DateTime.Now;
                ownedAccount.otp.Expires = DateTime.Now.AddMinutes(1);
               

                return new TextResponse(text);
            }

            
        }

        public async Task<IResponse> SendSms(SendSmsRequest sendSmsRequest)
        {
            Init();
            var response =await SendSmsAsync(sendSmsRequest);
            

            return response;
        }

        public String VerifyPhoneNumber(string otp)
        {
            Account ownedAccount = accountContext.Accounts.SingleOrDefault(user => user.otp.OtpCode == otp);

            if (ownedAccount != null && ownedAccount.otp.Expires != DateTime.Now)
            {
                ownedAccount.VerificationStatus = VerificationStatus.VERIFIED;
                ownedAccount.otp.verificationStatus = true;
            }
            else throw new AccountNotFoundException("Invalid OTP. Click the resend button to receive a valid OTP");

            return ownedAccount.VerificationStatus;
        }

    }
}
