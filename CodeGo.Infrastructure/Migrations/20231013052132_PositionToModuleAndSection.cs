using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PositionToModuleAndSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "sections");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "modules");
        }
    }
}
