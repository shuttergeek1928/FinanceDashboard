using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    public partial class addedSubscriptionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("160c14f8-d9ee-47b0-a072-0bfffb9d461d") });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribedOnEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscribedOnMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewalCycle = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RenewalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CanceledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledBy = table.Column<int>(type: "int", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_User_UserAccountId_UserId",
                        columns: x => new { x.UserAccountId, x.UserId },
                        principalTable: "User",
                        principalColumns: new[] { "AccountId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("d781d6af-1a3c-4576-871e-27bab65534e6"), new DateTime(2022, 9, 3, 0, 6, 55, 826, DateTimeKind.Local).AddTicks(8004), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464,", null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserAccountId_UserId",
                table: "Subscription",
                columns: new[] { "UserAccountId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("d781d6af-1a3c-4576-871e-27bab65534e6") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("160c14f8-d9ee-47b0-a072-0bfffb9d461d"), new DateTime(2022, 9, 1, 22, 50, 56, 979, DateTimeKind.Local).AddTicks(3384), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464,", null, null, null });
        }
    }
}
