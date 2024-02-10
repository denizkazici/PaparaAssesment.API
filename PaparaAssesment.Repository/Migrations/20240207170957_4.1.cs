using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaparaAssesment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class _41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApartmentId",
                table: "AspNetUsers",
                column: "ApartmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Apartments_ApartmentId",
                table: "AspNetUsers",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Apartments_ApartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Apartments");
        }
    }
}
