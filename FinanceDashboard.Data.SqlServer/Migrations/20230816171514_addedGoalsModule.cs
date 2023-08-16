using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class addedGoalsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AccountEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AchievedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanceledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanceledBy = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => new { x.GoalId, x.Id });
                    table.ForeignKey(
                        name: "FK_Goals_Account_AccountId_AccountEntityId",
                        columns: x => new { x.AccountId, x.AccountEntityId },
                        principalTable: "Account",
                        principalColumns: new[] { "AccountId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_AccountId_AccountEntityId",
                table: "Goals",
                columns: new[] { "AccountId", "AccountEntityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
