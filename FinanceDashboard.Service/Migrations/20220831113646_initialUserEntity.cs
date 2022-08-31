using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Service.Migrations
{
    public partial class initialUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    AccpuntId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_User", x => new { x.AccpuntId, x.Id });
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "AccpuntId", "Id", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "FirstName", "HashingSalt", "InvalidPasswordCount", "IsLocked", "LastName", "MobileNumber", "Name", "PasswordHash", "PasswordHashHistory", "SecondFactorKey", "SecondFactorValidated", "VerifiedOn" },
                values: new object[] { 1, new Guid("e7f52b42-b802-4d1e-a029-9aff55f18d56"), new DateTime(2022, 8, 31, 17, 6, 45, 721, DateTimeKind.Local).AddTicks(8447), null, null, "atishay1928@outlook.com", "Atishay", "==4d8dh51d9c", null, null, "Vishwakarma", "9827766387", "Atishay Vishwakarma", "FSZ2d17i8jn8wewbvMuht62CYYaohPtv3b8xzlMGHTA=", "FSZ2d17i8jn8wewbvMuht62CYYaohPtv3b8xzlMGHTA=,", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
