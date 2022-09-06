using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    public partial class addedLastUpdateInSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Subscription",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdateBy",
                table: "Subscription",
                type: "int",
                nullable: true);            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscription",
                keyColumn: "Id",
                keyValue: new Guid("c355a32f-f590-4090-9aea-dc9e9b708759"));

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("43c6114e-8e73-4d5a-ae5d-25fa5a0b9f38") });

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "LastUpdateBy",
                table: "Subscription");

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("4a5e417f-e4a1-4915-af6a-a0875643e474"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "atishay1928@outlook.com", "Atishay", "@6ZD3aazp-zp", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=,", null, null, null });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "AccountId", "Amount", "BillingDate", "CanceledBy", "CanceledOn", "IsExpired", "Password", "RenewalAmount", "RenewalCycle", "RenewalDate", "SubscribedOnEmail", "SubscribedOnMobileNumber", "SubscriptionName" },
                values: new object[] { new Guid("6087fa18-1640-496e-84b3-2eeb629b45c0"), 1, 500m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Password1!", 350m, 1, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "atishay1928@outlook.com", "9827766387", "Netflix" });
        }
    }
}
