using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauarntMinimalApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class VoegRondeToe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rondes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameSessieId = table.Column<int>(type: "int", nullable: false),
                    Nummer = table.Column<int>(type: "int", nullable: false),
                    GeplandeStartMoment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAfgesloten = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rondes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rondes_gameSessies_GameSessieId",
                        column: x => x.GameSessieId,
                        principalTable: "gameSessies",
                        principalColumn: "GameSessieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rondes_GameSessieId_Nummer",
                table: "rondes",
                columns: new[] { "GameSessieId", "Nummer" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rondes");
        }
    }
}
