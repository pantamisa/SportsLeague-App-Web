using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsLeague.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixMatchLineupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineup_Matches_MatchId",
                table: "MatchLineup");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineup_Players_PlayerId",
                table: "MatchLineup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchLineup",
                table: "MatchLineup");

            migrationBuilder.RenameTable(
                name: "MatchLineup",
                newName: "MatchLineups");

            migrationBuilder.RenameIndex(
                name: "IX_MatchLineup_PlayerId",
                table: "MatchLineups",
                newName: "IX_MatchLineups_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchLineup_MatchId_PlayerId",
                table: "MatchLineups",
                newName: "IX_MatchLineups_MatchId_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchLineups",
                table: "MatchLineups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineups_Matches_MatchId",
                table: "MatchLineups",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineups_Matches_MatchId",
                table: "MatchLineups");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchLineups",
                table: "MatchLineups");

            migrationBuilder.RenameTable(
                name: "MatchLineups",
                newName: "MatchLineup");

            migrationBuilder.RenameIndex(
                name: "IX_MatchLineups_PlayerId",
                table: "MatchLineup",
                newName: "IX_MatchLineup_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchLineups_MatchId_PlayerId",
                table: "MatchLineup",
                newName: "IX_MatchLineup_MatchId_PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchLineup",
                table: "MatchLineup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineup_Matches_MatchId",
                table: "MatchLineup",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineup_Players_PlayerId",
                table: "MatchLineup",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
