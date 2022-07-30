using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessIngGameWithEfCore.Migrations
{
    public partial class AddNumberToGuess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Guess",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Guess");
        }
    }
}
