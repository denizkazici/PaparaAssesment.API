using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaparaAssesment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class _60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Payments");
        }
    }
}
