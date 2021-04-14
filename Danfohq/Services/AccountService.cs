using Danfohq.Auth;
using Danfohq.DataContexts;
using Danfohq.Dtos;
using Danfohq.Exceptions;
using Danfohq.Models;
using Danfohq.PhoneVerification;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Danfohq.Services
{
    public class AccountService : IAccountService
    {
        public AccountService() { }
        private AccountContext accountContext = new AccountContext();

        private IVerificationService verificationService = new VerificationService(PhoneVerificationConstants.TWILIO_ACCOUNT_SID, PhoneVerificationConstants.TWILIO_AUTH_TOKEN);

        private AuthConstants authConstants = new AuthConstants();

        Random random = new Random();

        public AccountService(AccountContext accountContext, Microsoft.Extensions.Options.IOptions<AuthConstants> authConstants)
        {
            this.accountContext = accountContext;
            this.authConstants = authConstants.Value;
        }


        private bool ValidatePhoneNumber(string phoneNumber)
        {
            string regex = @"^[0]\d{10}$";

            RegexOptions options = RegexOptions.Singleline;

            return Regex.Match(phoneNumber, @"^[0]\\d{10}$").Success;
        }

        public void RegisterNewAccount(LoginRequest request)
        {
            Account newAccount = new Account(random.Next(), null, null, request.PhoneNumber);
            accountContext.Accounts.Add(newAccount);
            accountContext.SaveChanges();
            SendSmsRequest smsRequest = new SendSmsRequest(null, request.PhoneNumber, verificationService.setOtp());
            verificationService.SendSms(smsRequest);
            
        }

        public Dtos.AuthorizeResponse Authorize(LoginRequest request, string ipAddress)
        {
           Account account = accountContext.Accounts.FirstOrDefault(user => user.PhoneNumber == request.PhoneNumber);

            if (!ValidatePhoneNumber(request.PhoneNumber))
            {
                throw new InvalidInputException("Phone number is not valid");
            }else
            {
                 //TO-DO : create user if account does not exist
                if (account == null)
                {
                    RegisterNewAccount(request);

                }
                var jwtToken = generateJwtToken(account);
                var refreshToken = generateRefreshToken(ipAddress);
                account.token = jwtToken;
                accountContext.Update(account);
                accountContext.SaveChanges();
                return new AuthorizeResponse(account, jwtToken);
            }
        }

        public String generateJwtToken(Account account)
        {
            var tokenProvider = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(authConstants.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString())

                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenProvider.CreateToken(tokenDescriptor);
            return tokenProvider.WriteToken(token);
        }

        public AuthorizeResponse RefreshToken(string token, string ipAddress)
        {
            //find user by given token
            var account = accountContext.Accounts.SingleOrDefault(user => user.token == token);

            //TO:DO - should user be created if token is not valid?
            if (account == null) throw new AccountNotFoundException("Account does not exist");

            var refreshToken = account.RefreshTokens.Single(user => user.token == token);

            if (!refreshToken.IsActive) throw new SessionException("Session has expired");

            var newToken = generateRefreshToken(ipAddress);
            refreshToken.RevokedAt = DateTime.Now;
            refreshToken.ReplacedByToken = newToken.token;
            account.RefreshTokens.Add(newToken);
            accountContext.Update(account);
            accountContext.SaveChanges();

            var jwtToken = generateJwtToken(account);

            return new AuthorizeResponse(account, newToken.token);
            throw new NotImplementedException();
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.Now.AddDays(7),
                    CreatedAT = DateTime.Now
                };
            }
            
        }
    }
}
