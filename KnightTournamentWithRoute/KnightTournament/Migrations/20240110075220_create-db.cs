using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnightTournament.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_Rank = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    User_Rating = table.Column<int>(type: "int", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_ImgUri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Location_Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Tournament_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tournament_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tournament_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tournament_Scope = table.Column<int>(type: "int", nullable: false),
                    Tournament_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tournament_Status = table.Column<int>(type: "int", nullable: false),
                    Tournament_LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tournament_HolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Tournament_Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_AspNetUsers_Tournament_HolderId",
                        column: x => x.Tournament_HolderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tournaments_Locations_Tournament_LocationId",
                        column: x => x.Tournament_LocationId,
                        principalTable: "Locations",
                        principalColumn: "Location_Id");
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Round_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Round_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Round_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Round_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Round_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Round_TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Round_Id);
                    table.ForeignKey(
                        name: "FK_Rounds_Tournaments_Round_TournamentId",
                        column: x => x.Round_TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Tournament_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentUsers",
                columns: table => new
                {
                    TournamentUsers_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TournamentUsers_TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TournamentUsers_KnightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentUsers", x => x.TournamentUsers_Id);
                    table.ForeignKey(
                        name: "FK_TournamentUsers_AspNetUsers_TournamentUsers_KnightId",
                        column: x => x.TournamentUsers_KnightId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentUsers_Tournaments_TournamentUsers_TournamentId",
                        column: x => x.TournamentUsers_TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Tournament_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Combats",
                columns: table => new
                {
                    Combat_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Combat_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Combat_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Combat_Type = table.Column<int>(type: "int", nullable: false),
                    Combat_IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    Combat_RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combats", x => x.Combat_Id);
                    table.ForeignKey(
                        name: "FK_Combats_Rounds_Combat_RoundId",
                        column: x => x.Combat_RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trophies",
                columns: table => new
                {
                    Trophy_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Trophy_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trophy_Value = table.Column<double>(type: "float", nullable: false),
                    Trophy_RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Trophy_KnightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trophies", x => x.Trophy_Id);
                    table.ForeignKey(
                        name: "FK_Trophies_AspNetUsers_Trophy_KnightId",
                        column: x => x.Trophy_KnightId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id");
                    table.ForeignKey(
                        name: "FK_Trophies_Rounds_Trophy_RoundId",
                        column: x => x.Trophy_RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatsKnights",
                columns: table => new
                {
                    CombatsKnight_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CombatsKnight_Points = table.Column<int>(type: "int", nullable: false),
                    CombatsKnight_CombatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CombatsKnight_AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatsKnights", x => x.CombatsKnight_Id);
                    table.ForeignKey(
                        name: "FK_CombatsKnights_AspNetUsers_CombatsKnight_AppUserId",
                        column: x => x.CombatsKnight_AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Round_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatsKnights_Combats_CombatsKnight_CombatId",
                        column: x => x.CombatsKnight_CombatId,
                        principalTable: "Combats",
                        principalColumn: "Combat_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Combats_Combat_RoundId",
                table: "Combats",
                column: "Combat_RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatsKnights_CombatsKnight_AppUserId",
                table: "CombatsKnights",
                column: "CombatsKnight_AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatsKnights_CombatsKnight_CombatId",
                table: "CombatsKnights",
                column: "CombatsKnight_CombatId");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_Round_TournamentId",
                table: "Rounds",
                column: "Round_TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Tournament_HolderId",
                table: "Tournaments",
                column: "Tournament_HolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_Tournament_LocationId",
                table: "Tournaments",
                column: "Tournament_LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_TournamentUsers_KnightId",
                table: "TournamentUsers",
                column: "TournamentUsers_KnightId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUsers_TournamentUsers_TournamentId",
                table: "TournamentUsers",
                column: "TournamentUsers_TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trophies_Trophy_KnightId",
                table: "Trophies",
                column: "Trophy_KnightId");

            migrationBuilder.CreateIndex(
                name: "IX_Trophies_Trophy_RoundId",
                table: "Trophies",
                column: "Trophy_RoundId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CombatsKnights");

            migrationBuilder.DropTable(
                name: "TournamentUsers");

            migrationBuilder.DropTable(
                name: "Trophies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Combats");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
