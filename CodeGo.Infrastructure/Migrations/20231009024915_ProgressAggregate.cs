using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProgressAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "progresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentModule = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentSection = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progresses", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "progressCompletedModuleIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletedModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progressCompletedModuleIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_progressCompletedModuleIds_progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "progressCompletedSectionIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletedSectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progressCompletedSectionIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_progressCompletedSectionIds_progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "progressLessonTrackingIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LessonTrackingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progressLessonTrackingIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_progressLessonTrackingIds_progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categoryTrackings_ProgressId",
                table: "categoryTrackings",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_progressCompletedModuleIds_ProgressId",
                table: "progressCompletedModuleIds",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_progressCompletedSectionIds_ProgressId",
                table: "progressCompletedSectionIds",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_progressLessonTrackingIds_ProgressId",
                table: "progressLessonTrackingIds",
                column: "ProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoryTrackings");

            migrationBuilder.DropTable(
                name: "progressCompletedModuleIds");

            migrationBuilder.DropTable(
                name: "progressCompletedSectionIds");

            migrationBuilder.DropTable(
                name: "progressLessonTrackingIds");

            migrationBuilder.DropTable(
                name: "progresses");
        }
    }
}
