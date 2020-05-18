using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPlan.Planification.Api.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    usage_surface = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "croptypes",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    crop_name = table.Column<string>(nullable: true),
                    crop_code = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crop_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planification",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    client_id = table.Column<string>(nullable: true),
                    planification_year = table.Column<int>(nullable: false),
                    total_surface = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_planifications_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "crops",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    planification_id = table.Column<Guid>(nullable: true),
                    type_id = table.Column<Guid>(nullable: true),
                    surface = table.Column<float>(nullable: true),
                    duration = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_crop", x => x.id);
                    table.ForeignKey(
                        name: "fk_crop_planifications_planification_id",
                        column: x => x.planification_id,
                        principalTable: "planification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_crop_crop_types_type_id",
                        column: x => x.type_id,
                        principalTable: "croptypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_crops_planification_id",
                table: "crops",
                column: "planification_id");

            migrationBuilder.CreateIndex(
                name: "ix_crops_type_id",
                table: "crops",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_planification_client_id",
                table: "planification",
                column: "client_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crops");

            migrationBuilder.DropTable(
                name: "planification");

            migrationBuilder.DropTable(
                name: "croptypes");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
