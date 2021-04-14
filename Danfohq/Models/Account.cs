using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Danfohq.Models
{
    public class Account
    {
        public Account(long Id, string FirstName, string LastName, string PhoneNumber, string Password)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
        }

       
       public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string VerificationStatus { get; set; }


       
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [JsonIgnore]
        public string token { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

        public OTP otp;
    }
}
