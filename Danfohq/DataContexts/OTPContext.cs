using Danfohq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.DataContexts
{
   public class OTPContext : DbContext
    {

        public OTPContext()
        {

        }
        public OTPContext(DbContextOptions<OTPContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("A FALLBACK CONNECTION STRING");
            }
        }

        public DbSet<OTP> Otps { get; set; }
    }
}
