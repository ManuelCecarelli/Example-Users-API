using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "FirstName", "HashedPassword", "LastName", "LastUpdate", "ProfileImageUrl", "Role", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 14, 3, 32, 52, 681, DateTimeKind.Utc).AddTicks(3907), "manu@gmail.com", "Manuel", "manu1234", "Cecarelli", null, null, 0, 0 },
                    { 2, new DateTime(2025, 10, 14, 3, 32, 52, 681, DateTimeKind.Utc).AddTicks(3913), "fede@gmail.com", "Federico", "fede1234", "Perez", null, null, 1, 0 },
                    { 3, new DateTime(2025, 10, 14, 3, 32, 52, 681, DateTimeKind.Utc).AddTicks(3914), "viky@gmail.com", "Victoria", "viky1234", "Lopez", null, null, 2, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
