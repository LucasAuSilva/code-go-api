using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RankingAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rankings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Period_InitialDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Period_EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rankings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankingProgresses",
                columns: table => new
                {
                    RankingProgressId = table.Column<Guid>(type: "uuid", nullable: false),
                    RankingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserFullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Points_Points = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingProgresses", x => new { x.RankingId, x.RankingProgressId });
                    table.ForeignKey(
                        name: "FK_RankingProgresses_rankings_RankingId",
                        column: x => x.RankingId,
                        principalTable: "rankings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingProgresses");

            migrationBuilder.DropTable(
                name: "rankings");
        }
    }
}
