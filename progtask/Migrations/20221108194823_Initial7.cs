using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace progtask.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    ModuleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    ClassHoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    WeeksInSemester = table.Column<int>(type: "int", nullable: false),
                    SemesterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecommendedHoursToStudy = table.Column<double>(type: "float", nullable: false),
                    HoursStudiedToday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.ModuleCode);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Username);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
