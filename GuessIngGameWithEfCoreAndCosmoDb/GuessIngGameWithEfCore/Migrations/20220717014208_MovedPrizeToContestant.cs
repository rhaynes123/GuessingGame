using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessIngGameWithEfCoreAndCosmoDb.Migrations
{
    public partial class MovedPrizeToContestant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guess_Prize_PrizeId",
                table: "Guess");

            migrationBuilder.DropIndex(
                name: "IX_Guess_PrizeId",
                table: "Guess");

            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "Guess");

            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "Contestant",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "Contestant");

            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "Guess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Guess_PrizeId",
                table: "Guess",
                column: "PrizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guess_Prize_PrizeId",
                table: "Guess",
                column: "PrizeId",
                principalTable: "Prize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
