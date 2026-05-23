using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsLeague.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPersistentStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Matches_MatchId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Players_PlayerId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Matches_MatchId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Players_PlayerId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchResult_Matches_MatchId",
                table: "MatchResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchResult",
                table: "MatchResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.RenameTable(
                name: "MatchResult",
                newName: "MatchResults");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_MatchResult_MatchId",
                table: "MatchResults",
                newName: "IX_MatchResults_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_PlayerId",
                table: "Goals",
                newName: "IX_Goals_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_MatchId",
                table: "Goals",
                newName: "IX_Goals_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_PlayerId",
                table: "Cards",
                newName: "IX_Cards_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_MatchId",
                table: "Cards",
                newName: "IX_Cards_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchResults",
                table: "MatchResults",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PlayerDisciplines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    TotalCards = table.Column<int>(type: "int", nullable: false),
                    IsSuspendedNextMatch = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerDisciplines_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerDisciplines_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerScorers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Penalties = table.Column<int>(type: "int", nullable: false),
                    MatchesWithGoals = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScorers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerScorers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerScorers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamStandings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalDifference = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStandings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStandings_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStandings_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDisciplines_PlayerId",
                table: "PlayerDisciplines",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDisciplines_RedCards_YellowCards",
                table: "PlayerDisciplines",
                columns: new[] { "RedCards", "YellowCards" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDisciplines_TournamentId_PlayerId",
                table: "PlayerDisciplines",
                columns: new[] { "TournamentId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScorers_Goals_MatchesWithGoals",
                table: "PlayerScorers",
                columns: new[] { "Goals", "MatchesWithGoals" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScorers_PlayerId",
                table: "PlayerScorers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScorers_TournamentId_PlayerId",
                table: "PlayerScorers",
                columns: new[] { "TournamentId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamStandings_Points_GoalDifference_GoalsFor",
                table: "TeamStandings",
                columns: new[] { "Points", "GoalDifference", "GoalsFor" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStandings_TeamId",
                table: "TeamStandings",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStandings_TournamentId_TeamId",
                table: "TeamStandings",
                columns: new[] { "TournamentId", "TeamId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Matches_MatchId",
                table: "Cards",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Players_PlayerId",
                table: "Cards",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Matches_MatchId",
                table: "Goals",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Players_PlayerId",
                table: "Goals",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResults_Matches_MatchId",
                table: "MatchResults",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Matches_MatchId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Players_PlayerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Matches_MatchId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Players_PlayerId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchResults_Matches_MatchId",
                table: "MatchResults");

            migrationBuilder.DropTable(
                name: "PlayerDisciplines");

            migrationBuilder.DropTable(
                name: "PlayerScorers");

            migrationBuilder.DropTable(
                name: "TeamStandings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchResults",
                table: "MatchResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "MatchResults",
                newName: "MatchResult");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameIndex(
                name: "IX_MatchResults_MatchId",
                table: "MatchResult",
                newName: "IX_MatchResult_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_PlayerId",
                table: "Goal",
                newName: "IX_Goal_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_MatchId",
                table: "Goal",
                newName: "IX_Goal_MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_PlayerId",
                table: "Card",
                newName: "IX_Card_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_MatchId",
                table: "Card",
                newName: "IX_Card_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchResult",
                table: "MatchResult",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Matches_MatchId",
                table: "Card",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Players_PlayerId",
                table: "Card",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Matches_MatchId",
                table: "Goal",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Players_PlayerId",
                table: "Goal",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResult_Matches_MatchId",
                table: "MatchResult",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
