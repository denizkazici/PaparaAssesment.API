using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaparaAssesment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class _40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_UserId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_AppUserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Buildings_BuildingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AppUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BuildingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_UserId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "Payments",
                newName: "buildingId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentCategory",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Apartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Apartments_ApartmentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ApartmentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "buildingId",
                table: "Payments",
                newName: "BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentCategory",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Apartments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Apartments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppUserId",
                table: "Payments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BuildingId",
                table: "Payments",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_UserId",
                table: "Apartments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_UserId",
                table: "Apartments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_AppUserId",
                table: "Payments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Buildings_BuildingId",
                table: "Payments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
