using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TankGame.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameMaps",
                columns: table => new
                {
                    GameMapId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Map = table.Column<string>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    NoObstacles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMaps", x => x.GameMapId);
                });

            migrationBuilder.CreateTable(
                name: "TankModels",
                columns: table => new
                {
                    TankModelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    TankModelName = table.Column<string>(maxLength: 300, nullable: false),
                    TankModelDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    GunRange = table.Column<int>(nullable: false),
                    GunPower = table.Column<double>(nullable: false),
                    ShieldLife = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TankModels", x => x.TankModelId);
                });

            migrationBuilder.CreateTable(
                name: "GameBattles",
                columns: table => new
                {
                    GameBattleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    BlueTankModelId = table.Column<int>(nullable: false),
                    BlueTankX = table.Column<int>(nullable: false),
                    BlueTankY = table.Column<int>(nullable: false),
                    RedTankX = table.Column<int>(nullable: false),
                    RedTankY = table.Column<int>(nullable: false),
                    RedTankModelId = table.Column<int>(nullable: false),
                    GameMapId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBattles", x => x.GameBattleId);
                    table.ForeignKey(
                        name: "FK_GameBattles_TankModels_BlueTankModelId",
                        column: x => x.BlueTankModelId,
                        principalTable: "TankModels",
                        principalColumn: "TankModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameBattles_GameMaps_GameMapId",
                        column: x => x.GameMapId,
                        principalTable: "GameMaps",
                        principalColumn: "GameMapId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameBattles_TankModels_RedTankModelId",
                        column: x => x.RedTankModelId,
                        principalTable: "TankModels",
                        principalColumn: "TankModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSimulations",
                columns: table => new
                {
                    GameSimulationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    GameBattleId = table.Column<int>(nullable: false),
                    Simulation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSimulations", x => x.GameSimulationId);
                    table.ForeignKey(
                        name: "FK_GameSimulations_GameBattles_GameBattleId",
                        column: x => x.GameBattleId,
                        principalTable: "GameBattles",
                        principalColumn: "GameBattleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameBattles_BlueTankModelId",
                table: "GameBattles",
                column: "BlueTankModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBattles_GameMapId",
                table: "GameBattles",
                column: "GameMapId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBattles_RedTankModelId",
                table: "GameBattles",
                column: "RedTankModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSimulations_GameBattleId",
                table: "GameSimulations",
                column: "GameBattleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSimulations");

            migrationBuilder.DropTable(
                name: "GameBattles");

            migrationBuilder.DropTable(
                name: "TankModels");

            migrationBuilder.DropTable(
                name: "GameMaps");
        }
    }
}
