﻿using FinanceDashboard.Data.SqlServer.Entities;
using FinanceDashboard.Utilities.EncryptorsDecryptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinanceDashboard.Data.SqlServer
{
#pragma warning restore CS1591
    public class FinanceDashboardContext : DbContext
    {
        private readonly IPasswordMethods _passwordMethods;
        private readonly string _password;
        private readonly string _passwordHash;
        private readonly string _salt;
        private readonly IConfiguration _configuration;

        //public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options, IPasswordMethods passwordMethods, IConfiguration configuration) : base(options)
        //{
        //    _salt = "==4d8dh51d9c";
        //    _password = "Password1!";
        //    _passwordMethods = passwordMethods;
        //    _passwordHash = _passwordMethods.GetHash(_password, _salt);
        //    _configuration = configuration;
        //}

        public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options) : base(options)
        {
            _salt = " @6ZD3aazp-zp";
            _password = "Password1!";
            _passwordHash = "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=";
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<SegmentLimits> SegmentLimits { get; set; }
        public DbSet<EMI> Emi { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Goals> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>().HasKey(table => new
            {
                table.AccountId,
                table.Id
            });

            builder.Entity<Audit>().HasKey(table => new
            {
                table.AuditSequence,
                table.AuditId
            });

            builder.Entity<Goals>().HasKey(table => new
            {
                table.GoalId,
                table.Id
            });

            builder.Entity<Account>().HasAlternateKey(c => c.Email).HasName("AlternateKey_Email");

            builder.Entity<Account>().HasData(DataSeeding.SeedAccount);
            builder.Entity<Subscription>().HasData(DataSeeding.SeedSubscription);

            builder.Entity<Subscription>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Subscriptions).HasPrincipalKey(z => z.AccountId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Income>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Income).HasPrincipalKey(z => z.AccountId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SegmentLimits>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.SegmentLimits).HasPrincipalKey(z => z.AccountId).OnDelete(DeleteBehavior.Restrict);
            });
            
            builder.Entity<EMI>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Emi).HasPrincipalKey(z => z.AccountId).OnDelete(DeleteBehavior.Restrict);
            });
            
            builder.Entity<Asset>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Assets).HasPrincipalKey(z => z.AccountId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Audit>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Audits).HasForeignKey(z => new { z.AccountId, z.AccountEntityId}).OnDelete(DeleteBehavior.Restrict);
            });
            
            builder.Entity<Goals>(table =>
            {
                table.HasOne(x => x.User).WithMany(y => y.Goals).HasForeignKey(z => new { z.AccountId, z.AccountEntityId}).OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceDashboardContext>
    {
        public FinanceDashboardContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../FinanceDashboard.Service/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<FinanceDashboardContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            builder.UseSqlServer(connectionString);
            return new FinanceDashboardContext(builder.Options);
        }
    }
#pragma warning restore CS1591
}