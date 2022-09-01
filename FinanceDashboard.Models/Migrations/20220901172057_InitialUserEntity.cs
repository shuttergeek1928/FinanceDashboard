using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    public partial class InitialUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
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
                    table.PrimaryKey("PK_User", x => new { x.AccountId, x.Id });
                    table.UniqueConstraint("AlternateKey_Email", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccountId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("160c14f8-d9ee-47b0-a072-0bfffb9d461d"), new DateTime(2022, 9, 1, 22, 50, 56, 979, DateTimeKind.Local).AddTicks(3384), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464", "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464,", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
