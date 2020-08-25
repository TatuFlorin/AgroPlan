using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPlan.Property.Api.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    TotalSurface = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NorthNeighbor = table.Column<string>(maxLength: 30, nullable: true),
                    SouthNeighbor = table.Column<string>(maxLength: 30, nullable: true),
                    WestHeighbor = table.Column<string>(maxLength: 30, nullable: true),
                    EastNeighbor = table.Column<string>(maxLength: 30, nullable: true),
                    OwnerId = table.Column<string>(nullable: false),
                    Surface = table.Column<float>(nullable: false),
                    PhysicalBlockId = table.Column<int>(nullable: false),
                    ParcelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
