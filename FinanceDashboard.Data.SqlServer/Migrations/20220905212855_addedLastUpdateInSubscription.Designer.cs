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
    [Migration("20220905212855_addedLastUpdateInSubscription")]
    partial class addedLastUpdateInSubscription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FinanceDashboard.Data.SqlServer.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"), 1L, 1);

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
                            Id = new Guid("43c6114e-8e73-4d5a-ae5d-25fa5a0b9f38"),
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
                            Id = new Guid("c355a32f-f590-4090-9aea-dc9e9b708759"),
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
                    b.Navigation("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
