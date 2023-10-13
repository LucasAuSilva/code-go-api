using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModuleTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentModule",
                table: "progresses");

            migrationBuilder.CreateTable(
                name: "moduleTrackings",
                columns: table => new
                {
                    ModuleTrackingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonsCompleted = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moduleTrackings", x => new { x.ModuleTrackingId, x.ProgressId });
                    table.ForeignKey(
                        name: "FK_moduleTrackings_progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_moduleTrackings_ProgressId",
                table: "moduleTrackings",
                column: "ProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moduleTrackings");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentModule",
                table: "progresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
