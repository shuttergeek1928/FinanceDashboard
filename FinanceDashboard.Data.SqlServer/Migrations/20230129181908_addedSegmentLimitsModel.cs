using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addedSegmentLimitsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscription",
                keyColumn: "Id",
                keyValue: new Guid("2f5bd117-debc-4109-be28-b2a7cee51025"));

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("53ab364d-587f-4d40-957a-e0b88636b7ce") });

            migrationBuilder.CreateTable(
                name: "SegmentLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmiLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentLimits_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SegmentLimits_AccountId",
                table: "SegmentLimits",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SegmentLimits");

            migrationBuilder.DeleteData(
                table: "Subscription",
                keyColumn: "Id",
                keyValue: new Guid("4ed63ee2-cce9-4f15-82f9-bc9b619207a7"));

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumns: new[] { "AccountId", "Id" },
                keyValues: new object[] { 1, new Guid("4b264e63-687f-4d10-a4ba-703af6b6870d") });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("53ab364d-587f-4d40-957a-e0b88636b7ce"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "atishay1928@outlook.com", "Atishay", "@6ZD3aazp-zp", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=,", null, null, null });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "AccountId", "Amount", "BillingDate", "CanceledBy", "CanceledOn", "IsExpired", "LastUpdate", "LastUpdateBy", "Password", "RenewalAmount", "RenewalCycle", "RenewalDate", "SubscribedOnEmail", "SubscribedOnMobileNumber", "SubscriptionName" },
                values: new object[] { new Guid("2f5bd117-debc-4109-be28-b2a7cee51025"), 1, 500m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, null, null, "Password1!", 350m, 1, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "atishay1928@outlook.com", "9827766387", "Netflix" });
        }
    }
}
