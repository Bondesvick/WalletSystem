using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Data
{
    /// <summary>
    ///
    /// </summary>
    public class DataContext : IdentityDbContext<User>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DbSet<Wallet> Wallets { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DbSet<Funding> Fundings { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.ApplyConfiguration(new SeedCurrency());
        //}
    }
}