using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.Models
{
    [Owned]
    public class RefreshToken
    {
        public int Id { get; set; }

        public bool IsExpired { get; set; }

        public DateTime CreatedAT { get; set; }

        public string token { get; set; }

        public DateTime Expires { get; set; }

        public bool IsActive { get; set; }

        public DateTime RevokedAt { get; set; }

        public String ReplacedByToken { get; set; }
    }
}
