using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    public partial class updatedSubscriptionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("d781d6af-1a3c-4576-871e-27bab65534e6") });

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Subscription");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("c9057a7e-8aaa-42b2-9a04-ef0811a6c329"), new DateTime(2022, 9, 3, 0, 11, 18, 934, DateTimeKind.Local).AddTicks(9299), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464,", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("c9057a7e-8aaa-42b2-9a04-ef0811a6c329") });

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("d781d6af-1a3c-4576-871e-27bab65534e6"), new DateTime(2022, 9, 3, 0, 6, 55, 826, DateTimeKind.Local).AddTicks(8004), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464,", null, null, null });
        }
    }
}
