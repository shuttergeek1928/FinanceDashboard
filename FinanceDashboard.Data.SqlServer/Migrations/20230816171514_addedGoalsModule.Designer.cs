﻿// <auto-generated />
using System;
using FinanceDashboard.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    [DbContext(typeof(FinanceDashboardContext))]
    [Migration("20230816171514_addedGoalsModule")]
    partial class addedGoalsModule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashingSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("InvalidPasswordCount")
                        .HasColumnType("smallint");

                    b.Property<bool?>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHashHistory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondFactorKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("SecondFactorValidated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("VerifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountId", "Id");

                    b.HasAlternateKey("Email")
                        .HasName("AlternateKey_Email");

                    b.ToTable("Account");

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            Id = new Guid("53ab364d-587f-4d40-957a-e0b88636b7ce"),
                            CreatedOn = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "atishay1928@outlook.com",
                            FirstName = "Atishay",
                            HashingSalt = "@6ZD3aazp-zp",
                            LastName = "Vishwakarma",
                            MobileNumber = "9827766387",
                            Name = "Atishay Vishwakarma",
                            PasswordHash = "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=",
                            PasswordHashHistory = "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=,"
                        });
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Asset", b =>
                {
                    b.Property<Guid>("AssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AbsoluteReturns")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DissolvedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DissolvedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DissolvedReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("InvestedValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<decimal>("ReturnPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AssetId");

                    b.HasIndex("AccountId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Audit", b =>
                {
                    b.Property<long>("AuditSequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AuditSequence"));

                    b.Property<Guid>("AuditId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuditSequence", "AuditId");

                    b.HasIndex("AccountId", "AccountEntityId");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.EMI", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BillingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CanceledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CanceledOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CompletionDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("EmiName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GstRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("InstallmentDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdateBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Emi");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Goals", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AchievedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CanceledBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CanceledOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GoalId", "Id");

                    b.HasIndex("AccountId", "AccountEntityId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Income", b =>
                {
                    b.Property<Guid>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CreditCycle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Creditor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CredtiDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpiredBy")
                        .HasColumnType("int");

                    b.Property<string>("ExpiringReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Expiry")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("IncomeBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IncomeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncomeId");

                    b.HasIndex("AccountId");

                    b.ToTable("Income");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.SegmentLimits", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("EmiLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubscriptionLimit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SegmentLimits");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BillingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CanceledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CanceledOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastUpdateBy")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RenewalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RenewalCycle")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RenewalDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SubscribedOnEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscribedOnMobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Subscription");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2f5bd117-debc-4109-be28-b2a7cee51025"),
                            AccountId = 1,
                            Amount = 500m,
                            BillingDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsExpired = false,
                            Password = "Password1!",
                            RenewalAmount = 350m,
                            RenewalCycle = 1,
                            RenewalDate = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SubscribedOnEmail = "atishay1928@outlook.com",
                            SubscribedOnMobileNumber = "9827766387",
                            SubscriptionName = "Netflix"
                        });
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Asset", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Assets")
                        .HasForeignKey("AccountId")
                        .HasPrincipalKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Audit", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Audits")
                        .HasForeignKey("AccountId", "AccountEntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.EMI", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Emi")
                        .HasForeignKey("AccountId")
                        .HasPrincipalKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Goals", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Goals")
                        .HasForeignKey("AccountId", "AccountEntityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Income", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Income")
                        .HasForeignKey("AccountId")
                        .HasPrincipalKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.SegmentLimits", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("SegmentLimits")
                        .HasForeignKey("AccountId")
                        .HasPrincipalKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Subscription", b =>
                {
                    b.HasOne("FinanceDashboard.Data.SqlServer.Entities.Account", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("AccountId")
                        .HasPrincipalKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Account", b =>
                {
                    b.Navigation("Assets");

                    b.Navigation("Audits");

                    b.Navigation("Emi");

                    b.Navigation("Goals");

                    b.Navigation("Income");

                    b.Navigation("SegmentLimits");

                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
