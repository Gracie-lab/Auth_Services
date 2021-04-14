using Danfohq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danfohq.DataContexts
{
    public class AccountContext : DbContext
    {
        public AccountContext()
        {

        }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("A FALLBACK CONNECTION STRING");
            }
        }

       // protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
          // base.OnModelCreating(modelBuilder);

        //}


        public DbSet<Account> Accounts { get; set; }

        public List<Account> GetAccounts()
        {
            return Accounts.ToList();
        }
    }
}
