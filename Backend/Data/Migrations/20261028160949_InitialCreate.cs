using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauarntMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameSessies",
                columns: table => new
                {
                    GameSessieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartSituatieTijd = table.Column<int>(type: "int", nullable: false),
                    AantalIngesteldeRondes = table.Column<int>(type: "int", nullable: false),
                    TijdVoorRondesInMinuten = table.Column<int>(type: "int", nullable: false),
                    IngesteldeAantalTeams = table.Column<int>(type: "int", nullable: false),
                    IngesteldeAantalSpelersPerTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameSessies", x => x.GameSessieId);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Teamnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Afbeelding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameSessieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teams_gameSessies_GameSessieId",
                        column: x => x.GameSessieId,
                        principalTable: "gameSessies",
                        principalColumn: "GameSessieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamLid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamLid_teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamLid_TeamId",
                table: "TeamLid",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_GameSessieId",
                table: "teams",
                column: "GameSessieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamLid");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "gameSessies");
        }
    }
}
