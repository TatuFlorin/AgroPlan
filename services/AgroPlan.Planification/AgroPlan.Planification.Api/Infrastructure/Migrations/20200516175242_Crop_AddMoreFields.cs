using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroPlan.Planification.Api.Infrastructure.Migrations
{
    public partial class Crop_AddMoreFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_crop_crop_types_type_id",
                table: "crops");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "crops",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "planification_id",
                table: "crops",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parcel_code",
                table: "crops",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "physical_block_code",
                table: "crops",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_crop_crop_types_type_id",
                table: "crops",
                column: "type_id",
                principalTable: "croptypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_crop_crop_types_type_id",
                table: "crops");

            migrationBuilder.DropColumn(
                name: "parcel_code",
                table: "crops");

            migrationBuilder.DropColumn(
                name: "physical_block_code",
                table: "crops");

            migrationBuilder.AlterColumn<Guid>(
                name: "type_id",
                table: "crops",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "planification_id",
                table: "crops",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "fk_crop_crop_types_type_id",
                table: "crops",
                column: "type_id",
                principalTable: "croptypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
