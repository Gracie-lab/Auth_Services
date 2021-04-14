using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Models
{
    [Owned]
    public class OTP
    {
        public OTP(String phoneNumber, bool verificationStatus)
        {
            this.phoneNumber = phoneNumber;
            this.verificationStatus = verificationStatus;
        }

        [Key]
        public string phoneNumber { get; set; }

        public bool verificationStatus { get; set; }

        public string OtpCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Expires { get; set; }
    }
}
