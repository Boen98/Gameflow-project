using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauarntMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class EndpointsAfschermen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "TeamLid",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "TeamLid");
        }
    }
}
