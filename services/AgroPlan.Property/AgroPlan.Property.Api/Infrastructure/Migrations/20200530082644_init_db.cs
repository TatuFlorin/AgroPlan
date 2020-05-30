using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPlan.Property.Api.Infrastructure.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
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
                    Surface = table.Column<float>(nullable: true),
                    NorthNeighbor = table.Column<string>(nullable: true),
                    SouthNeighbor = table.Column<string>(nullable: true),
                    WestHeighbor = table.Column<string>(nullable: true),
                    EastNeighbor = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PhysicalBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PBCode = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalBlocks_Properties_Id",
                        column: x => x.Id,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pracels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParcelCode = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracels_Properties_Id",
                        column: x => x.Id,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhysicalBlocks");

            migrationBuilder.DropTable(
                name: "Pracels");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
