using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamSearcherAPI.Migrations
{
    /// <inheritdoc />
    public partial class TeamPersonStatusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TeamPersons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TeamPersons");
        }
    }
}
