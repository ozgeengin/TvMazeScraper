using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RTL.TvMazeScraper.Infastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TvMazeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.UniqueConstraint("AK_People_TvMazeId", x => x.TvMazeId);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TvMazeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.UniqueConstraint("AK_Shows_TvMazeId", x => x.TvMazeId);
                });

            migrationBuilder.CreateTable(
                name: "ShowCast",
                columns: table => new
                {
                    PersonTvMazeId = table.Column<int>(type: "int", nullable: false),
                    ShowTvMazeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowCast", x => new { x.PersonTvMazeId, x.ShowTvMazeId });
                    table.ForeignKey(
                        name: "FK_ShowCast_People_PersonTvMazeId",
                        column: x => x.PersonTvMazeId,
                        principalTable: "People",
                        principalColumn: "TvMazeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowCast_Shows_ShowTvMazeId",
                        column: x => x.ShowTvMazeId,
                        principalTable: "Shows",
                        principalColumn: "TvMazeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_TvMazeId",
                table: "People",
                column: "TvMazeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowCast_ShowTvMazeId",
                table: "ShowCast",
                column: "ShowTvMazeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_TvMazeId",
                table: "Shows",
                column: "TvMazeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowCast");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
