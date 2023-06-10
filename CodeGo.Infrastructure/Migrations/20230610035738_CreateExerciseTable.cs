using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateExerciseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseCode = table.Column<string>(type: "TEXT", nullable: false),
                    Difficulty_Value = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "testCases",
                columns: table => new
                {
                    TestCaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testCases", x => new { x.ExerciseId, x.TestCaseId });
                    table.ForeignKey(
                        name: "FK_testCases_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "testCases");

            migrationBuilder.DropTable(
                name: "exercises");
        }
    }
}
