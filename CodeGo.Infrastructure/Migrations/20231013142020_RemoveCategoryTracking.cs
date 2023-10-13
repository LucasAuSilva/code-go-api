using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoryTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoryTrackings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoryTrackings",
                columns: table => new
                {
                    CategoryTrackingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    DifficultyLevel_Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoryTrackings", x => new { x.CategoryTrackingId, x.ProgressId });
                    table.ForeignKey(
                        name: "FK_categoryTrackings_progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoryTrackings_ProgressId",
                table: "categoryTrackings",
                column: "ProgressId");
        }
    }
}
