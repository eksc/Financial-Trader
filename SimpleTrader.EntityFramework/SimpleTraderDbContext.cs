using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts{ get; set; }
        public DbSet<AssertTransaction> AssertTransactions{ get; set; }

        public SimpleTraderDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssertTransaction>().OwnsOne(a => a.Stock);

            base.OnModelCreating(modelBuilder);
        }
    }

}
