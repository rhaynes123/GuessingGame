using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessIngGameWithEfCoreAndCosmoDb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WinningNumber = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contestant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContestId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contestant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contestant_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Place = table.Column<int>(type: "int", nullable: false),
                    IsWon = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ContestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prize_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Guess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContestId = table.Column<int>(type: "int", nullable: false),
                    ContestantId = table.Column<int>(type: "int", nullable: false),
                    PrizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guess_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guess_Contestant_ContestantId",
                        column: x => x.ContestantId,
                        principalTable: "Contestant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guess_Prize_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "Prize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Contestant_ContestId",
                table: "Contestant",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Guess_ContestantId",
                table: "Guess",
                column: "ContestantId");

            migrationBuilder.CreateIndex(
                name: "IX_Guess_ContestId",
                table: "Guess",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Guess_PrizeId",
                table: "Guess",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prize_ContestId",
                table: "Prize",
                column: "ContestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guess");

            migrationBuilder.DropTable(
                name: "Contestant");

            migrationBuilder.DropTable(
                name: "Prize");

            migrationBuilder.DropTable(
                name: "Contest");
        }
    }
}

/*
 * DROP SCHEMA game;
CREATE SCHEMA game;
USE game;
DROP TABLE IF EXISTS game.contest;
CREATE TABLE game.contest(
	Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Uid VARCHAR(255) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	WinningNumber INT NOT NULL,
	Active BOOLEAN NOT NULL DEFAULT 1,
	INDEX(Name)
	
);
DROP TABLE IF EXISTS game.prizes;
CREATE TABLE game.prizes(
	Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Description VARCHAR(100) NOT NULL,
	ContestId INT NOT NULL,
	Place TINYINT NOT NULL,
	IsWon BOOLEAN NOT NULL DEFAULT 0,
	FOREIGN KEY(contestId) REFERENCES game.contest(Id) 
);

DROP TABLE IF EXISTS game.contestant; 
CREATE TABLE game.contestant(
	Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	ContestId INT NOT NULL, 
	Name VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	FOREIGN KEY(contestId) REFERENCES game.contest(Id) 
);

DROP TABLE IF EXISTS game.guess;
CREATE TABLE game.guess(
Id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	ContestId INT NOT NULL,
	ContestantId INT NOT NULL,
	PrizeId INT NOT NULL,
	FOREIGN KEY(contestId) REFERENCES game.contest(Id) ,
	FOREIGN KEY(ContestantId) REFERENCES game.contestant(Id) ,
	FOREIGN KEY(PrizeId) REFERENCES game.prizes(Id) 
);

 */