using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnightTournament.Migrations
{
    /// <inheritdoc />
    public partial class ChangeManytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatsKnights_AspNetUsers_KnightId",
                table: "CombatsKnights");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatsKnights_Combats_CombatId",
                table: "CombatsKnights");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentUsers_AspNetUsers_KnightId",
                table: "TournamentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentUsers_Tournaments_TournamentId",
                table: "TournamentUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatsKnights_AspNetUsers_KnightId",
                table: "CombatsKnights",
                column: "KnightId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatsKnights_Combats_CombatId",
                table: "CombatsKnights",
                column: "CombatId",
                principalTable: "Combats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentUsers_AspNetUsers_KnightId",
                table: "TournamentUsers",
                column: "KnightId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentUsers_Tournaments_TournamentId",
                table: "TournamentUsers",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatsKnights_AspNetUsers_KnightId",
                table: "CombatsKnights");

            migrationBuilder.DropForeignKey(
                name: "FK_CombatsKnights_Combats_CombatId",
                table: "CombatsKnights");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentUsers_AspNetUsers_KnightId",
                table: "TournamentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentUsers_Tournaments_TournamentId",
                table: "TournamentUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatsKnights_AspNetUsers_KnightId",
                table: "CombatsKnights",
                column: "KnightId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CombatsKnights_Combats_CombatId",
                table: "CombatsKnights",
                column: "CombatId",
                principalTable: "Combats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentUsers_AspNetUsers_KnightId",
                table: "TournamentUsers",
                column: "KnightId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentUsers_Tournaments_TournamentId",
                table: "TournamentUsers",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
