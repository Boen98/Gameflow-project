using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauarntMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class VoegTeamcodeToe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_teams_GameSessieId",
                table: "teams");

            migrationBuilder.AddColumn<string>(
                name: "Teamcode",
                table: "teams",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_teams_GameSessieId_Teamcode",
                table: "teams",
                columns: new[] { "GameSessieId", "Teamcode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_teams_GameSessieId_Teamcode",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "Teamcode",
                table: "teams");

            migrationBuilder.CreateIndex(
                name: "IX_teams_GameSessieId",
                table: "teams",
                column: "GameSessieId");
        }
    }
}
