using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LessonTrackingAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lessonTrackings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonTrackings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "practices",
                columns: table => new
                {
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonTrackingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<string>(type: "character varying(38)", maxLength: 38, nullable: false),
                    AnswerId = table.Column<string>(type: "character varying(38)", maxLength: 38, nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practices", x => new { x.PracticeId, x.LessonTrackingId });
                    table.ForeignKey(
                        name: "FK_practices_lessonTrackings_LessonTrackingId",
                        column: x => x.LessonTrackingId,
                        principalTable: "lessonTrackings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_practices_LessonTrackingId",
                table: "practices",
                column: "LessonTrackingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "practices");

            migrationBuilder.DropTable(
                name: "lessonTrackings");
        }
    }
}
