using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    public partial class InitUserAndSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashingSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHashHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvalidPasswordCount = table.Column<short>(type: "smallint", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: true),
                    SecondFactorKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondFactorValidated = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => new { x.AccountId, x.Id });
                    table.UniqueConstraint("AK_Account_AccountId", x => x.AccountId);
                    table.UniqueConstraint("AlternateKey_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Subscription_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("4a5e417f-e4a1-4915-af6a-a0875643e474"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "atishay1928@outlook.com", "Atishay", "@6ZD3aazp-zp", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=", "rlJuiL2QibBsb6S/cFinFP9BYEYx8bGObdhMJ0k3RIQ=,", null, null, null });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "AccountId", "Amount", "BillingDate", "CanceledBy", "CanceledOn", "IsExpired", "Password", "RenewalAmount", "RenewalCycle", "RenewalDate", "SubscribedOnEmail", "SubscribedOnMobileNumber", "SubscriptionName" },
                values: new object[] { new Guid("6087fa18-1640-496e-84b3-2eeb629b45c0"), 1, 500m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, "Password1!", 350m, 1, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "atishay1928@outlook.com", "9827766387", "Netflix" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_AccountId",
                table: "Subscription",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
