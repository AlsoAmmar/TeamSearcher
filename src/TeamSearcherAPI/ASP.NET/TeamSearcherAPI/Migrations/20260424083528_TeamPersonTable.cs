using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamSearcherAPI.Migrations
{
    /// <inheritdoc />
    public partial class TeamPersonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tag",
                table: "Team",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "maxCount",
                table: "Team",
                newName: "MaxCount");

            migrationBuilder.RenameColumn(
                name: "currentCount",
                table: "Team",
                newName: "CurrentCount");

            migrationBuilder.CreateTable(
                name: "TeamPersons",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPersons", x => new { x.PersonId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamPersons_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPersons_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamPersons_TeamId",
                table: "TeamPersons",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPersons");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Team",
                newName: "tag");

            migrationBuilder.RenameColumn(
                name: "MaxCount",
                table: "Team",
                newName: "maxCount");

            migrationBuilder.RenameColumn(
                name: "CurrentCount",
                table: "Team",
                newName: "currentCount");
        }
    }
}
