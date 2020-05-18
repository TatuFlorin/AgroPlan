using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPlan.Planification.Api.Infrastructure.Migrations
{
    public partial class FixSomeBugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total_surface",
                table: "planification",
                newName: "surface");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surface",
                table: "planification",
                newName: "total_surface");
        }
    }
}
