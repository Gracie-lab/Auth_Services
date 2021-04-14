using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Dtos
{
    public class LoginRequest
    {
        [Required]
        public string PhoneNumber { get; set; }

    }
}
